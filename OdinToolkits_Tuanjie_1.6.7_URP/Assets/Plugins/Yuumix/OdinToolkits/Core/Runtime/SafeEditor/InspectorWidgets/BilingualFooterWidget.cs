using System;
using Sirenix.OdinInspector;
using UnityEngine;
#if UNITY_EDITOR
using Sirenix.Utilities.Editor;
#endif

namespace Yuumix.OdinToolkits.Core.SafeEditor
{
    [Summary("双语页脚控件，用于补充信息")]
    [Serializable]
    [InlineProperty]
    [HideLabel]
    public class BilingualFooterWidget
    {
        string _lastUpdate;

        public BilingualFooterWidget(string lastUpdate = null, ChangeLogData[] additionalDesc = null)
        {
            _lastUpdate = lastUpdate;
            AdditionalDesc = additionalDesc;
        }

        [PropertyOrder(10)]
        [ShowInInspector]
        [HideIf("$AdditionIsNull")]
        [EnableGUI]
        [LabelText("$GetAdditionalDescText")]
        [ListDrawerSettings(HideAddButton = true, IsReadOnly = true)]
        public ChangeLogData[] AdditionalDesc { get; }

        [PropertyOrder(0)]
        [OnInspectorGUI]
        [HideIf("$AdditionIsNull")]
        public void Separate()
        {
#if UNITY_EDITOR
            SirenixEditorGUI.DrawThickHorizontalSeperator(4, 10, 5);
#endif
        }

        string GetAdditionalDescText()
        {
            var chineseText = "补充说明 - 上次更新时间: " + _lastUpdate;
            var englishText = "Additional Description - Last Update: " + _lastUpdate;
            return new BilingualData(chineseText, englishText).GetCurrentOrFallback();
        }

        bool AdditionIsNull() => AdditionalDesc == null || AdditionalDesc.Length == 0;
    }
}
