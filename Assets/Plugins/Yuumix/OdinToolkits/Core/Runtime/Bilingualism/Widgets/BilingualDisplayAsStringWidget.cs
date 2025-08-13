using System;
using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.Core
{
    /// <summary>
    /// 双语字符串显示控件，以字段的形式支持多语言
    /// </summary>
    /// <remarks>
    /// 支持实时语言切换，使用 Odin Inspector 绘制系统实现
    /// </remarks>
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
