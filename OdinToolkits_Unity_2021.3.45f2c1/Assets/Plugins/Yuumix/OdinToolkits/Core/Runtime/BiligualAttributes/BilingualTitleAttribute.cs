using Sirenix.OdinInspector;
using System;
using System.Diagnostics;

namespace Yuumix.OdinToolkits.Core
{
    /// <summary>
    /// 多语言的标题特性，默认支持中文和英语
    /// </summary>
    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public class BilingualTitleAttribute : Attribute
    {
        public BilingualTitleAttribute(
            string chineseTitle,
            string englishTitle = null,
            string chineseSubTitle = null,
            string englishSubTitle = null,
            TitleAlignments titleAlignment = TitleAlignments.Left,
            bool horizontalLine = true,
            bool bold = true,
            bool beforeSpace = true)
        {
            TitleData = new BilingualData(chineseTitle, englishTitle);
            SubtitleData = new BilingualData(chineseSubTitle, englishSubTitle);
            TitleAlignment = titleAlignment;
            HorizontalLine = horizontalLine;
            Bold = bold;
            BeforeSpace = beforeSpace;
        }

        public bool BeforeSpace { get; set; }
        public bool Bold { get; set; }
        public bool HorizontalLine { get; set; }
        public BilingualData SubtitleData { get; set; }
        public TitleAlignments TitleAlignment { get; set; }
        public BilingualData TitleData { get; set; }
    }
}
