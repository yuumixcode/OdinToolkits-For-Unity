using System.Linq;
using UnityEngine;
#if UNITY_EDITOR
using UnityEditor;
using YuumixEditor;
#endif

namespace Yuumix.OdinToolkits.Core.SafeEditor
{
    public static class ScriptableObjectSafeEditorUtility
    {
        [Summary("获取对应类型的 SO 资源单例的相对路径。如果项目中不存在该类型的实例，则生成一个 SO 资源并保存，返回相对路径。" +
                 "如果有多个 SO 资源，则只返回第一个资源的相对路径，并删除其他 SO 资源。打包后此方法将失效，返回 string.Empty")]
        public static string GetSingletonAssetPathAndDeleteOther<T>(string relativeFolderPath = "")
            where T : ScriptableObject
        {
#if UNITY_EDITOR
            return Internal_GetSingletonAssetPathAndDeleteOther<T>(relativeFolderPath);
#else
            return string.Empty;
#endif
        }

        [Summary("获取对应类型的 SO 资源单例。如果项目中不存在该类型的实例，则生成一个 SO 资源并保存。如果有多个 SO 资源，则只返回第一个，并删除其他 SO 资源。打包后此方法将失效，返回 null。")]
        public static T GetSingletonAssetAndDeleteOther<T>(string relativeFolderPath = "") where T : ScriptableObject
        {
#if UNITY_EDITOR
            return Internal_GetSingletonAssetAndDeleteOther<T>(relativeFolderPath);
#else
            return null;
#endif
        }

#if UNITY_EDITOR
        const string RELATIVE_FOLDER_PATH =
            OdinToolkitsEditorPaths.ODIN_TOOLKITS_ANY_DATA_ROOT_FOLDER + "/Editor/SingletonSO";

        static string Internal_GetSingletonAssetPathAndDeleteOther<T>(string relativeFolderPath = "")
            where T : ScriptableObject
        {
            T singletonAsset = null;
            var targetPath = string.Empty;
            var guids = AssetDatabase.FindAssets("t:" + typeof(T));
            if (guids.Length > 0)
            {
                var allPaths = guids.Select(AssetDatabase.GUIDToAssetPath);
                foreach (var path in allPaths)
                {
                    if (!singletonAsset)
                    {
                        singletonAsset = AssetDatabase.LoadAssetAtPath<T>(path);
                        targetPath = path;
                    }
                    else
                    {
                        AssetDatabase.DeleteAsset(path);
                    }
                }

                AssetDatabase.Refresh();
                if (singletonAsset)
                {
                    return targetPath;
                }
            }

            if (string.IsNullOrWhiteSpace(relativeFolderPath))
            {
                relativeFolderPath = RELATIVE_FOLDER_PATH;
            }

            PathEditorUtility.CreateDirectoryRecursivelyInAssets(relativeFolderPath);
            singletonAsset = ScriptableObject.CreateInstance<T>();
            var fileNameWithoutExtension = typeof(T).Name.EndsWith("SO")
                ? typeof(T).Name.Remove(typeof(T).Name.Length - 2)
                : typeof(T).Name;
            var filePath = relativeFolderPath + "/[SingletonAsset]" + fileNameWithoutExtension + ".asset";
            AssetDatabase.CreateAsset(singletonAsset, filePath);
            AssetDatabase.ImportAsset(filePath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            return filePath;
        }

        static T Internal_GetSingletonAssetAndDeleteOther<T>(string relativeFolderPath = "") where T : ScriptableObject
        {
            T singletonAsset = null;
            var guids = AssetDatabase.FindAssets("t:" + typeof(T));
            if (guids.Length > 0)
            {
                var allPaths = guids.Select(AssetDatabase.GUIDToAssetPath);
                foreach (var path in allPaths)
                {
                    if (!singletonAsset)
                    {
                        singletonAsset = AssetDatabase.LoadAssetAtPath<T>(path);
                    }
                    else
                    {
                        AssetDatabase.DeleteAsset(path);
                    }
                }

                AssetDatabase.Refresh();
                return singletonAsset;
            }

            if (string.IsNullOrEmpty(relativeFolderPath))
            {
                relativeFolderPath = RELATIVE_FOLDER_PATH;
            }

            PathEditorUtility.CreateDirectoryRecursivelyInAssets(relativeFolderPath);
            singletonAsset = ScriptableObject.CreateInstance<T>();
            var fileNameWithoutExtension = typeof(T).Name.EndsWith("SO")
                ? typeof(T).Name.Remove(typeof(T).Name.Length - 2)
                : typeof(T).Name;
            var filePath = relativeFolderPath + "/[SingletonAsset]" + fileNameWithoutExtension + ".asset";
            AssetDatabase.CreateAsset(singletonAsset, filePath);
            AssetDatabase.ImportAsset(filePath);
            AssetDatabase.SaveAssets();
            AssetDatabase.Refresh();
            ProjectEditorUtility.PingAndSelectAsset(filePath);
            return singletonAsset;
        }
#endif
    }
}
