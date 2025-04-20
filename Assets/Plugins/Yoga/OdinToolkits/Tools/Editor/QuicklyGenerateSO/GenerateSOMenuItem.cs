using System.IO;
using UnityEditor;
using UnityEngine;

namespace YOGA.OdinToolkits.Tools.Editor.GenerateSO
{
    /// <summary>
    /// 快速生成 ScriptableObject 资源
    /// </summary>
    public static class GenerateSOMenuItem
    {
        // 检查是否可以创建ScriptableObject资源
        [MenuItem("Assets/Create SO Asset From Selected", true)]
        static bool CanCreateScriptableObjectFromSelected()
        {
            var selectedObject = Selection.activeObject;
            if (selectedObject == null)
            {
                return false;
            }

            // 处理多选情况，只要有一个满足，就可以执行这个方法
            foreach (var obj in Selection.objects)
            {
                if (obj is not MonoScript script)
                {
                    continue;
                }

                var scriptClass = script.GetClass();
                if (scriptClass == null)
                {
                    continue;
                }

                // 判断脚本对应的类是否是非抽象的 ScriptableObject 子类
                if (!scriptClass.IsAbstract &&
                    scriptClass.IsSubclassOf(typeof(ScriptableObject)))
                {
                    return true;
                }
            }

            return false;
        }

        [MenuItem("Assets/Create SO Asset From Selected")]
        static void CreateScriptableObjectFromSelected()
        {
            // 分别处理多选和单选
            if (Selection.objects.Length == 1)
            {
                if (Selection.activeObject is not MonoScript script)
                {
                    return;
                }

                // 创建 ScriptableObject 实例并保存为资源
                var instance = ScriptableObject.CreateInstance(script.GetClass());
                ProjectWindowUtil.CreateAsset(instance,
                    $"{script.name}.asset");
                Selection.activeObject = instance;
            }
            else
            {
                // 处理多选情况，为每个选中的脚本生成对应的 ScriptableObject 资源
                foreach (var guid in Selection.assetGUIDs)
                {
                    var objAssetPath = AssetDatabase.GUIDToAssetPath(guid);
                    var obj = AssetDatabase.LoadAssetAtPath<Object>(objAssetPath);
                    if (obj is not MonoScript script)
                    {
                        continue;
                    }

                    var scriptClass = script.GetClass();
                    if (scriptClass == null)
                    {
                        continue;
                    }

                    // 生成资源路径并确保路径唯一
                    if (Path.GetExtension(objAssetPath) != "")
                    {
                        objAssetPath = Path.GetDirectoryName(objAssetPath);
                    }

                    var assetPath = AssetDatabase.GenerateUniqueAssetPath($"{objAssetPath}/{script.name}.asset");
                    AssetDatabase.CreateAsset(ScriptableObject.CreateInstance(scriptClass), assetPath);
                    AssetDatabase.SaveAssets();
                    AssetDatabase.Refresh();
                    Debug.Log("生成一个 SO 资源，路径为: " + assetPath);
                }
            }
        }
    }
}
