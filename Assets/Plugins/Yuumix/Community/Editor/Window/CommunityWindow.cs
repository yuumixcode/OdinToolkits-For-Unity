using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Core.Editor;
using YuumixEditor;

namespace Yuumix.OdinToolkits.Community.Editor
{
    public class CommunityWindow : OdinEditorWindow
    {
        #region Serialized Fields

        [PropertyOrder(-99)]
        [PropertySpace(0, 10)]
        public BilingualHeaderWidget header = new BilingualHeaderWidget(
            "社区",
            "Community Overview",
            "社区资源或者优质资源推荐的总览窗口",
            "The overview window for recommending community resources or high-quality resources");

        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        public CommunityRepositorySO repository;

        #endregion

        #region Event Functions

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

        #endregion

        [MenuItem(OdinToolkitsMenuItems.COMMUNITY, false,
            OdinToolkitsMenuItems.COMMUNITY_PRIORITY)]
        public static void ShowWindow()
        {
            var window = GetWindow<CommunityWindow>();
            window.titleContent = new GUIContent(OdinToolkitsMenuItems.COMMUNITY_WINDOW_NAME);
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(1000, 800);
            window.ShowUtility();
        }
    }
}
