using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Common.InspectorMultiLanguage;

namespace Yuumix.OdinToolkits.Common.Editor
{
    public class HelpWindow : OdinEditorWindow
    {
        [MultiLanguageButtonWidgetConfig("打开 GitHub 仓库：https://github.com/Yuumi-Zeus/OdinToolkits-For-Unity",
            "Open GitHub Repo: https://github.com/Yuumi-Zeus/OdinToolkits-For-Unity",
            buttonSize: ButtonSizes.Large, icon: SdfIconType.Github)]
        public MultiLanguageButtonProperty gitHubButton = new MultiLanguageButtonProperty(() =>
        {
            Application.OpenURL("https://github.com/Yuumi-Zeus/OdinToolkits-For-Unity");
        });

        [PropertyOrder(-1)]
        [MultiLanguageButton("打开 GitHub 仓库",
            "Open GitHub Repository")]
        public void OpenGitHub()
        {
            Application.OpenURL("https://github.com/Yuumi-Zeus/OdinToolkits-For-Unity");
        }

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
            var multiLanguageData = new MultiLanguageData(MenuItemGlobalSettings.HelpWindowNameCn,
                MenuItemGlobalSettings.HelpWindowNameEn);
            window.titleContent = new GUIContent(multiLanguageData.GetCurrentOrFallback());
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
            window.minSize = new Vector2(500, 500);
            window.ShowUtility();
        }
    }
}
