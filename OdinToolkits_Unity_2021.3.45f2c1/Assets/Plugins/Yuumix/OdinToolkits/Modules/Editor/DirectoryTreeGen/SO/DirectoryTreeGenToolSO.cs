using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.IO;
using UnityEngine;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Core.Editor;
using YuumixEditor;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class DirectoryTreeGenToolSO : OdinEditorScriptableSingleton<DirectoryTreeGenToolSO>,
        IOdinToolkitsEditorReset
    {
        public static BilingualData DirectoryTreeGenToolMenuPathData =
            new BilingualData("目录树生成工具", "Directory Tree Generate Tool");

        public static string[] FileExtensionsFilterArray =
        {
            ".meta",
            ".asmdef"
        };

        #region Serialized Fields

        public BilingualHeaderWidget headerWidget =
            new BilingualHeaderWidget(DirectoryTreeGenToolMenuPathData.GetChinese(),
                DirectoryTreeGenToolMenuPathData.GetEnglish(),
                "根据文件夹路径，解析其子路径文件夹及文件，生成不同种类的目录树",
                "Based on the folder path, parse the sub-path folders and files within it, " +
                "and generate different types of directory trees.");

        [FolderPath(RequireExistingPath = true, AbsolutePath = true)]
        [BilingualTitle("选择的文件夹路径", "Folder Path")]
        [HideLabel]
        [CustomContextMenu("Odin Toolkits Reset", "EditorReset")]
        public string folderPath;

        [BilingualTitle("结果显示的最大深度", "Result Max Depth")]
        [HideLabel]
        [MinValue(1)]
        [CustomContextMenu("OdinToolkitsReset", "EditorReset")]
        public int maxDepth;

        [BilingualTitle("生成命令选择", "Generate Command Selector")]
        [HideLabel]
        public GenerateCommandSO command;

        BilingualData _operatorButtonsTitleGroupData = new BilingualData("操作按钮", "Operator Buttons");

        [PropertyOrder(50)]
        [TextArea(3, 15)]
        [BilingualTitle("结果", "Result")]
        [HideLabel]
        public string resultText;

        [PropertyOrder(100)]
        [BilingualTitle("过程数据", "Process Data")]
        [ReadOnly]
        [OdinSerialize]
        DirectoryAnalysisData _directoryAnalysisData;

        #endregion

        #region IOdinToolkitsEditorReset Members

        public void EditorReset()
        {
            folderPath = null;
            maxDepth = 1;
            resultText = null;
            command = null;
            _directoryAnalysisData = null;
        }

        #endregion

        [BilingualTitleGroup("TG", "操作按钮", "Operator Buttons")]
        [HorizontalGroup("TG/B")]
        [BilingualButton("解析路径", "Analyze Folder Path", ButtonSizes.Large)]
        public void Analyze()
        {
            if (!Directory.Exists(folderPath))
            {
                YuumixLogger.EditorLogError("路径不存在");
                return;
            }

            DirectoryAnalysisData.CurrentRootPath = folderPath;
            _directoryAnalysisData = DirectoryAnalysisData.FromDirectoryInfo(new DirectoryInfo(folderPath));
        }

        [BilingualTitleGroup("TG", "操作按钮", "Operator Buttons")]
        [HorizontalGroup("TG/B")]
        [BilingualButton("执行生成命令", "Execute Generate Command", ButtonSizes.Large)]
        public void GenerateProjectStructureTree()
        {
            if (command)
            {
                resultText = command.Generate(_directoryAnalysisData, maxDepth);
            }
        }
    }
}
