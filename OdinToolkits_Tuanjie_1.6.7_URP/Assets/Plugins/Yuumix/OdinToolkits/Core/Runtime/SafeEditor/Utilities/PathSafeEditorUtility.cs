using System.Diagnostics;
using System.IO;
using System.Linq;
using Sirenix.Utilities;
#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Yuumix.OdinToolkits.Core.SafeEditor
{
    [Summary("关于 Path 路径的工具类。仅在编辑器阶段可用。")]
    public static class PathSafeEditorUtility
    {
#if UNITY_EDITOR
        [Summary("根据相对路径，递归创建 Assets 下的文件夹。仅在编辑器阶段可用，打包自动剔除。")]
        [Conditional("UNITY_EDITOR")]
        public static void CreateDirectoryRecursivelyInAssets(string relativePath)
        {
            var pathWithoutAssets = relativePath.Replace("Assets/", "");
            var folders = pathWithoutAssets.Split('/');
            var currentPath = "Assets";
            foreach (var folder in folders)
            {
                currentPath = PathUtilities.Combine(currentPath, folder);
                if (!AssetDatabase.IsValidFolder(currentPath))
                {
                    var parentPath = Path.GetDirectoryName(currentPath);
                    var folderName = Path.GetFileName(currentPath);
                    AssetDatabase.CreateFolder(parentPath, folderName);
                }
            }

            AssetDatabase.Refresh();
        }
#endif

        [Summary("获取以目标文件夹名称结尾且路径中包含所有指定的文件夹名称的相对路径。仅在编辑器阶段可用，打包后返回 string.Empty")]
        public static string GetTargetFolderPath(string targetFolderName, params string[] containFolderName)
        {
#if UNITY_EDITOR
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

            YuumixLogger.LogWarning("在项目中没有找到 " + targetFolderName + " 文件夹，请检查是否改名，或者条件不满足");
            return string.Empty;
#else
            return string.Empty;
#endif
        }
    }
}
