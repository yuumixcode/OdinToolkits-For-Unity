using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class OnInspectorGUIExample : ExampleScriptableObject
    {
        [OnInspectorInit("@texture = Sirenix.Utilities.Editor.EditorIcons.OdinInspectorLogo")]
        [OnInspectorGUI("DrawPreview", true)]
        public Texture2D texture;

        [PropertySpace(30)]
        [OnInspectorInit("@texture2D = Sirenix.Utilities.Editor.EditorIcons.OdinInspectorLogo")]
        [OnInspectorGUI(nameof(DrawPreview), nameof(DrawPreview))]
        public Texture2D texture2D;

        private void DrawPreview()
        {
            if (texture == null) return;

            GUILayout.BeginVertical(GUI.skin.box);
            GUILayout.Label(texture);
            GUILayout.EndVertical();
        }

        [OnInspectorGUI]
        private void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("OnInspectorGUI 能够使用在方法也可以使用在字段",
                MessageType.Info);
        }
    }
}