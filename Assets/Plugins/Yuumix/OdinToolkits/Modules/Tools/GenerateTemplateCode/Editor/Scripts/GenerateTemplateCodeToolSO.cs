using Sirenix.OdinInspector;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using Yuumix.OdinToolkits.Common.Runtime;
using Yuumix.OdinToolkits.Modules.CustomExtensions.Attributes;
using Yuumix.OdinToolkits.YuumiEditor;

namespace Yuumix.OdinToolkits.Modules.Tools.GenerateTemplateCode.Editor
{
    public class GenerateTemplateCodeToolSO : SerializedScriptableObject
    {
        public const string NameSpaceSymbol = "#NAMESPACE#";
        public const string ClassNameSymbol = "#CLASSNAME#";

        [PropertyOrder(-1)]
        [Title("脚本所在命名空间")]
        [HideLabel]
        public string codeNamespace;

        [PropertyOrder(-1)]
        [Title("脚本类名")]
        [HideLabel]
        public string codeClassName;

        [PropertyOrder(-1)]
        [Title("脚本模板选择器")]
        [ValueDropdown(nameof(templateList), ExcludeExistingValuesInList = true)]
        [HideLabel]
        public string templateSelector;

        [PropertyOrder(-1)]
        [Title("生成脚本文件的文件夹路径")]
        [PropertySpace(SpaceBefore = 0, SpaceAfter = 10)]
        [ValueDropdown("TryGetPath", IsUniqueList = true, ExcludeExistingValuesInList = true)]
        [HideLabel]
        public string codeTargetPath;

        [TitleGroup("工具配置")]
        [InfoBox("脚本模板文件必须为 .txt 文本文件，修改模板路径后需要点击应用工具配置")]
        [Sirenix.OdinInspector.FilePath(IncludeFileExtension = true, RequireExistingPath = true)]
        [LabelText("脚本模板路径配置")]
        public List<string> templatePathConfig;

        [TitleGroup("工具配置")]
        [FolderPath]
        [LabelText("目标文件夹路径配置")]
        public List<string> preSavePaths;

        [TitleGroup("中间数据")]
        [LabelText("调试信息")]
        [SwitchButton]
        public bool debugInspector;

        [ShowIf(nameof(debugInspector))]
        [ReadOnly]
        [ListDrawerSettings(IsReadOnly = true)]
        public List<string> templateList;

        [ShowIf(nameof(debugInspector))]
        [ReadOnly]
        [DictionaryDrawerSettings(DisplayMode = DictionaryDisplayOptions.Foldout)]
        public Dictionary<string, string> TemplatePathMaps = new Dictionary<string, string>();

        [PropertyOrder(-1)]
        [ButtonGroup("按钮")]
        [Button("生成代码文件", ButtonSizes.Large, Icon = SdfIconType.ArrowUpSquareFill)]
        public void Generate()
        {
            GenerateCode(codeNamespace, codeClassName, codeTargetPath, templateSelector);
        }

        [PropertyOrder(-1)]
        [ButtonGroup("按钮")]
        [Button("应用工具配置", ButtonSizes.Large, Icon = SdfIconType.ArrowDownSquareFill)]
        public void ApplyConfig()
        {
            OdinEditorLog.Log("配置已应用，重新生成模板映射");
            templateList.Clear();
            TemplatePathMaps.Clear();
            var noTxtNumber = 0;
            foreach (var template in templatePathConfig)
            {
                var content = template.Split('/')[^1];
                // OdinEditorLog.Log(content);
                if (content.EndsWith(".txt"))
                {
                    var templateName = content.Replace(".txt", "");
                    if (!TemplatePathMaps.TryAdd(templateName, template))
                    {
                        OdinEditorLog.Warning("发现重复添加模板，请修改");
                    }
                    else
                    {
                        templateList.Add(templateName);
                    }
                }
                else
                {
                    if (content == string.Empty)
                    {
                        return;
                    }

                    noTxtNumber += 1;
                }
            }

            if (noTxtNumber > 0)
            {
                OdinEditorLog.Warning("存在" + noTxtNumber + "个非 .txt 类型的文件，请重新选择路径");
            }
        }

        public List<string> TryGetPath() => preSavePaths;

        void GenerateCode(string targetNamespace, string targetClassName, string targetPath,
            string targetTemplateKey)
        {
            if (string.IsNullOrEmpty(targetNamespace) || string.IsNullOrEmpty(targetClassName) ||
                string.IsNullOrEmpty(targetPath))
            {
                OdinEditorLog.Error("请填写完整工具信息！");
                return;
            }

            if (!File.Exists(targetPath))
            {
                OdinEditorLog.Error("目标文件夹路径不存在，请修改！");
            }

            TemplatePathMaps.TryGetValue(targetTemplateKey, out var templatePath);
            if (templatePath == null)
            {
                OdinEditorLog.Error("不存在这个模板，请修改模板路径配置后，点击应用配置按钮");
                return;
            }

            var absolutePath = Path.GetFullPath(templatePath);
            // Debug.Log("模板绝对路径为: " + absolutePath);
            var templateContent = File.ReadAllText(absolutePath);
            if (!templateContent.Contains(NameSpaceSymbol) || !templateContent.Contains(ClassNameSymbol))
            {
                OdinEditorLog.Error("模板中不存在 " + NameSpaceSymbol + " 或 " + ClassNameSymbol + " 占位符");
                return;
            }

            templateContent = templateContent.Replace(NameSpaceSymbol, targetNamespace);
            templateContent = templateContent.Replace(ClassNameSymbol, targetClassName);
            // Debug.Log("读取到的模板代码替换后为: " + templateContent);
            var codeRelativePath = targetPath + "/" + targetClassName + ".cs";
            var codeAbsolutePath = Path.GetFullPath(codeRelativePath);
            // Debug.Log("目标文件的绝对路径为: " + codePath);
            if (File.Exists(codeAbsolutePath))
            {
                if (!EditorUtility.DisplayDialog("生成脚本冲突", "目标文件夹内已经存在相同名称的脚本，此操作无法撤回，是否确定覆盖原脚本?",
                        "确认覆盖", "取消"))
                {
                    return;
                }
            }

            File.WriteAllText(codeAbsolutePath, templateContent);
            AssetDatabase.ImportAsset(codeRelativePath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            ProjectEditorUtil.PingAndSelectAsset(codeRelativePath);
        }
    }
}
