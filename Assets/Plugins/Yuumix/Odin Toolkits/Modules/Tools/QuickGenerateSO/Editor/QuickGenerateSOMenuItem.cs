using System.IO;
using UnityEditor;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.Tools.QuickGenerateSO.Editor
{
    public static class QuickGenerateSOMenuItem
    {
        const string MenuName = "Assets/Create SO Asset From Selected";

        [MenuItem(MenuName, true)]
        static bool CanCreateScriptableObjectFromSelected()
        {
            var selectedObject = Selection.activeObject;
            if (selectedObject == null)
            {
                return false;
            }

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

                if (!scriptClass.IsAbstract &&
                    scriptClass.IsSubclassOf(typeof(ScriptableObject)))
                {
                    return true;
                }
            }

            return false;
        }

        [MenuItem(MenuName)]
        static void CreateScriptableObjectFromSelected()
        {
            if (Selection.objects.Length == 1)
            {
                SingleSelectCreateSO();
            }
            else
            {
                MultiSelectCreateSO();
            }
        }

        static void SingleSelectCreateSO()
        {
            if (Selection.activeObject is not MonoScript script)
            {
                return;
            }

            var instance = ScriptableObject.CreateInstance(script.GetClass());

            var defaultName = script.name;
            if (defaultName.EndsWith("SO"))
            {
                defaultName = defaultName[..^2];
            }

            ProjectWindowUtil.CreateAsset(instance, $"{defaultName}.asset");
            Selection.activeObject = instance;
        }

        static void MultiSelectCreateSO()
        {
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

                if (!scriptClass.IsSubclassOf(typeof(ScriptableObject)) || scriptClass.IsAbstract)
                {
                    continue;
                }

                if (Path.GetExtension(objAssetPath) != "")
                {
                    objAssetPath = Path.GetDirectoryName(objAssetPath);
                }

                var defaultName = script.name;
                if (defaultName.EndsWith("SO"))
                {
                    defaultName = defaultName[..^2];
                }

                var assetPath = AssetDatabase.GenerateUniqueAssetPath($"{objAssetPath}/{defaultName}.asset");
                AssetDatabase.CreateAsset(ScriptableObject.CreateInstance(scriptClass), assetPath);
                AssetDatabase.SaveAssets();
                Debug.Log("生成一个 SO 资源，路径为: " + assetPath);
            }

            AssetDatabase.Refresh();
        }
    }
}
