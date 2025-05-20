using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Modules.Utilities.YuumiEditor;

namespace Yuumix.OdinToolkits.Common.Editor.Locator
{
    public static class OdinToolkitsPaths
    {
        [Tooltip("根节点文件夹名称")]
        private const string RootFolderName = "OdinToolkits";

        /// <summary>
        /// OdinToolkits 相对路径，"Assets/.../OdinToolkits"
        /// </summary>
        private static string _odinToolkitsFolderPath;

        private static string _markerSOPath;

        static OdinToolkitsPaths()
        {
            SetFolderPath();
            EditorApplication.projectChanged -= SetFolderPath;
            EditorApplication.projectChanged += SetFolderPath;
        }

        public static string GetRootPath()
        {
            SetFolderPath();
            return _odinToolkitsFolderPath;
        }

        private static void SetFolderPath()
        {
            _markerSOPath = ProjectEditorUtility.SO.GetScriptableObjectAssetPath<OdinToolkitsLookup>();
            Debug.Log("MarkerSOPath:" + _markerSOPath);
            _odinToolkitsFolderPath = ProjectEditorUtility.Paths.GetSubPathByEndsWith(_markerSOPath, RootFolderName);
            Debug.Log("FolderPath:" + _odinToolkitsFolderPath);
        }
    }
}
