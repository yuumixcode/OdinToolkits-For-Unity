using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Yuumix.OdinToolkits.Core.Editor
{
    public class OdinToolkitEditorSettingsWindow : OdinEditorWindow
    {
        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        public InspectorBilingualismConfigSO bilingualismConfig;

        protected override void OnEnable()
        {
            base.OnEnable();
            if (!bilingualismConfig)
            {
                bilingualismConfig = InspectorBilingualismConfigSO.Instance;
            }
        }

        [MenuItem(OdinToolkitsMenuItems.EDITOR_SETTINGS, priority = OdinToolkitsMenuItems.EDITOR_SETTINGS_PRIORITY)]
        public static void ShowWindow()
        {
            var window = GetWindow<OdinToolkitEditorSettingsWindow>();
            window.titleContent = new GUIContent(OdinToolkitsMenuItems.EDITOR_SETTINGS_WINDOW_NAME);
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(700, 600);
        }
    }
}
