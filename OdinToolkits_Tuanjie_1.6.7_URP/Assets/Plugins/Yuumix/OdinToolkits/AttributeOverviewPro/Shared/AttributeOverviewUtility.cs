using Sirenix.Utilities.Editor;
using UnityEngine;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Shared
{
    public static class AttributeOverviewUtility
    {
        const int CONTAINER_CONTENT_PADDING = 10;
        static GUIStyle _containerTitleStyle;
        static GUIStyle _containerContentStyle;
        static GUIStyle _tableCellTextStyle;
        static GUIStyle _resolvedStringParameterValueTitleStyle;

        static GUIStyle _tabButtonCellTextStyle;

        public static GUIStyle ContainerTitleStyle
        {
            get
            {
                _containerTitleStyle ??= new GUIStyle(SirenixGUIStyles.TitleCentered)
                {
                    fontSize = 16
                };
                return _containerTitleStyle;
            }
        }

        public static GUIStyle ContainerContentStyle
        {
            get
            {
                _containerContentStyle ??= new GUIStyle(SirenixGUIStyles.ToolbarBackground)
                {
                    stretchHeight = false,
                    padding = new RectOffset(
                        CONTAINER_CONTENT_PADDING,
                        CONTAINER_CONTENT_PADDING,
                        CONTAINER_CONTENT_PADDING,
                        CONTAINER_CONTENT_PADDING)
                };
                return _containerContentStyle;
            }
        }

        public static GUIStyle TableCellTextStyle
        {
            get
            {
                _tableCellTextStyle ??= new GUIStyle(SirenixGUIStyles.MultiLineCenteredLabel)
                {
                    padding = new RectOffset(5, 5, 5, 5),
                    clipping = TextClipping.Overflow,
                    richText = true
                };
                return _tableCellTextStyle;
            }
        }

        public static GUIStyle ResolvedStringParameterValueTitleStyle
        {
            get
            {
                _resolvedStringParameterValueTitleStyle ??= new GUIStyle(SirenixGUIStyles.TitleCentered)
                {
                    fontSize = 14
                };
                return _resolvedStringParameterValueTitleStyle;
            }
        }

        public static GUIStyle TabButtonCellTextStyle
        {
            get
            {
                _tabButtonCellTextStyle ??= new GUIStyle
                {
                    padding = new RectOffset(10, 10, 10, 10),
                    alignment = TextAnchor.MiddleCenter,
                    clipping = TextClipping.Overflow
                };
                return _tabButtonCellTextStyle;
            }
        }

#if UNITY_EDITOR

        [UnityEditor.InitializeOnEnterPlayMode]
        static void Reset()
        {
            _containerTitleStyle = null;
            _containerContentStyle = null;
            _tableCellTextStyle = null;
        }
#endif
    }
}
