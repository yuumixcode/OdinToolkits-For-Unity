using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Core.Editor;

namespace Yuumix.OdinToolkits.Community.Editor
{
    public class CommunityWindow : OdinEditorWindow
    {
        [PropertyOrder(-99)]
        [PropertySpace(0, 10)]
        public BilingualHeaderWidget header = new BilingualHeaderWidget(
            "社区",
            "Community Overview",
            "社区资源或者优质资源推荐的总览窗口",
            "The overview window for recommending community resources or high-quality resources");

        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        public CommunityRepositorySO repository;

        protected override void OnEnable()
        {
            base.OnEnable();
            WindowPadding = new Vector4(10, 10, 10, 10);
            if (!repository)
            {
                repository = CommunityRepositorySO.Instance;
                repository.CanSelectTags = false;
            }
        }

        [MenuItem(OdinToolkitsWindowMenuItems.COMMUNITY, false,
            OdinToolkitsWindowMenuItems.COMMUNITY_PRIORITY)]
        public static void ShowWindow()
        {
            var window = GetWindow<CommunityWindow>();
            window.titleContent = new GUIContent(OdinToolkitsWindowMenuItems.COMMUNITY_WINDOW_NAME);
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
            window.ShowUtility();
        }
    }
}
