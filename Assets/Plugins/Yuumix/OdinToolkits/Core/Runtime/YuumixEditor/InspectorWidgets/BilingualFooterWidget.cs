#if UNITY_EDITOR

using System;
using Sirenix.OdinInspector;
using Sirenix.Utilities.Editor;
using Yuumix.OdinToolkits.Core;

namespace YuumixEditor
{
    /// <summary>
    /// 双语页脚控件，为模块补充一些信息
    /// </summary>
    [Serializable]
    [InlineProperty]
    [HideLabel]
    public class BilingualFooterWidget
    {
        string _lastUpdate;

        public BilingualFooterWidget(string lastUpdate = null,
            ChangeLogData[] additionalDesc = null)
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
            string chineseText = "补充说明 - 上次更新时间: " + _lastUpdate;
            string englishText = "Additional Description - Last Update: " + _lastUpdate;
            return new BilingualData(chineseText, englishText).GetCurrentOrFallback();
        }

        bool AdditionIsNull() => AdditionalDesc == null || AdditionalDesc.Length == 0;
    }
}
#endif
