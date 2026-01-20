#if UNITY_EDITOR
using UnityEditor;
#endif

namespace Yuumix.OdinToolkits.Core.SafeEditor
{
    [Summary("关于 OdinToolkits 编辑器阶段的路径工具集。用于提供 OdinToolkits 相关的文件夹路径。")]
    public static class OdinToolkitsEditorPaths
    {
        [Summary("OdinToolkits 生成的内容的根文件夹路径，Assets/OdinToolkitsData")]
        public const string ALL_DATA_ROOT_FOLDER = "Assets/OdinToolkitsData";

        const string ROOT_FOLDER_NAME = "OdinToolkits";
        const string ODIN_TOOLKITS_ROOT_PATH_KEY = "ODIN_TOOLKITS_ROOT_PATH_KEY";

        [Summary("OdinToolkits 相对路径，Assets/.../OdinToolkits")]
        static string _odinToolkitsRootFolderPath;

        static string _lookupSOPath;

        [Summary(
            "获取 OdinToolkits 文件夹相对路径，默认为 Assets/Plugins/Yuumix/OdinToolkits。仅在编辑器环境下有效，打包后返回 string.Empty。")]
        public static string GetRootFolderPath()
        {
#if UNITY_EDITOR

            if (EditorPrefs.HasKey(ODIN_TOOLKITS_ROOT_PATH_KEY))
            {
                var folderPath = EditorPrefs.GetString(ODIN_TOOLKITS_ROOT_PATH_KEY);
                if (AssetDatabase.IsValidFolder(folderPath))
                {
                    return folderPath;
                }
            }

            SetFolderPath();
            return EditorPrefs.GetString(ODIN_TOOLKITS_ROOT_PATH_KEY);
#else
            return string.Empty;
#endif
        }

        [Summary("获取 Yuumix 文件夹相对路径，默认为 Assets/Plugins/Yuumix。仅在编辑器环境下有效，打包后返回 string.Empty。")]
        public static string GetYuumixRootPath() => GetRootFolderPath()
            .Replace("/" + ROOT_FOLDER_NAME, string.Empty);

        static void SetFolderPath()
        {
            _lookupSOPath = ScriptableObjectSafeEditorUtility
                .GetSingletonAssetPathAndDeleteOther<OdinToolkitsLookup>();
            if (string.IsNullOrWhiteSpace(_lookupSOPath))
            {
                YuumixLogger.LogWarning("没有找到 OdinToolkitsLookup Asset，无法定位 OdinToolkits 文件夹路径。");
                return;
            }
#if UNITY_EDITOR
            if (PathUtility.TryGetSubPathWithSpecialEnd(_lookupSOPath, ROOT_FOLDER_NAME,
                    out _odinToolkitsRootFolderPath))
            {
                EditorPrefs.SetString(ODIN_TOOLKITS_ROOT_PATH_KEY, _odinToolkitsRootFolderPath);
            }
#endif
        }
#if UNITY_EDITOR
        static OdinToolkitsEditorPaths()
        {
            Initialize();
        }

        static void Initialize()
        {
            EditorApplication.projectChanged -= SetFolderPath;
            EditorApplication.projectChanged += SetFolderPath;
            SetFolderPath();
        }
#endif
    }
}
