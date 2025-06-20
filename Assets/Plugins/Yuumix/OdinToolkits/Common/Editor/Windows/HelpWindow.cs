using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Common.InspectorLocalization;

namespace Yuumix.OdinToolkits.Common.Editor
{
    [Searchable]
    public class HelpWindow : OdinEditorWindow
    {
        [LocalizedButtonWidgetConfig("https://github.com/Yuumi-Zeus/OdinToolkits-For-Unity",
            buttonSize: ButtonSizes.Large, icon: SdfIconType.Github)]
        public LocalizedButtonWidget gitHubButton = new LocalizedButtonWidget(() =>
        {
            Application.OpenURL("https://github.com/Yuumi-Zeus/OdinToolkits-For-Unity");
        });

        protected override void OnEnable()
        {
            base.OnEnable();
            WindowPadding = new Vector4(10, 10, 10, 10);
        }

        [MenuItem(MenuItemGlobalSettings.HelpMenuItemName, false,
            MenuItemGlobalSettings.HelpWindowPriority)]
        public static void ShowWindow()
        {
            var window = GetWindow<HelpWindow>();
            if (InspectorLocalizationManagerSO.IsChinese)
            {
                window.titleContent = new GUIContent(MenuItemGlobalSettings.HelpWindowNameCn);
            }
            else if (InspectorLocalizationManagerSO.IsEnglish)
            {
                window.titleContent = new GUIContent(MenuItemGlobalSettings.HelpWindowNameEn);
            }

            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
            window.minSize = new Vector2(500, 500);
            window.ShowUtility();
        }
    }
}
