using System.Linq;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Core.Editor;
using Yuumix.OdinToolkits.Modules;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    /// <summary>
    /// 右键快捷处理 BilingualComment 特性
    /// </summary>
    [ChineseSummary("右键快捷处理 BilingualComment 特性")]
    public static class QuickHandleBilingualCommentMenuItem
    {
        const string ADD_MENU_NAME = "Assets/Add BilingualComment Attribute";
        const string REMOVE_MENU_NAME = "Assets/Remove BilingualComment Attribute";

        [MenuItem(ADD_MENU_NAME)]
        public static void QuickAddBilingualComment()
        {
            if (Selection.objects.Length == 1)
            {
                XMLSummaryConverter.InsertChineseSummaryAttribute(AssetDatabase.GetAssetPath(Selection.activeObject));
            }
            else
            {
                foreach (Object obj in Selection.objects)
                {
                    XMLSummaryConverter.InsertChineseSummaryAttribute(AssetDatabase.GetAssetPath(obj));
                }
            }
        }

        [MenuItem(REMOVE_MENU_NAME)]
        public static void QuickRemoveBilingualComment()
        {
            if (Selection.objects.Length == 1)
            {
                XMLSummaryConverter.RemoveAllBilingualCommentAttributes(
                    AssetDatabase.GetAssetPath(Selection.activeObject));
            }
            else
            {
                foreach (Object obj in Selection.objects)
                {
                    XMLSummaryConverter.RemoveAllBilingualCommentAttributes(AssetDatabase.GetAssetPath(obj));
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
