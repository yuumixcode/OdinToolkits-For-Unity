using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Common.Editor;

namespace Yuumix.OdinToolkits.Modules.Odin.AttributeOverviewPro.Editor
{
    public class AttributeOverviewProWindow : OdinMenuEditorWindow
    {
        [MenuItem(MenuItemSettings.AttributeOverviewProMenuItemName,
            priority = MenuItemSettings.AttributeOverviewProPriority)]
        static void ShowWindow()
        {
            var window = GetWindow<AttributeOverviewProWindow>();
            window.titleContent = new GUIContent(MenuItemSettings.AttributeOverviewProWindowName);
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 700);
            window.Show();
        }

        protected override OdinMenuTree BuildMenuTree() => new OdinMenuTree();
    }
}
