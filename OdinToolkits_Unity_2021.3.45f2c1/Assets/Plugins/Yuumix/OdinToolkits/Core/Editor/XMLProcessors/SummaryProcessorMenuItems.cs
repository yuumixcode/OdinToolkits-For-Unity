using System.IO;
using System.Linq;
using UnityEditor;

namespace Yuumix.OdinToolkits.Core.Editor
{
    /// <summary>
    /// 右键快捷处理 Summary 特性
    /// </summary>
    [Summary("右键快捷处理 Summary 特性")]
    public static class SummaryProcessorMenuItems
    {
        const string ADD_MENU_NAME = "Assets/Process Summary/Sync";
        const string REPLACE_MENU_NAME = "Assets/Process Summary/Replace";
        const string REMOVE_MENU_NAME = "Assets/Process Summary/Remove";

        [MenuItem(ADD_MENU_NAME, false, 1000)]
        public static void QuickSyncSummary()
        {
            if (Selection.objects.Length == 1)
            {
                WriteSyncSummary(AssetDatabase.GetAssetPath(Selection.activeObject));
            }
            else
            {
                foreach (var obj in Selection.objects)
                {
                    WriteSyncSummary(AssetDatabase.GetAssetPath(obj));
                }
            }
        }

        [MenuItem(REPLACE_MENU_NAME, false, 1001)]
        public static void QuickReplaceSummary()
        {
            if (Selection.objects.Length == 1)
            {
                WriteReplaceSummary(AssetDatabase.GetAssetPath(Selection.activeObject));
            }
            else
            {
                foreach (var obj in Selection.objects)
                {
                    WriteReplaceSummary(AssetDatabase.GetAssetPath(obj));
                }
            }
        }

        [MenuItem(REMOVE_MENU_NAME, false, 1002)]
        public static void QuickRemoveSummary()
        {
            if (Selection.objects.Length == 1)
            {
                WriteRemoveSummary(AssetDatabase.GetAssetPath(Selection.activeObject));
            }
            else
            {
                foreach (var obj in Selection.objects)
                {
                    WriteRemoveSummary(AssetDatabase.GetAssetPath(obj));
                }
            }
        }

        [MenuItem(ADD_MENU_NAME, true)]
        static bool CanAddBilingualComment() => IsMonoScript();

        [MenuItem(REPLACE_MENU_NAME, true)]
        static bool CanReplaceBilingualComment() => IsMonoScript();

        [MenuItem(REMOVE_MENU_NAME, true)]
        static bool CanRemoveBilingualComment() => IsMonoScript();

        static bool IsMonoScript()
        {
            var selectedObject = Selection.activeObject;
            return selectedObject && Selection.objects.All(obj => obj is MonoScript);
        }

        static void WriteSyncSummary(string filePath)
        {
            var sourceCode = File.ReadAllText(filePath);
            var processor = new XMLSummaryProcessor(sourceCode).ParseSourceScript();
            sourceCode = processor.GetProcessedSourceScript(XMLSummaryProcessor.ProcessMode.SyncSummary);
            File.WriteAllText(filePath, sourceCode);
            AssetDatabase.Refresh();
        }

        static void WriteReplaceSummary(string filePath)
        {
            var sourceCode = File.ReadAllText(filePath);
            var processor = new XMLSummaryProcessor(sourceCode).ParseSourceScript();
            sourceCode = processor.GetProcessedSourceScript(XMLSummaryProcessor.ProcessMode.ReplaceSummary);
            File.WriteAllText(filePath, sourceCode);
            AssetDatabase.Refresh();
        }

        static void WriteRemoveSummary(string filePath)
        {
            var sourceCode = File.ReadAllText(filePath);
            var processor = new XMLSummaryProcessor(sourceCode).ParseSourceScript();
            sourceCode = processor.GetProcessedSourceScript(XMLSummaryProcessor.ProcessMode.RemoveSummary);
            File.WriteAllText(filePath, sourceCode);
            AssetDatabase.Refresh();
        }
    }
}
