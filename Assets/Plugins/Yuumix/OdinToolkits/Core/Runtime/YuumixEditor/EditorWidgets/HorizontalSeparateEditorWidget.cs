#if UNITY_EDITOR
using System;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;

namespace YuumixEditor
{
    [Serializable]
    [HideLabel]
    [InlineProperty]
    public class HorizontalSeparateEditorWidget
    {
        int _height;
        float _spaceBefore;
        float _spaceAfter;

        Color DarkLineColor => EditorGUIUtility.isProSkin
            ? SirenixGUIStyles.BorderColor
            : new Color(0f, 0f, 0f, 0.2f);

        Color LightLineColor => EditorGUIUtility.isProSkin
            ? new Color(1f, 1f, 1f, 0.1f)
            : new Color(1f, 1f, 1f, 1f);

        [OnInspectorGUI]
        public void Separate()
        {
            EditorGUILayout.BeginVertical();
            GUILayout.Space(_spaceBefore);
            SirenixEditorGUI.HorizontalLineSeparator(DarkLineColor, _height);
            SirenixEditorGUI.HorizontalLineSeparator(LightLineColor, _height);
            GUILayout.Space(_spaceAfter);
            EditorGUILayout.EndVertical();
        }

        public HorizontalSeparateEditorWidget()
        {
            _height = 2;
            _spaceBefore = 5;
            _spaceAfter = 5;
        }

        public HorizontalSeparateEditorWidget(int height, float spaceBefore, float spaceAfter)
        {
            _height = height;
            _spaceBefore = spaceBefore;
            _spaceAfter = spaceAfter;
        }
    }
}
#endif
