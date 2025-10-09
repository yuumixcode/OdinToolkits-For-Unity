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
    public abstract class AttributeContainerProSO : SerializedScriptableObject, IOdinToolkitsEditorReset
    {
        const int CONTAINER_CONTENT_PADDING = 10;
        GUIStyle _containerTitleStyle;
        GUIStyle _containerContentStyle;

        Rect _contentRect;

        GUIStyle ContainerTitleStyle =>
            _containerTitleStyle ??= new GUIStyle(SirenixGUIStyles.TitleCentered)
            {
                fontSize = 15
            };

        GUIStyle ContainerContentStyle =>
            _containerContentStyle ??= new GUIStyle(SirenixGUIStyles.ToolbarBackground)
            {
                stretchHeight = false,
                padding = new RectOffset(
                    CONTAINER_CONTENT_PADDING,
                    CONTAINER_CONTENT_PADDING,
                    CONTAINER_CONTENT_PADDING,
                    CONTAINER_CONTENT_PADDING)
            };

        [PropertyOrder(-99)]
        [EnableGUI]
        [ShowInInspector]
        public BilingualHeaderWidget Header => GetHeaderWidget();

        [PropertyOrder(-50)]
        [ShowInInspector]
        public HorizontalSeparateWidget Separate => new HorizontalSeparateWidget();

        public abstract void EditorReset();

        [PropertyOrder(-50)]
        [OnInspectorGUI]
        public void UseTips(InspectorProperty property)
        {
            const string title = "使用提示";
            Rect headerRect = SirenixEditorGUI.BeginHorizontalToolbar(30f);
            float titleWidth = ContainerTitleStyle.CalcSize(GUIHelper.TempContent(title)).x;
            Rect titleRect = headerRect.AlignCenter(titleWidth);
            EditorGUI.LabelField(titleRect, title, ContainerTitleStyle);
            // SirenixEditorGUI.DrawBorders(titleRect, 1, Color.green);
            GUILayout.FlexibleSpace();
            SirenixEditorGUI.EndHorizontalToolbar();
            GUILayout.Space(-2);
            _contentRect = EditorGUILayout.BeginVertical(ContainerContentStyle);
        }

        [PropertyOrder(-44)]
        [OnInspectorGUI]
        public void DrawList() { }

        [PropertyOrder(-40)]
        [OnInspectorGUI]
        public void UseTip2(InspectorProperty property)
        {
            EditorGUILayout.EndVertical();
            SirenixEditorGUI.DrawBorders(_contentRect, 1);
        }

        protected abstract BilingualHeaderWidget GetHeaderWidget();

        protected void BaseReset() { }
    }
}
