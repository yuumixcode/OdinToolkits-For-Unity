using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Core.Editor;

namespace Yuumix.OdinToolkits.ScriptDocGenerator.Editor
{
    public class ScriptDocGeneratorWindow : OdinEditorWindow
    {
        #region Serialized Fields

        [SerializeField]
        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        ScriptDocGeneratorPanelSO panel;

        #endregion

        [MenuItem(OdinToolkitsMenuItems.SCRIPT_DOC_GEN, false, OdinToolkitsMenuItems.SCRIPT_DOC_GEN_PRIORITY)]
        public static void OpenWindow()
        {
            var window = GetWindow<ScriptDocGeneratorWindow>();
            window.titleContent = new GUIContent(OdinToolkitsMenuItems.SCRIPT_DOC_GEN_WINDOW_NAME);
            window.position = GUIHelper.GetEditorWindowRect()
                .AlignCenter(1000, 800);
            window.Show();
        }

        #region Event Functions

        protected override void OnEnable()
        {
            base.OnEnable();
            WindowPadding = new Vector4(10, 10, 10, 10);
            panel = ScriptDocGeneratorPanelSO.Instance;
            ScriptDocGeneratorPanelSO.ToastRequested -= ShowToast;
            ScriptDocGeneratorPanelSO.ToastRequested += ShowToast;
        }

        protected override void OnDestroy()
        {
            ScriptDocGeneratorPanelSO.ToastRequested -= ShowToast;
            base.OnDestroy();
        }

        #endregion
    }
}
