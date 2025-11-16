#if UNITY_EDITOR
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using System;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Core;

namespace YuumixEditor
{
    /// <summary>
    /// 水平横向分割线组件
    /// </summary>
    [Summary("水平横向分割线组件")]
    [Serializable]
    [HideLabel]
    [InlineProperty]
    public class HorizontalSeparateWidget
    {
        /// <summary>
        /// 深色横线高度
        /// </summary>
        [Summary("深色横线高度")]
        int _darkLineHeight;

        /// <summary>
        /// 浅色横线高度，构造函数中未设置则默认为深色横线高度 - 1
        /// </summary>
        [Summary("浅色横线高度，构造函数中未设置则默认为深色横线高度 - 1")]
        int _lightLineHeight;

        /// <summary>
        /// 分割线下方高度
        /// </summary>
        [Summary("分割线下方高度")]
        float _spaceAfter;

        /// <summary>
        /// 分割线上方高度
        /// </summary>
        [Summary("分割线上方高度")]
        float _spaceBefore;

        public HorizontalSeparateWidget()
        {
            _darkLineHeight = 2;
            _lightLineHeight = _darkLineHeight - 1;
            _spaceBefore = 5;
            _spaceAfter = 5;
        }

        public HorizontalSeparateWidget(int darkLineHeight, float spaceBefore, float spaceAfter)
        {
            _darkLineHeight = darkLineHeight;
            _lightLineHeight = _darkLineHeight - 1;
            _spaceBefore = spaceBefore;
            _spaceAfter = spaceAfter;
        }

        public HorizontalSeparateWidget(int darkLineHeight, int lightLineHeight, float spaceAfter,
            float spaceBefore)
        {
            _darkLineHeight = darkLineHeight;
            _lightLineHeight = lightLineHeight;
            _spaceAfter = spaceAfter;
            _spaceBefore = spaceBefore;
        }

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
            SirenixEditorGUI.HorizontalLineSeparator(DarkLineColor, _darkLineHeight);
            SirenixEditorGUI.HorizontalLineSeparator(LightLineColor, _lightLineHeight);
            GUILayout.Space(_spaceAfter);
            EditorGUILayout.EndVertical();
        }
    }
}
#endif
