using UnityEditor;
using UnityEngine;
using YOGA.OdinToolkits.Common.Runtime;

namespace YOGA.OdinToolkits.Config.Editor
{
    public static class OdinToolkitsPaths
    {
        [Tooltip("根节点文件夹名称")]
        const string RootFolderName = "OdinToolkits";

        /// <summary>
        /// Odin Toolkits 相对路径，"Assets/.../Odin Toolkits"
        /// </summary>
        static string _odinToolkitsFolderPath;

        public static string GetRootPath()
        {
            if (string.IsNullOrEmpty(_odinToolkitsFolderPath))
            {
                SetFolderPath();
            }

            return _odinToolkitsFolderPath;
        }

        static OdinToolkitsPaths()
        {
            SetFolderPath();
            EditorApplication.projectChanged -= SetFolderPath;
            EditorApplication.projectChanged += SetFolderPath;
        }

        static void SetFolderPath()
        {
            _odinToolkitsFolderPath = GetRootPathWithAssetDatabase();
        }

        static string GetRootPathWithAssetDatabase()
        {
            var paths = AssetDatabase.GetAllAssetPaths();
            foreach (var path in paths)
            {
                if (path.EndsWith(RootFolderName))
                {
                    return path;
                }
            }

            OdinLog.Error("在项目中没有找到 " + RootFolderName + " 文件夹，请检查是否改名");
            return string.Empty;
        }
    }
}
