#if UNITY_EDITOR
using Sirenix.Utilities;
using System.IO;
using System.Linq;
using UnityEditor;

namespace Yuumix.YuumixEditor
{
    public static class PathEditorUtil
    {
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
                currentPath = PathUtilities.Combine(currentPath, folder);
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

            YuumixEditorEasyLog.Error("在项目中没有找到 " + targetFolderName + " 文件夹，请检查是否改名，或者条件不满足");
            return null;
        }
    }
}
#endif
