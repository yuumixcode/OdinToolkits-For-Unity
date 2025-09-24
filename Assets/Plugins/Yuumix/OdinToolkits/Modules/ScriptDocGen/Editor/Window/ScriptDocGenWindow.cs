using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Core.Editor;

namespace Yuumix.OdinToolkits.Modules.ScriptDocGen.Editor
{
    public class ScriptDocGenWindow : OdinEditorWindow
    {
        [SerializeField]
        [InlineEditor(InlineEditorObjectFieldModes.Hidden)]
        ScriptDocGenInspectorSO inspector;

        protected override void OnEnable()
        {
            base.OnEnable();
            WindowPadding = new Vector4(10, 10, 10, 10);
            inspector = ScriptDocGenInspectorSO.Instance;
            ScriptDocGenInspectorSO.ToastEvent -= ShowToast;
            ScriptDocGenInspectorSO.ToastEvent += ShowToast;
        }

        [MenuItem(OdinToolkitsMenuItems.SCRIPT_DOC_GEN,
            false,
            OdinToolkitsMenuItems.SCRIPT_DOC_GEN_PRIORITY)]
        public static void Open()
        {
            var window = GetWindow<ScriptDocGenWindow>();
            window.titleContent = new GUIContent(OdinToolkitsMenuItems.SCRIPT_DOC_GEN_WINDOW_NAME);
            window.position = GUIHelper.GetEditorWindowRect().AlignCenter(1100, 800);
            window.Show();
        }
    }
}
