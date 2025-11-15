using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Core.Editor;

namespace Yuumix.Community.Editor
{
    public class CommunityWindow : OdinEditorWindow
    {
        #region Serialized Fields

        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        public CommunityRepositoryVisualPanelSO repositoryVisualPanel;

        #endregion

        #region Event Functions

        protected override void OnEnable()
        {
            base.OnEnable();
            WindowPadding = new Vector4(10, 10, 10, 10);
            if (repositoryVisualPanel)
            {
                return;
            }

            repositoryVisualPanel = CommunityRepositoryVisualPanelSO.Instance;
            repositoryVisualPanel.CanSelectTags = false;
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
