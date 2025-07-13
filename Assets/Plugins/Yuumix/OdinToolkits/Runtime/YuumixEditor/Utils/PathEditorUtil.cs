#if UNITY_EDITOR
using System.IO;
using System.Linq;
using Sirenix.Utilities;
using UnityEditor;
using Yuumix.OdinToolkits.Core;

namespace YuumixEditor
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
            string pathWithoutAssets = relativePath.Replace("Assets/", "");
            string[] folders = pathWithoutAssets.Split('/');
            var currentPath = "Assets";
            foreach (string folder in folders)
            {
                currentPath = PathUtilities.Combine(currentPath, folder);
                if (!AssetDatabase.IsValidFolder(currentPath))
                {
                    string parentPath = Path.GetDirectoryName(currentPath);
                    string folderName = Path.GetFileName(currentPath);
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
            string[] paths = AssetDatabase.GetAllAssetPaths();
            foreach (string path in paths)
            {
                // 检查路径是否不以目标文件夹名称结束，或者路径不包含所有指定的文件夹名称
                if (!path.EndsWith(targetFolderName) || !containFolderName.All(path.Contains))
                {
                    continue;
                }

                return path;
            }

            YuumixLogger.LogWarning("在项目中没有找到 " + targetFolderName + " 文件夹，请检查是否改名，或者条件不满足");
            return null;
        }
    }
}
#endif
