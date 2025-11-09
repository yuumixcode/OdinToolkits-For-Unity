using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Core.Editor;
using YuumixEditor;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class ExportPackageToolVisualPanelSO : OdinEditorScriptableSingleton<ExportPackageToolVisualPanelSO>,
        IOdinToolkitsEditorReset
    {
        public static BilingualData ToolMenuPathData = new BilingualData("导出包工具", "Export Package Tool");

        [PropertyOrder(-99)]
        public BilingualHeaderWidget headerWidget;

        [BilingualTitle("导出设置", "Export Setting")]
        [HideLabel]
        [InlineEditor(InlineEditorObjectFieldModes.Foldout)]
        public ExportSettingsSO exportSettings;

        /// <summary>
        /// 判断导出路径是否包含导出工具自身的脚本文件，如果包含，则需要在实际导出前重置导出设置
        /// </summary>
        static bool ExportPathsContainsSelf(List<string> paths)
        {
            return paths.Any(path => path.Contains(nameof(ExportPackageToolVisualPanelSO) + ".cs"));
        }

        void OnEnable()
        {
            headerWidget = new BilingualHeaderWidget(ToolMenuPathData.GetChinese(),
                ToolMenuPathData.GetEnglish(),
                "根据导出设置，导出指定文件夹和文件到指定路径",
                "Based on the export settings, export the specified folders and files to the specified path.");
        }

        public void EditorReset()
        {
            exportSettings = null;
        }

        [PropertySpace(10f)]
        [BilingualButton("根据设置导出包", "Export Package By Settings", ButtonSizes.Large)]
        public void ExportPackage()
        {
            if (!exportSettings)
            {
                Debug.LogError("导出设置不能为空");
                return;
            }

            if (ValidateExportSetting())
            {
                return;
            }

            // 调用导出前的重置操作
            exportSettings.BeforeExportReset();
            // 过滤排除的文件和文件夹
            if (exportSettings.exportPathsFilterRule != null)
            {
                exportSettings.filePaths = exportSettings.filePaths
                    .Where(filePath => !exportSettings.exportPathsFilterRule.IsExcludedFile(filePath)).ToList();
                exportSettings.folderPaths = exportSettings.folderPaths
                    .Where(folderPath => !exportSettings.exportPathsFilterRule.IsExcludedFolder(folderPath)).ToList();
            }

            // 合并文件路径和文件夹路径中的所有文件
            var filePaths = new List<string>(exportSettings.filePaths);
            var extensionFiles = new List<string>();
            foreach (var files in exportSettings.folderPaths.Select(folderPath =>
                         Directory.GetFiles(folderPath, "*.*", SearchOption.AllDirectories)))
            {
                if (exportSettings.exportPathsFilterRule == null)
                {
                    extensionFiles.AddRange(files);
                }
                else
                {
                    extensionFiles.AddRange(
                        files.Where(filePath => !exportSettings.exportPathsFilterRule.IsExcludedFile(filePath)));
                }
            }

            filePaths.AddRange(extensionFiles);
            var includeDependenciesCache = exportSettings.includeDependencies;
            var autoOpenFolderAfterExportCache = exportSettings.autoOpenFolderAfterExport;
            // 构建导出包的完整路径
            var fullFileName = GeneratePackageFileName(exportSettings.overwriteExistingPackage);
            var fullExportPath = Path.Combine(exportSettings.exportFolderPath, fullFileName);
            // 检查导出路径是否包含导出工具自身的脚本文件
            if (ExportPathsContainsSelf(filePaths))
            {
                Debug.LogWarning("导出路径包含导出工具自身的脚本文件，导出前会重置导出设置");
                exportSettings = null;
            }

            try
            {
                // 导出包
                AssetDatabase.ExportPackage(
                    filePaths.ToArray(),
                    fullExportPath,
                    includeDependenciesCache
                        ? ExportPackageOptions.IncludeDependencies
                        : ExportPackageOptions.Recurse
                );

                Debug.Log("成功导出文件列表到: " + fullExportPath);
                AssetDatabase.Refresh();
                if (autoOpenFolderAfterExportCache)
                {
                    EditorUtility.RevealInFinder(fullExportPath);
                }
            }
            catch (Exception e)
            {
                Debug.LogError("导出文件列表失败: " + e.Message);
            }
        }

        bool ValidateExportSetting()
        {
            if (string.IsNullOrEmpty(exportSettings.packageName))
            {
                Debug.LogError("导出包名不能为空");
                return true;
            }

            if (string.IsNullOrEmpty(exportSettings.version))
            {
                Debug.LogError("版本号不能为空");
                return true;
            }

            if (exportSettings.filePaths.Count == 0 && exportSettings.folderPaths.Count == 0)
            {
                Debug.LogError("资源包中包含的文件路径列表和文件夹路径列表不能同时为空");
                return true;
            }

            // 创建导出路径
            if (!Directory.Exists(exportSettings.exportFolderPath))
            {
                Directory.CreateDirectory(exportSettings.exportFolderPath);
                Debug.Log("创建导出路径: " + exportSettings.exportFolderPath);
            }

            return false;
        }

        string GeneratePackageFileName(bool overwrite = false)
        {
            var initialFileName = $"{exportSettings.packageName}_{exportSettings.version}.unitypackage";
            if (overwrite)
            {
                return initialFileName;
            }

            var existingFilePath = Path.Combine(exportSettings.exportFolderPath, initialFileName);
            if (File.Exists(existingFilePath))
            {
                initialFileName =
                    $"{exportSettings.packageName}_{exportSettings.version}_{DateTime.Now:yyyyMMddHHmmss}.unitypackage";
            }

            return initialFileName;
        }
    }
}
