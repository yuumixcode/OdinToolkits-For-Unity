#if UNITY_EDITOR
using UnityEditor;
#endif
using System;
using System.IO;
using System.Linq;
using UnityEngine;
using YOGA.OdinToolkits.Common.Runtime;
using Object = UnityEngine.Object;

namespace YOGA.Modules.Utilities
{
    /// <summary>
    /// ProjectEditorUtility 仅限编辑器使用
    /// </summary>
    public static class ProjectEditorUtility
    {
        public static class SO
        {
#if UNITY_EDITOR

            /// <summary>
            /// 是否存在该类型的 SO 资源
            /// </summary>
            /// <param name="type"></param>
            /// <returns></returns>
            public static bool IsExistScriptableObjectAsset(Type type)
            {
                var assetPaths = AssetDatabase.FindAssets("t:" + type);
                return assetPaths.Length > 0;
            }

            /// <summary>
            /// 根据类型获取单个资源的路径，更加通用，非脚本类型
            /// </summary>
            /// <typeparam name="T"> 资源类型 </typeparam>
            /// <returns> 字符串路径 </returns>
            public static string GetScriptableObjectAssetPath<T>() where T : ScriptableObject
            {
                var assetPath = AssetDatabase.FindAssets("t:" + typeof(T))
                    .Select(AssetDatabase.GUIDToAssetPath)
                    .FirstOrDefault();
                if (!string.IsNullOrEmpty(assetPath))
                {
                    return assetPath;
                }

                OdinLog.Error("没有找到对应类型的 ScriptableObject 文件，需要手动生成对应的文件");
                return null;
            }

            /// <summary>
            /// 获取或创建一个单例 SO 资源，如果资源不存在则创建，如果有多个 SO 资源，则只返回第一个，并删除其他资源
            /// </summary>
            public static T GetScriptableObjectDeleteExtra<T>(string filePath = "") where T : ScriptableObject
            {
                T wantToAsset;
                var guids = AssetDatabase.FindAssets("t:" + typeof(T));
                var allPaths = new string[guids.Length];
                if (guids.Length > 0)
                {
                    allPaths[0] = AssetDatabase.GUIDToAssetPath(guids[0]);

                    // 只获取一个资源 0 号
                    wantToAsset = AssetDatabase.LoadAssetAtPath<T>(allPaths[0]);
                    if (wantToAsset == null)
                    {
                        Debug.LogWarning("GetScriptableObjectDeleteExtra 中加载资源失败");
                    }

                    // 删除从序号 1 开始的所有资源
                    for (var i = 1; i < guids.Length; i++)
                    {
                        // 能获得扩展名
                        allPaths[i] = AssetDatabase.GUIDToAssetPath(guids[i]);
                        AssetDatabase.DeleteAsset(allPaths[i]);
                    }

                    AssetDatabase.Refresh();
                    return wantToAsset;
                }

                if (string.IsNullOrEmpty(filePath))
                {
                    OdinLog.Warning("没有找到对应的 ScriptableObject 资源，且没有设置路径，不会立即生成新资源");
                    return null;
                }

                wantToAsset = ScriptableObject.CreateInstance<T>();
                if (!filePath.EndsWith(".asset"))
                {
                    filePath = Path.Combine(filePath, ".asset");
                }

                AssetDatabase.CreateAsset(wantToAsset, filePath);
                AssetDatabase.ImportAsset(filePath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                return wantToAsset;
            }
#endif
        }

        public static class Script
        {
#if UNITY_EDITOR
            /// <summary>
            /// 查找脚本，并选择这个脚本文件
            /// 注意：查找的是 MonoScript，而不是 ScriptableObject，加载的也是 MonoScript
            /// </summary>
            public static MonoScript FindAndSelectedScript(string scriptName)
            {
                MonoScript foundMonoScript = null;
                var scriptAssetPath = AssetDatabase.FindAssets("t:MonoScript " + scriptName)
                    .Select(AssetDatabase.GUIDToAssetPath)
                    .FirstOrDefault();

                if (!string.IsNullOrEmpty(scriptAssetPath))
                {
                    foundMonoScript = AssetDatabase.LoadAssetAtPath<MonoScript>(scriptAssetPath);
                }

                if (foundMonoScript)
                {
                    Selection.activeObject = foundMonoScript;
                }

                return foundMonoScript;
            }
#endif
        }

        public static class Paths
        {
            /// <summary>
            /// Combines two paths 替换所有反斜杠为正斜杠，Window 系统资源管理器是 '\', MacOS 系统是 '/'
            /// </summary>
            public static string CombinePath(string a, string b)
            {
                a = a.Replace("\\", "/").TrimEnd('/');
                b = b.Replace("\\", "/").TrimStart('/');
                return a + "/" + b;
            }

            /// <summary>
            /// 递归创建 Assets 下的文件夹路径
            /// </summary>
            /// <param name="relativePath">以 Assets 开头的相对路径</param>
            public static void EnsureFolderRecursively(string relativePath)
            {
                // 移除 Assets 前缀，获取实际的文件夹路径
                var pathWithoutAssets = relativePath.Replace("Assets/", "");
                var folders = pathWithoutAssets.Split('/');
                var currentPath = "Assets";
                foreach (var folder in folders)
                {
                    currentPath = CombinePath(currentPath, folder);
                    if (!AssetDatabase.IsValidFolder(currentPath))
                    {
                        var parentPath = Path.GetDirectoryName(currentPath);
                        var folderName = Path.GetFileName(currentPath);
                        AssetDatabase.CreateFolder(parentPath, folderName);
                    }
                }

                // 刷新 AssetDatabase 使创建的文件夹在 Unity 编辑器中可见
                AssetDatabase.Refresh();
            }
        }
#if UNITY_EDITOR
        /// <summary>
        /// Ping 项目中的任何资源，需要相对路径
        /// </summary>
        /// <param name="path">相对路径</param>
        public static void PingAndSelectAsset(string path)
        {
            Debug.Assert(path.StartsWith("Assets"), "PingFolder 中传入的相对路径必须以 Assets 开头");
            if (!path.StartsWith("Assets"))
            {
                OdinLog.Error("PingFolder 中传入的相对路径必须以 Assets 开头");
            }

            var folder = AssetDatabase.LoadAssetAtPath<Object>(path);
            Selection.activeObject = folder;
            EditorGUIUtility.PingObject(folder);
        }

        /// <summary>
        /// 获取目标文件夹路径
        /// </summary>
        public static string GetTargetFolderPath(string targetFolderName, params string[] containFolderName)
        {
            var paths = AssetDatabase.GetAllAssetPaths();
            foreach (var path in paths)
            {
                // 检查路径是否不以目标文件夹名称结束，或者路径不包含所有指定的文件夹名称
                if (!path.EndsWith(targetFolderName) || !containFolderName.All(path.Contains))
                {
                    continue;
                }

                return path;
            }

            Debug.LogError("在项目中没有找到 " + targetFolderName + " 文件夹，请检查是否改名，或者条件不满足");
            return null;
        }
#endif
    }
}
