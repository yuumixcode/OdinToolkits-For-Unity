using Sirenix.OdinInspector;
using System;
using System.Diagnostics;

namespace Yuumix.OdinToolkits.Core
{
    /// <summary>
    /// 多语言的标题特性，默认支持中文和英语
    /// </summary>
    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    [MultiLanguageComment("多语言的标题特性，默认支持中文和英语",
        "Multi-language title Attribute, supported by default in Chinese and English")]
    public class MultiLanguageTitleAttribute : Attribute
    {
        public readonly MultiLanguageData TitleData;
        public readonly MultiLanguageData SubtitleData;
        public readonly TitleAlignments TitleAlignment;
        public readonly bool HorizontalLine;
        public readonly bool Bold;
        public readonly bool BeforeSpace;

        public MultiLanguageTitleAttribute(
            string chineseTitle,
            string englishTitle = null,
            string chineseSubTitle = null,
            string englishSubTitle = null,
            TitleAlignments titleAlignment = TitleAlignments.Left,
            bool horizontalLine = true,
            bool bold = true,
            bool beforeSpace = true)
        {
            TitleData = new MultiLanguageData(chineseTitle, englishTitle);
            SubtitleData = new MultiLanguageData(chineseSubTitle, englishSubTitle);
            TitleAlignment = titleAlignment;
            HorizontalLine = horizontalLine;
            Bold = bold;
            BeforeSpace = beforeSpace;
        }
    }
}
