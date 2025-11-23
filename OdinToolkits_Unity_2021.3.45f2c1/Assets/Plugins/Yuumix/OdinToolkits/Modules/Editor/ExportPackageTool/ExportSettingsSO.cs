using Sirenix.OdinInspector;
using System.Collections.Generic;
using UnityEngine;
using Yuumix.OdinToolkits.Core;
using YuumixEditor;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class ExportSettingsSO : ScriptableObject, IOdinToolkitsEditorReset
    {
        const string DEFAULT_EXPORT_FOLDER_NAME =
            OdinToolkitsEditorPaths.ODIN_TOOLKITS_ANY_DATA_ROOT_FOLDER + "/ExportPackages";

        #region IOdinToolkitsEditorReset Members

        public virtual void EditorReset()
        {
            packageName = string.Empty;
            version = string.Empty;
            exportFolderPath = DEFAULT_EXPORT_FOLDER_NAME;
            includeDependencies = false;
            overwriteExistingPackage = false;
            autoOpenFolderAfterExport = true;
            filePaths?.Clear();
            folderPaths?.Clear();
            exportPathsFilterRule = null;
        }

        #endregion

        /// <summary>
        /// 在导出前的重置操作接口，用于自定义导出前的操作
        /// </summary>
        [Summary("在导出前的重置操作接口，用于自定义导出前的操作")]
        public virtual void BeforeExportReset() { }

        #region Serialized Fields

        [BilingualTitle("资源包名称", "Package Name")]
        [HideLabel]
        [Required]
        public string packageName;

        [BilingualTitle("版本号", "Version")]
        [HideLabel]
        [Required]
        public string version;

        [BilingualTitle("导出目标文件夹路径", "Export Folder Path")]
        [HideLabel]
        [FolderPath]
        [Required]
        public string exportFolderPath;

        [PropertySpace(10)]
        [BilingualText("是否包含依赖项", "Include Dependencies")]
        public bool includeDependencies;

        [BilingualText("覆盖已存在的包", "Overwrite Existing Package")]
        public bool overwriteExistingPackage;

        [BilingualText("导出后打开文件夹", "Open Folder After Export")]
        public bool autoOpenFolderAfterExport = true;

        [Space(10)]
        [BilingualText("包含的文件路径列表", "Included File Paths")]
        [FilePath]
        public List<string> filePaths;

        [BilingualText("包含的文件夹路径列表", "Included Folder Paths")]
        [FolderPath]
        public List<string> folderPaths;

        [BilingualTitle("导出文件路径过滤规则", "Export File Paths Filter Rule")]
        [HideLabel]
        [SerializeReference]
        public ExportPathsFilterRule exportPathsFilterRule;

        #endregion
    }
}
