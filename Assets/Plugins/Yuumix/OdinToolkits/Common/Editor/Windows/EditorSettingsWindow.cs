using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Common.InspectorLocalization;

namespace Yuumix.OdinToolkits.Common.Editor
{
    public class EditorSettingsWindow : OdinEditorWindow
    {
        [ShowInInspector]
        [InlineEditor(InlineEditorObjectFieldModes.CompletelyHidden)]
        [EnableGUI]
        public InspectorLocalizationManagerSO InspectorLocalizationManager => InspectorLocalizationManagerSO.Instance;

        protected override void OnEnable()
        {
            base.OnEnable();
            WindowPadding = new Vector4(10, 10, 0,0);
        }

        [MenuItem(MenuItemGlobalSettings.EditorSettingsMenuItemName, false,
            MenuItemGlobalSettings.EditorSettingsWindowPriority)]
        public static void ShowWindow()
        {
            var window = GetWindow<EditorSettingsWindow>();
            if (InspectorLocalizationManagerSO.IsChinese)
            {
                window.titleContent = new GUIContent(MenuItemGlobalSettings.EditorSettingsWindowNameCn);
            }
            else if (InspectorLocalizationManagerSO.IsEnglish)
            {
                window.titleContent = new GUIContent(MenuItemGlobalSettings.EditorSettingsWindowNameEn);
            }

            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(800, 600);
            window.minSize = new Vector2(500, 500);
            window.ShowUtility();
        }
    }
}
