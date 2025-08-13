using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using Sirenix.Utilities;
using Sirenix.Utilities.Editor;
using UnityEditor;
using UnityEngine;
using Yuumix.OdinToolkits.Core;
using YuumixEditor;

namespace Yuumix.OdinToolkits.Modules.AttributeOverviewPro.Editor
{
    public abstract class AttributeContainerProSO : SerializedScriptableObject, IOdinToolkitsReset
    {
        const int CONTAINER_CONTENT_PADDING = 10;
        GUIStyle _containerTitleStyle;
        GUIStyle _containerContentStyle;

        void EnsureGUIStyle()
        {
            _containerTitleStyle ??= new GUIStyle(SirenixGUIStyles.TitleCentered)
            {
                fontSize = 15
            };
            _containerContentStyle = new GUIStyle(SirenixGUIStyles.ToolbarBackground)
            {
                stretchHeight = false,
                padding = new RectOffset(
                    CONTAINER_CONTENT_PADDING,
                    CONTAINER_CONTENT_PADDING,
                    CONTAINER_CONTENT_PADDING,
                    CONTAINER_CONTENT_PADDING)
            };
        }

        [PropertyOrder(-99)]
        [EnableGUI]
        [ShowInInspector]
        public BilingualHeaderWidget Header => GetHeaderWidget();

        [PropertyOrder(-50)]
        [ShowInInspector]
        public HorizontalSeparateEditorWidget Separate => new HorizontalSeparateEditorWidget();

        [PropertyOrder(-50)]
        [OnInspectorGUI]
        public void UseTips(InspectorProperty property)
        {
            EnsureGUIStyle();
            GUIStyle titleStyle = _containerTitleStyle;
            const string title = "使用提示";
            Rect headerRect = SirenixEditorGUI.BeginHorizontalToolbar();
            float titleWidth = titleStyle.CalcSize(GUIHelper.TempContent(title)).x;
            Rect titleRect = headerRect.AlignCenter(titleWidth);
            EditorGUI.LabelField(titleRect, title, titleStyle);
            // SirenixEditorGUI.DrawBorders(titleRect, 1, Color.green);
            GUILayout.FlexibleSpace();
            SirenixEditorGUI.EndHorizontalToolbar();
            // GUILayout.Space(-2);
            contentRect = EditorGUILayout.BeginVertical(_containerContentStyle);
        }

        Rect contentRect;

        [PropertyOrder(-45)]
        public List<string> number = new List<string>() { "1", "2", "3" };

        [PropertyOrder(-40)]
        [OnInspectorGUI]
        public void UseTip2(InspectorProperty property)
        {
            EditorGUILayout.EndVertical();
            // SirenixEditorGUI.DrawBorders(contentRect, 1);
        }

        protected abstract BilingualHeaderWidget GetHeaderWidget();

        public abstract void OdinToolkitsReset();

        protected void BaseReset() { }
    }
}
