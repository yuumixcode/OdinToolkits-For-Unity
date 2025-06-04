using Sirenix.OdinInspector;
using System;
using System.Diagnostics;

namespace Yuumix.OdinToolkits.Modules.CustomExtensions.Attributes
{
    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public class LocalizedTitleAttribute : Attribute
    {
        public readonly string ChineseSubTitle;
        public readonly string ChineseTitle;
        public readonly string EnglishSubTitle;
        public readonly string EnglishTitle;
        public readonly TitleAlignments TitleAlignment;
        public readonly bool HorizontalLine;
        public readonly bool Bold;

        public LocalizedTitleAttribute(
            string chineseTitle,
            string englishTitle = null,
            string chineseSubTitle = null,
            string englishSubTitle = null,
            TitleAlignments titleAlignment = TitleAlignments.Left,
            bool horizontalLine = true,
            bool bold = true)
        {
            ChineseTitle = chineseTitle ?? "null";
            EnglishTitle = englishTitle ?? "null";
            ChineseSubTitle = chineseSubTitle;
            EnglishSubTitle = englishSubTitle;
            TitleAlignment = titleAlignment;
            HorizontalLine = horizontalLine;
            Bold = bold;
        }
    }
}
