using System;
using System.IO;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Yuumix.OdinToolkits.Editor.Core
{
    public static class QuickGenerateSOMenuItem
    {
        const string MENU_NAME = "Assets/Create SO Asset From Selected";

        [MenuItem(MENU_NAME, true)]
        static bool CanCreateScriptableObjectFromSelected()
        {
            Object selectedObject = Selection.activeObject;
            if (!selectedObject)
            {
                return false;
            }

            foreach (Object obj in Selection.objects)
            {
                if (obj is not MonoScript script)
                {
                    continue;
                }

                Type scriptClass = script.GetClass();
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

        [MenuItem(MENU_NAME)]
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

            string defaultName = script.name;
            if (defaultName.EndsWith("SO"))
            {
                defaultName = defaultName[..^2];
            }

            ProjectWindowUtil.CreateAsset(instance, $"{defaultName}.asset");
            Selection.activeObject = instance;
        }

        static void MultiSelectCreateSO()
        {
            foreach (string guid in Selection.assetGUIDs)
            {
                string objAssetPath = AssetDatabase.GUIDToAssetPath(guid);
                var obj = AssetDatabase.LoadAssetAtPath<Object>(objAssetPath);
                if (obj is not MonoScript script)
                {
                    continue;
                }

                Type scriptClass = script.GetClass();
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

                string defaultName = script.name;
                if (defaultName.EndsWith("SO"))
                {
                    defaultName = defaultName[..^2];
                }

                string assetPath = AssetDatabase.GenerateUniqueAssetPath($"{objAssetPath}/{defaultName}.asset");
                AssetDatabase.CreateAsset(ScriptableObject.CreateInstance(scriptClass), assetPath);
                AssetDatabase.SaveAssets();
                Debug.Log("生成一个 SO 资源，路径为: " + assetPath);
            }

            AssetDatabase.Refresh();
        }
    }
}
