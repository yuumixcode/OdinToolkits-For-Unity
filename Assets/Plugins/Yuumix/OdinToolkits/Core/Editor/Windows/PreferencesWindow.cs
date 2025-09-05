using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Yuumix.OdinToolkits.Core.Runtime.Editor
{
    public class PreferencesWindow : OdinEditorWindow
    {
        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        public OdinToolkitsPreferencesSO preferences;

        protected override void OnEnable()
        {
            base.OnEnable();
            if (!preferences)
            {
                preferences = OdinToolkitsPreferencesSO.Instance;
            }
        }

        [MenuItem(OdinToolkitsWindowMenuItems.PREFERENCES, false, OdinToolkitsWindowMenuItems.PREFERENCES_PRIORITY)]
        public static void ShowWindow()
        {
            var window = GetWindow<PreferencesWindow>();
            window.titleContent = new GUIContent(OdinToolkitsWindowMenuItems.PREFERENCES_WINDOW_NAME);
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(700, 600);
        }
    }
}
