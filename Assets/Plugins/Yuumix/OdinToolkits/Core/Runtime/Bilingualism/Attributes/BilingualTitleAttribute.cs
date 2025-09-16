using System;
using System.Diagnostics;
using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.Core
{
    /// <summary>
    /// 多语言的标题特性，默认支持中文和英语
    /// </summary>
    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public class BilingualTitleAttribute : Attribute
    {
        public readonly BilingualData TitleData;
        public readonly BilingualData SubtitleData;
        public readonly TitleAlignments TitleAlignment;
        public readonly bool HorizontalLine;
        public readonly bool Bold;
        public readonly bool BeforeSpace;

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
    }
}
