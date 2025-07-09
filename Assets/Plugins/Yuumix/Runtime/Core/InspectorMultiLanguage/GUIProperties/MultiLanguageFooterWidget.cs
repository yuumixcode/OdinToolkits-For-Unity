using Sirenix.OdinInspector;
using System;
using UnityEngine;
using Yuumix.OdinToolkits.Core.Contributors;
using Yuumix.OdinToolkits.Core.Version;
#if UNITY_EDITOR
using Sirenix.Utilities.Editor;
using UnityEditor;
#endif

namespace Yuumix.OdinToolkits.Core
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
        public MultiLanguageFooterWidget(ContributorInfo[] contributors, string lastUpdate = null,
            MultiLanguageData[] additionalDesc = null)
        {
            Contributors = contributors;
            _lastUpdate = lastUpdate ?? OdinToolkitsVersion.GetLastUpdate();
            AdditionalDesc = additionalDesc;
        }

        [PropertyOrder(0)]
        [OnInspectorGUI]
        public void Separate()
        {
#if UNITY_EDITOR
            SirenixEditorGUI.DrawThickHorizontalSeperator(4, 10, 5);
#endif
        }

        [PropertyOrder(5)]
        [CustomContextMenu("Reset Footer", nameof(Reset))]
        [ListDrawerSettings(HideAddButton = true, IsReadOnly = true)]
        [MultiLanguageText("$GetLastUpdateLabelCn", "$GetLastUpdateLabelEn")]
        [ShowInInspector]
        [EnableGUI]
        [HideReferenceObjectPicker]
        public ContributorInfo[] Contributors { get; private set; }

        [PropertyOrder(10)]
        [ShowInInspector]
        [HideIf("$AdditionIsNull")]
        [EnableGUI]
        [CustomValueDrawer("DrawMultipleLanguageData")]
        [MultiLanguageText("补充说明(任何有用的信息)", "Additional Description (Any Useful Information)")]
        [ListDrawerSettings(HideAddButton = true, IsReadOnly = true)]
        public MultiLanguageData[] AdditionalDesc { get; }

        bool AdditionIsNull()
        {
            return AdditionalDesc == null || AdditionalDesc.Length == 0;
        }

        public void Reset()
        {
            Contributors = new[]
            {
                YuumiZeus.ToContributor(DateTime.Today.ToString("yyyy/MM/dd"))
            };
        }

        string _lastUpdate;
        string GetLastUpdateLabelCn() => "贡献者列表" + " - Last Update: " + _lastUpdate;
        string GetLastUpdateLabelEn() => "Contributors" + " - Last Update: " + _lastUpdate;
#if UNITY_EDITOR
        MultiLanguageData DrawMultipleLanguageData(MultiLanguageData value)
        {
            var rect = EditorGUILayout.GetControlRect();
            var labelStyle = new GUIStyle(SirenixGUIStyles.RichTextLabel)
            {
                fontSize = 14
            };
            EditorGUI.SelectableLabel(rect, value.GetCurrentOrFallback(), labelStyle);
            return value;
        }
#endif
    }
}
