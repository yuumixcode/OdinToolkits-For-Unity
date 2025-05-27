#if UNITY_EDITOR
using UnityEditor;
using UnityEditor.Callbacks;
#endif
using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using Yuumix.OdinToolkits.Modules.Utilities.Runtime;
using Yuumix.OdinToolkits.YuumiEditor;
using Debug = UnityEngine.Debug;

namespace Yuumix.OdinToolkits.Modules.ComponentBinder.Runtime
{
    [DisallowMultipleComponent]
    public class BinderAssistant :
#if UNITY_EDITOR
        SerializedMonoBehaviour
#else
        MonoBehaviour
#endif
    {
        [PropertyOrder(-5)]
        [HorizontalGroup("Bool")]
        [ToggleLeft]
        [LabelText("开启自动检查")]
        public bool openAutoValidate = true;

        [PropertyOrder(-3)]
        [HorizontalGroup("Bool")]
        [ToggleLeft]
        [LabelText("当前绑定信息有错误")]
        [ShowInInspector]
        public bool HasError { get; private set; }

        [LabelText("命名空间: ")]
        [LabelWidth(100)]
        [InlineButton("DefaultNamespace", "默认命名空间")]
        public string targetNamespace;

        [LabelText("脚本名: ")]
        [LabelWidth(100)]
        [InlineButton("DefaultScriptName", "默认脚本名")]
        public string scriptName;

        [ValueDropdown(nameof(GetBaseTypes))]
        [LabelText("基类: ")]
        [LabelWidth(100)]
        public string baseType;

        [FolderPath(RequireExistingPath = true)]
        [LabelText("目标文件夹路径: ")]
        [LabelWidth(100)]
        public string folderPath;

        void DefaultNamespace()
        {
            targetNamespace = "Game";
        }

        void DefaultScriptName()
        {
            scriptName = gameObject.name + "Presenter";
        }

        public ValueDropdownList<string> GetBaseTypes()
        {
            var typeStrings = new ValueDropdownList<string>
            {
                new ValueDropdownItem<string>(nameof(MonoBehaviour), typeof(MonoBehaviour).FullName)
            };
            return typeStrings;
        }

        public string HierarchyPath
        {
            get
            {
#if UNITY_EDITOR
                return HierarchyEditorUtil.GetAbsolutePath(transform);
#else
                return string.Empty;
#endif
            }
        }

        [Title("自动绑定列表")]
        public List<BinderUnit> units;

        [TitleGroup("自定义命名空间", "示例: UnityEngine.UI")]
        public List<string> customNamespaces;

        [TitleGroup("按钮操作")]
        [Button("构建绑定单元")]
        void CreateUnits()
        {
            var labels = transform.GetComponentsInChildren<BinderLabel>(true);
            // OdinLog.Log("获取到标签数量: " + labels.Length);
            foreach (var label in labels)
            {
                var number = label.ComponentNumber;
                for (var i = 0; i < units.Count; i++)
                {
                    var unit = units[i];
                    if (unit.labelObj != label.SelfObj)
                    {
                        continue;
                    }

                    unit.UpdatePath(this);
                    number--;
                }

                while (number > 0)
                {
                    units.Add(new BinderUnit(this, label));
                    number--;
                }
            }
        }

#if UNITY_EDITOR
        void Reset()
        {
            DefaultNamespace();
            DefaultScriptName();
            baseType = nameof(MonoBehaviour);
        }

        [TitleGroup("按钮操作")]
        [Button("生成文件夹")]
        void CreateFolder()
        {
            if (!AssetDatabase.IsValidFolder(folderPath))
            {
                Debug.Log("不存在该路径");
                var guid = AssetDatabase.CreateFolder("Assets", folderPath.Replace("Assets/", ""));
                var newFolderPath = AssetDatabase.GUIDToAssetPath(guid);
                Debug.Log(newFolderPath);
            }
        }

        [TitleGroup("按钮操作")]
        [Button("生成脚本")]
        void GenerateCode()
        {
            var generatedPath = Path.Combine(folderPath, scriptName + ".generated.cs");
            var controllerPath = Path.Combine(folderPath, scriptName + ".cs");
            try
            {
                WriteGeneratedScript(generatedPath);
                WriteControllerScript(controllerPath);
            }
            catch (Exception ex)
            {
                Debug.LogError($"生成脚本失败: {ex.Message}");
            }

            AssetDatabase.ImportAsset(controllerPath);
            AssetDatabase.ImportAsset(generatedPath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
        }

        void WriteControllerScript(string controllerPath)
        {
            using (var writer = new StreamWriter(controllerPath))
            {
                writer.WriteLine("// * ---------------------------------------------");
                writer.WriteLine("// * Controller 脚本仅由 Object Binder 生成一次");
                writer.WriteLine("// * 生成时间: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                writer.WriteLine("// * ---------------------------------------------");
                writer.WriteLine("using UnityEngine;");
                foreach (var item in customNamespaces)
                {
                    item.TrimEnd(';');
                    writer.WriteLine("using " + item + ";");
                }

                writer.WriteLine();
                writer.WriteLine("namespace " + targetNamespace);
                writer.WriteLine("{");
                writer.WriteLine("    public partial class " + scriptName);
                writer.WriteLine("    {");
                writer.WriteLine();
                writer.WriteLine("    }");
                writer.WriteLine("}");
                writer.Flush();
                writer.Close();
            }
        }

        void WriteGeneratedScript(string generatedPath)
        {
            using (var writer = new StreamWriter(generatedPath))
            {
                writer.WriteLine("// * ---------------------------------------------");
                writer.WriteLine("// * Generated 脚本由 Object Binder 自动生成，手动修改将会被覆盖");
                writer.WriteLine("// * 生成时间: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
                writer.WriteLine("// * ---------------------------------------------");
                writer.WriteLine("using UnityEngine;");
                writer.WriteLine("using Sirenix.OdinInspector;");
                foreach (var item in customNamespaces)
                {
                    item.TrimEnd(';');
                    writer.WriteLine("using " + item + ";");
                }

                writer.WriteLine();
                writer.WriteLine("namespace " + targetNamespace);
                writer.WriteLine("{");
                writer.WriteLine("    public partial class " + scriptName + " : " + baseType + ", " +
                                 typeof(IBindReferences).FullName);
                writer.WriteLine("    {");
                foreach (var unit in units)
                {
                    writer.WriteLine("        [PropertyOrder(-1000)]");
                    writer.WriteLine("        [TitleGroup(\"自动绑定变量\")]");
                    writer.WriteLine("        [BoxGroup(\"自动绑定变量/Box\",ShowLabel = false)]");
                    writer.WriteLine("        [SerializeField]");
                    writer.WriteLine("        " + unit.componentFullName + " " + unit.fieldName + ";");
                }

                writer.WriteLine();
                writer.WriteLine("        public void BindReferences()");
                writer.WriteLine("        {");
                foreach (var unit in units)
                {
                    if (unit.componentFullName == typeof(GameObject).FullName)
                    {
                        writer.WriteLine("            " + unit.fieldName + " = transform.Find(" + "\"" +
                                         unit.hierarchyPath +
                                         "\"" +
                                         ").gameObject;");
                    }
                    else
                    {
                        writer.WriteLine("            " + unit.fieldName + " = transform.Find(" + "\"" +
                                         unit.hierarchyPath +
                                         "\"" +
                                         ").GetComponent<" + unit.componentFullName +
                                         ">();");
                    }
                }

                writer.WriteLine("        }");
                writer.WriteLine();
                writer.WriteLine("        [ContextMenu(\"绑定引用\", false)]");
                writer.WriteLine("        public void BindCommand()");
                writer.WriteLine("        {");
                writer.WriteLine("            BindReferences();");
                writer.WriteLine("        }");
                writer.WriteLine("    }");
                writer.WriteLine("}");
                writer.Flush();
                writer.Close();
            }

            // 需要物体信息，脚本信息
            EditorPrefs.SetInt("即将绑定脚本的物体 Id", gameObject.GetInstanceID());
            EditorPrefs.SetString("即将绑定的脚本类型",
                targetNamespace + "." + scriptName +
                ", Assembly-CSharp, Version=0.0.0.0, Culture=neutral, PublicKeyToken=null");
            Debug.Log($"成功生成脚本: {generatedPath}");
        }

        [DidReloadScripts]
        static void CheckBinderUnit()
        {
            var assistants =
                FindObjectsByType<BinderAssistant>(FindObjectsInactive.Include, FindObjectsSortMode.None);
            foreach (var assistant in assistants)
            {
                assistant.HasError = false;
                if (!assistant.openAutoValidate)
                {
                    continue;
                }

                foreach (var unit in assistant.units)
                {
                    if (!unit.labelObj)
                    {
                        assistant.HasError = true;
                    }

                    if (unit.hierarchyPath !=
                        HierarchyUtil.GetRelativePath(assistant.HierarchyPath,
                            unit.labelObj.GetComponent<BinderLabel>().HierarchyPath))
                    {
                        assistant.HasError = true;
                    }
                }

                // Debug.Log(assistant.name + "执行了查找");
                if (assistant.HasError)
                {
                    Debug.LogWarning(assistant.name + "Binder Assistant 发现绑定错误，请重新生成绑定信息单元");
                }
            }
        }

        [DidReloadScripts]
        static void AttachToGameObject()
        {
            if (!EditorPrefs.HasKey("即将绑定脚本的物体 Id"))
            {
                return;
            }

            var targetObj = EditorUtility.InstanceIDToObject(EditorPrefs.GetInt("即将绑定脚本的物体 Id")) as GameObject;
            if (!targetObj)
            {
                EditorPrefs.DeleteKey("即将绑定脚本的物体 Id");
                EditorPrefs.DeleteKey("即将绑定的脚本类型");
                return;
            }

            var scriptType = Type.GetType(EditorPrefs.GetString("即将绑定的脚本类型"));
            if (scriptType == null)
            {
                Debug.LogError("即将绑定的脚本类型为空");
                return;
            }

            if (!targetObj.GetComponent(scriptType))
            {
                targetObj.AddComponent(scriptType);
            }

            var script = targetObj.GetComponent(scriptType);
            if (script is IBindReferences bindReferences)
            {
                bindReferences.BindReferences();
            }

            Selection.activeObject = targetObj;
            // EditorApplication.ExecuteMenuItem("File/Save");
            EditorPrefs.DeleteKey("即将绑定脚本的物体 Id");
            EditorPrefs.DeleteKey("即将绑定的脚本类型");
        }
#endif
    }
}
