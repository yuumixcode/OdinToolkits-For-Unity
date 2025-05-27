using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Modules.Utilities.YuumiEditor;

namespace Yuumix.OdinToolkits.Common.Locator
{
    public static class OdinToolkitsPaths
    {
        const string RootFolderName = "OdinToolkits";
        const string OdinToolkitsRootPathKey = "OdinToolkitsRootPath";

        /// <summary>
        /// OdinToolkits 相对路径，"Assets/.../OdinToolkits"
        /// </summary>
        static string _odinToolkitsFolderPath;

        static string _markerSOPath;

        static OdinToolkitsPaths()
        {
#if UNITY_EDITOR
            SetFolderPath();
            EditorApplication.projectChanged -= SetFolderPath;
            EditorApplication.projectChanged += SetFolderPath;
#endif
        }

        public static string GetRootPath()
        {
            var path = PlayerPrefs.GetString(OdinToolkitsRootPathKey);
#if UNITY_EDITOR
            if (AssetDatabase.IsValidFolder(path))
            {
                return path;
            }

            SetFolderPath();
            path = PlayerPrefs.GetString(OdinToolkitsRootPathKey);
#endif
            return path;
        }
#if UNITY_EDITOR
        static void SetFolderPath()
        {
            _markerSOPath = ProjectEditorUtility.SO.GetScriptableObjectAssetPath<OdinToolkitsLookup>();
            // Debug.Log("MarkerSOPath:" + _markerSOPath);
            _odinToolkitsFolderPath = ProjectEditorUtility.Paths.GetSubPathByEndsWith(_markerSOPath, RootFolderName);
            // Debug.Log("FolderPath:" + _odinToolkitsFolderPath);
            PlayerPrefs.SetString(OdinToolkitsRootPathKey, _odinToolkitsFolderPath);
            PlayerPrefs.Save();
        }
#endif
    }
}
