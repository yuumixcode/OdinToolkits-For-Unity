using Yuumix.OdinToolkits.Common;
#if UNITY_EDITOR
using YuumixEditor;
using UnityEditor;
#else
using UnityEngine;
#endif

namespace Yuumix.OdinToolkits.Core
{
    /// <summary>
    /// 编辑器阶段使用的 OdinToolkits 文件夹路径定位器
    /// </summary>
    public static class OdinToolkitsPaths
    {
        public const string ODIN_TOOLKITS_ANY_DATA_ROOT_FOLDER = "Assets/OdinToolkitsData";
        const string ROOT_FOLDER_NAME = "OdinToolkits";
        const string ODIN_TOOLKITS_ROOT_PATH_KEY = "ODIN_TOOLKITS_ROOT_PATH_KEY";

        /// <summary>
        /// OdinToolkits 相对路径，"Assets/.../Odin Toolkits"
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
                string folderPath = EditorPrefs.GetString(ODIN_TOOLKITS_ROOT_PATH_KEY);
                if (AssetDatabase.IsValidFolder(folderPath))
                {
                    return folderPath;
                }
            }

            SetFolderPath();
            return EditorPrefs.GetString(ODIN_TOOLKITS_ROOT_PATH_KEY);
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
                TempLog.Warning("没有找到 OdinToolkitsLookup 资源");
                return;
            }

            _odinToolkitsFolderPath = PathUtil.GetSubPathByEndsWith(_markerSOPath, ROOT_FOLDER_NAME);
            // Debug.Log("OdinToolkits 文件夹相对路径为：" + _odinToolkitsFolderPath);
            EditorPrefs.SetString(ODIN_TOOLKITS_ROOT_PATH_KEY, _odinToolkitsFolderPath);
        }
#endif
    }
}
