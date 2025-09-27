using Yuumix.OdinToolkits.Core;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    /// <summary>
    /// 右键快捷处理 ChineseSummary 特性
    /// </summary>
    [Summary("右键快捷处理 ChineseSummary 特性")]
    public static class ChineseSummaryMenuItems
    {
        const string ADD_MENU_NAME = "Assets/Add ChineseSummary";
        const string REMOVE_MENU_NAME = "Assets/Remove ChineseSummary";

        [MenuItem(ADD_MENU_NAME)]
        public static void QuickAddChineseSummary()
        {
            if (Selection.objects.Length == 1)
            {
                SummaryAttributeConverter.WriteSyncChineseSummaryText(
                    AssetDatabase.GetAssetPath(Selection.activeObject));
            }
            else
            {
                foreach (Object obj in Selection.objects)
                {
                    SummaryAttributeConverter.WriteSyncChineseSummaryText(
                        AssetDatabase.GetAssetPath(obj));
                }
            }
        }

        [MenuItem(REMOVE_MENU_NAME)]
        public static void QuickRemoveChineseSummary()
        {
            if (Selection.objects.Length == 1)
            {
                SummaryAttributeConverter.WriteRemoveChineseSummaryText(
                    AssetDatabase.GetAssetPath(Selection.activeObject));
            }
            else
            {
                foreach (Object obj in Selection.objects)
                {
                    SummaryAttributeConverter.WriteRemoveChineseSummaryText(
                        AssetDatabase.GetAssetPath(obj));
                }
            }
        }

        [MenuItem(ADD_MENU_NAME, true)]
        static bool CanAddBilingualComment() => IsMonoScript();

        [MenuItem(REMOVE_MENU_NAME, true)]
        static bool CanRemoveBilingualComment() => IsMonoScript();

        static bool IsMonoScript()
        {
            Object selectedObject = Selection.activeObject;
            return selectedObject && Selection.objects.All(obj => obj is MonoScript);
        }
    }
}
