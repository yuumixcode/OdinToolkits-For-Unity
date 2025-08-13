using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.Universal;
#if UNITY_EDITOR
using Sirenix.Utilities.Editor;
using UnityEditor;
#endif

namespace Yuumix.OdinToolkits.Core
{
    /// <summary>
    /// 双语页脚控件，为模块补充一些信息
    /// </summary>
    [Serializable]
    [InlineProperty]
    [HideLabel]
    [BilingualComment("双语页脚控件，为模块补充一些信息",
        "Bilingual footer widget, for the module to supplement some information")]
    public class BilingualFooterWidget
    {
        public BilingualFooterWidget(string lastUpdate = null,
            BilingualData[] additionalDesc = null) =>
            AdditionalDesc = additionalDesc;

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
        [CustomValueDrawer("DrawBilingualData")]
        [BilingualText("补充说明", "Additional Description")]
        [ListDrawerSettings(HideAddButton = true, IsReadOnly = true)]
        public BilingualData[] AdditionalDesc { get; }

        bool AdditionIsNull() => AdditionalDesc == null || AdditionalDesc.Length == 0;
#if UNITY_EDITOR
        BilingualData DrawBilingualData(BilingualData value)
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
