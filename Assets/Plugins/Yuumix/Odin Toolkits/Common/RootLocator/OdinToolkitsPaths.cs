using UnityEngine;
using Yuumix.OdinToolkits.Common.Logger;
using Yuumix.OdinToolkits.Modules.Utilities;

#if UNITY_EDITOR
using UnityEditor;
using Yuumix.YuumixEditor;
#endif

namespace Yuumix.OdinToolkits.Common.RootLocator
{
    /// <summary>
    /// 编辑器阶段使用的 OdinToolkits 文件夹路径定位器
    /// </summary>
    public static class OdinToolkitsPaths
    {
        public const string OdinToolkitsAnyDataRootFolder = "Assets/OdinToolkitsData";
        const string RootFolderName = "Odin Toolkits";
        const string OdinToolkitsRootPathKey = "OdinToolkitsRootPath";

        /// <summary>
        /// OdinToolkits 相对路径，"Assets/.../OdinToolkits"
        /// </summary>
        static string _odinToolkitsFolderPath;

        static string _markerSOPath;

        static OdinToolkitsPaths()
        {
            Initialize();
        }

        static void Initialize()
        {
#if UNITY_EDITOR
            EditorApplication.projectChanged -= SetFolderPath;
            EditorApplication.projectChanged += SetFolderPath;
            SetFolderPath();
#endif
        }

        public static string GetRootPath()
        {
#if UNITY_EDITOR
            if (EditorPrefs.HasKey("OdinToolkitsRootPathKey"))
            {
                var folderPath = EditorPrefs.GetString(OdinToolkitsRootPathKey);
                if (AssetDatabase.IsValidFolder(folderPath))
                {
                    return folderPath;
                }
            }

            SetFolderPath();
            return EditorPrefs.GetString(OdinToolkitsRootPathKey);
#else
            Debug.LogWarning("[OdinToolkitsPaths.GetRootPath()] 方法仅用于编辑器阶段，运行时调用无效");
            return "仅用于编辑器阶段方法，运行时调用无效";
#endif
        }

#if UNITY_EDITOR
        static void SetFolderPath()
        {
            _markerSOPath = ScriptableObjectEditorUtil.GetAssetPath<OdinToolkitsLookup>();
            if (string.IsNullOrEmpty(_markerSOPath))
            {
                YuumixLogger.EditorLogWarning("没有找到 OdinToolkitsLookup 资源");
                return;
            }

            _odinToolkitsFolderPath = PathUtil.GetSubPathByEndsWith(_markerSOPath, RootFolderName);
            // Debug.Log("OdinToolkits 文件夹相对路径为：" + _odinToolkitsFolderPath);
            EditorPrefs.SetString(OdinToolkitsRootPathKey, _odinToolkitsFolderPath);
        }
#endif
    }
}
