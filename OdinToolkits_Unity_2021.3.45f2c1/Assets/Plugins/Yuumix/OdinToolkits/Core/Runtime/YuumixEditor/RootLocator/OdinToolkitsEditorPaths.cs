#if UNITY_EDITOR
using UnityEditor;
using Yuumix.OdinToolkits.Core;

namespace YuumixEditor
{
    /// <summary>
    /// 编辑器阶段使用的 OdinToolkits 文件夹路径定位器
    /// </summary>
    public static class OdinToolkitsEditorPaths
    {
        /// <summary>
        /// OdinToolkits 生成的相关内容的根文件夹路径，"Assets/OdinToolkitsData"，参考 AddressableData
        /// </summary>
        public const string ODIN_TOOLKITS_ANY_DATA_ROOT_FOLDER = "Assets/OdinToolkitsData";

        const string ROOT_FOLDER_NAME = "OdinToolkits";
        const string ODIN_TOOLKITS_ROOT_PATH_KEY = "ODIN_TOOLKITS_ROOT_PATH_KEY";

        /// <summary>
        /// OdinToolkits 相对路径，"Assets/.../Odin Toolkits"
        /// </summary>
        static string _odinToolkitsRootFolderPath;

        static string _lookupSOPath;

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

        /// <summary>
        /// 获取 OdinToolkits 文件夹相对路径，默认为 "Assets/Plugins/Yuumix/OdinToolkits"
        /// </summary>
        public static string GetRootFolderPath()
        {
            if (EditorPrefs.HasKey("OdinToolkitsRootPathKey"))
            {
                var folderPath = EditorPrefs.GetString(ODIN_TOOLKITS_ROOT_PATH_KEY);
                if (AssetDatabase.IsValidFolder(folderPath))
                {
                    return folderPath;
                }
            }

            SetFolderPath();
            return EditorPrefs.GetString(ODIN_TOOLKITS_ROOT_PATH_KEY);
        }

        /// <summary>
        /// 获取 Yuumix 文件夹相对路径，默认为 "Assets/Plugins/Yuumix"
        /// </summary>
        public static string GetYuumixRootPath() => GetRootFolderPath().Replace("/" + ROOT_FOLDER_NAME, string.Empty);

        static void SetFolderPath()
        {
            _lookupSOPath = ScriptableObjectEditorUtility.GetAssetPath<OdinToolkitsLookup>();
            if (string.IsNullOrEmpty(_lookupSOPath))
            {
                YuumixLogger.LogWarning("没有找到 OdinToolkitsLookup Asset，无法定位 OdinToolkits 文件夹路径。");
                return;
            }

            _odinToolkitsRootFolderPath = GetSubPathByEndsWith(_lookupSOPath, ROOT_FOLDER_NAME);
            EditorPrefs.SetString(ODIN_TOOLKITS_ROOT_PATH_KEY, _odinToolkitsRootFolderPath);
            return;

            string GetSubPathByEndsWith(string fullPath, string endWithString)
            {
                if (string.IsNullOrEmpty(fullPath) || string.IsNullOrEmpty(endWithString))
                {
                    YuumixLogger.LogError("路径或目标字符串不能为空！");
                    return null;
                }

                if (!fullPath.StartsWith("Assets"))
                {
                    YuumixLogger.LogError("完整路径不是以 Assets 开头的，需要使用相对路径。");
                    return null;
                }

                // 分割路径
                var parts = fullPath.Split('/');
                var lastIndex = -1;

                // 遍历查找最后一个匹配的索引
                for (var i = 0; i < parts.Length; i++)
                {
                    if (parts[i] == endWithString)
                    {
                        lastIndex = i;
                    }
                }

                // 未找到匹配项
                if (lastIndex != -1)
                {
                    return string.Join("/", parts, 0, lastIndex + 1);
                }

                YuumixLogger.LogError("路径中未找到以 " + endWithString + " 结尾的部分: " + fullPath);
                return null;
            }
        }
    }
}
#endif
