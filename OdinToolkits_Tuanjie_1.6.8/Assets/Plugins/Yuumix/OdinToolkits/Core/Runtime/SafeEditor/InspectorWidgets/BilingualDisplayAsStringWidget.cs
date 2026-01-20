using System;
using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.Core.SafeEditor
{
    [Summary("双语字符串显示控件，以字段的形式支持多语言")]
    [Serializable]
    [HideLabel]
    [InlineProperty]
    public class BilingualDisplayAsStringWidget
    {
        public BilingualDisplayAsStringWidget(string chinese, string english = null)
        {
            ChineseDisplay = chinese;
            EnglishDisplay = english ?? chinese;
        }

        [ShowIfChinese]
        [HideLabel]
        [ShowInInspector]
        [EnableGUI]
        public string ChineseDisplay { get; set; }

        [ShowIfEnglish]
        [HideLabel]
        [ShowInInspector]
        [EnableGUI]
        public string EnglishDisplay { get; set; }
    }
}
