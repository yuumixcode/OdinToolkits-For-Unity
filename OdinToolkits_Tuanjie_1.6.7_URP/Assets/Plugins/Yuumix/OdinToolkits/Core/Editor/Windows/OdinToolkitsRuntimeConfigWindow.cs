using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace Yuumix.OdinToolkits.Core.Editor
{
    public class OdinToolkitsRuntimeConfigWindow : OdinEditorWindow
    {
        #region Serialized Fields

        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        public OdinToolkitsRuntimeConfigSO runtimeConfig;

        #endregion

        #region Event Functions

        protected override void OnEnable()
        {
            base.OnEnable();
            if (!runtimeConfig)
            {
                runtimeConfig = OdinToolkitsRuntimeConfigSO.Instance;
            }
        }

        #endregion

        [MenuItem(OdinToolkitsMenuItems.RUNTIME_CONFIG, false, OdinToolkitsMenuItems.RUNTIME_CONFIG_PRIORITY)]
        public static void ShowWindow()
        {
            var window = GetWindow<OdinToolkitsRuntimeConfigWindow>();
            window.titleContent = new GUIContent(OdinToolkitsMenuItems.RUNTIME_CONFIG_WINDOW_NAME);
            window.position = GUIHelper.GetEditorWindowRect()
                .AlignCenter(700, 600);
        }
    }
}
