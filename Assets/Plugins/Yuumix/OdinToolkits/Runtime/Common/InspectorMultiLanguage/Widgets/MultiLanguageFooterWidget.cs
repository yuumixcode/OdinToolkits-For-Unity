using System;
using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using Sirenix.Utilities.Editor;
using UnityEditor;
#endif

namespace Yuumix.OdinToolkits.Common
{
    /// <summary>
    /// 多语言的页脚控件，为模块补充一些信息
    /// </summary>
    [Serializable]
    [InlineProperty]
    [HideLabel]
    [MultiLanguageComment("多语言的页脚控件，为模块补充一些信息",
        "Multi-language footer widget, for the module to supplement some information")]
    public class MultiLanguageFooterWidget
    {
        public MultiLanguageFooterWidget(string lastUpdate = null,
            MultiLanguageData[] additionalDesc = null)
        {
            AdditionalDesc = additionalDesc;
        }

        [PropertyOrder(0)]
        [OnInspectorGUI]
        [HideIf("$AdditionIsNull")]
        public void Separate()
        {
#if UNITY_EDITOR
            SirenixEditorGUI.DrawThickHorizontalSeperator(4, 10, 5);
#endif
        }

        [PropertyOrder(10)]
        [ShowInInspector]
        [HideIf("$AdditionIsNull")]
        [EnableGUI]
        [CustomValueDrawer("DrawMultipleLanguageData")]
        [MultiLanguageText("补充说明", "Additional Description")]
        [ListDrawerSettings(HideAddButton = true, IsReadOnly = true)]
        public MultiLanguageData[] AdditionalDesc { get; }

        bool AdditionIsNull() => AdditionalDesc == null || AdditionalDesc.Length == 0;
#if UNITY_EDITOR
        MultiLanguageData DrawMultipleLanguageData(MultiLanguageData value)
        {
            Rect rect = EditorGUILayout.GetControlRect();
            var labelStyle = new GUIStyle(SirenixGUIStyles.RichTextLabel)
            {
                fontSize = 13
            };
            EditorGUI.SelectableLabel(rect, value.GetCurrentOrFallback(), labelStyle);
            return value;
        }
#endif
    }
}
