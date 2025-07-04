using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Common.InspectorMultiLanguage;
using Yuumix.OdinToolkits.Community.Editor;

namespace Yuumix.OdinToolkits.Common.Editor
{
   
    public class CommunityWindow : OdinEditorWindow
    {
        [PropertyOrder(-99)]
        public MultiLanguageHeaderWidget header = new MultiLanguageHeaderWidget(
            "社区贡献窗口",
            "Community Window",
            "存放社区贡献工具，以卡片窗口的形式存放，点击可以打开对应的工具窗口/使用示例。",
            "Store community tools in the form of card windows." +
            " Clicking on them will open the corresponding tool window / usage examples.");

        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        public CommunityRepoSO repo;

        protected override void OnEnable()
        {
            base.OnEnable();
            WindowPadding = new Vector4(10, 10, 10, 10);
            repo = CommunityRepoSO.Instance;
        }

        [MenuItem(MenuItemGlobalSettings.CommunityMenuItemName, false,
            MenuItemGlobalSettings.CommunityPriority)]
        public static void ShowWindow()
        {
            var window = GetWindow<CommunityWindow>();
            if (InspectorMultiLanguageManagerSO.IsChinese)
            {
                window.titleContent = new GUIContent(MenuItemGlobalSettings.CommunityWindowNameCn);
            }
            else if (InspectorMultiLanguageManagerSO.IsEnglish)
            {
                window.titleContent = new GUIContent(MenuItemGlobalSettings.CommunityWindowNameEn);
            }

            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
            window.minSize = new Vector2(500, 500);
            window.ShowUtility();
        }
    }
}
