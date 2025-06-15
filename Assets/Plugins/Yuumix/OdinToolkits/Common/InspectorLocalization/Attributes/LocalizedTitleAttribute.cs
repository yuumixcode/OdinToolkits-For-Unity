using Sirenix.OdinInspector;
using System;
using System.Diagnostics;
using Yuumix.OdinToolkits.Common.InspectorLocalization.Structs;

namespace Yuumix.OdinToolkits.Common.InspectorLocalization.Attributes
{
    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public class LocalizedTitleAttribute : Attribute
    {
        public readonly MultiLanguageTitleData MultiLanguageData;
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
            MultiLanguageData =
                new MultiLanguageTitleData(chineseTitle, englishTitle, chineseSubTitle, englishSubTitle);
            TitleAlignment = titleAlignment;
            HorizontalLine = horizontalLine;
            Bold = bold;
        }
    }
}
