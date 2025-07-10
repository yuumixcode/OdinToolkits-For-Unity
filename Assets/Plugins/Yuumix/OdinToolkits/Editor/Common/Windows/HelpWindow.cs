using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Common;
using Yuumix.OdinToolkits.Editor.Core;

namespace Yuumix.OdinToolkits.Editor.Common
{
    public class HelpWindow : OdinEditorWindow
    {
        [PropertyOrder(-1)]
        [MultiLanguageButton("打开 GitHub 仓库: https://github.com/Yuumi-Zeus/OdinToolkits-For-Unity",
            "Open GitHub Repo: https://github.com/Yuumi-Zeus/OdinToolkits-For-Unity", ButtonSizes.Large,
            icon: SdfIconType.Github)]
        public void OpenGitHub()
        {
            Application.OpenURL("https://github.com/Yuumi-Zeus/OdinToolkits-For-Unity");
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            WindowPadding = new Vector4(10, 10, 10, 10);
        }

        [MenuItem(OdinToolkitsWindowMenuItems.HelpMenuItemName, false,
            OdinToolkitsWindowMenuItems.HelpWindowPriority)]
        public static void ShowWindow()
        {
            var window = GetWindow<HelpWindow>();
            var multiLanguageData = new MultiLanguageData(OdinToolkitsWindowMenuItems.HelpWindowNameCn,
                OdinToolkitsWindowMenuItems.HelpWindowNameEn);
            window.titleContent = new GUIContent(multiLanguageData.GetCurrentOrFallback());
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
            window.minSize = new Vector2(500, 500);
            window.ShowUtility();
        }
    }
}
