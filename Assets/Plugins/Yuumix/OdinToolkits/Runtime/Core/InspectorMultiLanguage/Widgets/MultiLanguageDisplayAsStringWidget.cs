using System;
using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Core
{
    /// <summary>
    /// 多语言字符串显示部件，以字段的形式支持多语言
    /// </summary>
    /// <remarks>
    /// 支持兼容 Odin Inspector 绘制系统<br />
    /// 支持实时语言切换
    /// </remarks>
    [Serializable]
    [HideLabel]
    [InlineProperty]
    public class MultiLanguageDisplayAsStringWidget
    {
        public MultiLanguageDisplayAsStringWidget(string chinese, string english = null)
        {
            ChineseDisplay = chinese;
            EnglishDisplay = english ?? chinese;
        }

        [ShowIfChinese]
        [HideLabel]
        [ShowInInspector]
        [EnableGUI]
        public string ChineseDisplay { get; }

        [ShowIfEnglish]
        [HideLabel]
        [ShowInInspector]
        [EnableGUI]
        public string EnglishDisplay { get; }
    }
}
