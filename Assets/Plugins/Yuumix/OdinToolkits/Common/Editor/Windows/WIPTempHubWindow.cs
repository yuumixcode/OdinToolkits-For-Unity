using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Common.Editor;
using Yuumix.OdinToolkits.Common.InspectorLocalization;

namespace Yuumix.OdinToolkits.Common.Editor
{
    public class WIPTempHubWindow : OdinEditorWindow
    {
        public LocalizedHeaderWidget header =
            new LocalizedHeaderWidget("临时调试窗口中心", "Temporary Hub");

        [MenuItem(MenuItemGlobalSettings.TempHubMenuItemName, false, MenuItemGlobalSettings.TempHubPriority)]
        public static void ShowWindow()
        {
            var window = GetWindow<WIPTempHubWindow>();
            window.titleContent = new GUIContent("临时调试窗口中心");
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
            window.Show();
        }
    }
}
