using Sirenix.OdinInspector;
using UnityEditor;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [OdinToolkitsAttributeExample]
    public class OnInspectorGUIExample : ExampleSO
    {
        #region Serialized Fields

        [OnInspectorInit("@texture = Sirenix.Utilities.Editor.EditorIcons.OdinInspectorLogo")]
        [OnInspectorGUI("DrawPreview")]
        public Texture2D texture;

        [PropertySpace(30)]
        [OnInspectorInit("@texture2D = Sirenix.Utilities.Editor.EditorIcons.OdinInspectorLogo")]
        [OnInspectorGUI(nameof(DrawPreview), nameof(DrawPreview))]
        public Texture2D texture2D;

        #endregion

        void DrawPreview()
        {
            if (texture == null)
            {
                return;
            }

            GUILayout.BeginVertical(GUI.skin.box);
            GUILayout.Label(texture);
            GUILayout.EndVertical();
        }

        [OnInspectorGUI]
        void OnInspectorGUI()
        {
            EditorGUILayout.HelpBox("OnInspectorGUI 能够使用在方法也可以使用在字段",
                MessageType.Info);
        }
    }
}
