using Sirenix.OdinInspector;
using UnityEngine;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    [IsChineseAttributeExample]
    public class OnInspectorGUIExample : ExampleScriptableObject
    {
        [OnInspectorInit("@texture = Sirenix.Utilities.Editor.EditorIcons.OdinInspectorLogo")]
        [OnInspectorGUI("DrawPreview", append: true)]
        public Texture2D texture;

        [PropertySpace(30)]
        [OnInspectorInit("@texture2D = Sirenix.Utilities.Editor.EditorIcons.OdinInspectorLogo")]
        [OnInspectorGUI(nameof(DrawPreview), nameof(DrawPreview))]
        public Texture2D texture2D;

        void DrawPreview()
        {
            if (texture == null) return;

            GUILayout.BeginVertical(GUI.skin.box);
            GUILayout.Label(texture);
            GUILayout.EndVertical();
        }

        [OnInspectorGUI]
        void OnInspectorGUI()
        {
            UnityEditor.EditorGUILayout.HelpBox("OnInspectorGUI 能够使用在方法也可以使用在字段",
                UnityEditor.MessageType.Info);
        }
    }
}