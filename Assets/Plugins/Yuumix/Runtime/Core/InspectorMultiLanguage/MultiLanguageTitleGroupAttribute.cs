using Sirenix.OdinInspector;
using System;
using System.Diagnostics;

namespace Yuumix.OdinToolkits.Core
{
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public class MultiLanguageTitleGroupAttribute : PropertyGroupAttribute
    {
        public MultiLanguageData TitleData;
        public MultiLanguageData SubtitleData;
        public TitleAlignments TitleAlignment;
        public bool HorizontalLine;
        public bool BoldTitle;
        public bool Indent;

        public MultiLanguageTitleGroupAttribute(string groupId,
            string chineseTitle,
            string englishTitle = null,
            string chineseSubtitle = null,
            string englishSubtitle = null,
            TitleAlignments titleAlignment = TitleAlignments.Left,
            bool horizontalLine = true,
            bool boldTitle = true,
            bool indent = false,
            float order = 0) :
            base(groupId, order)
        {
            TitleData = new MultiLanguageData(chineseTitle, englishTitle);
            chineseSubtitle ??= string.Empty;
            SubtitleData = new MultiLanguageData(chineseSubtitle, englishSubtitle);
            TitleAlignment = titleAlignment;
            HorizontalLine = horizontalLine;
            BoldTitle = boldTitle;
            Indent = indent;
        }

        protected override void CombineValuesWith(PropertyGroupAttribute other)
        {
            // 非默认值优先的原则
            var multiLanguageTitleGroupAttribute = other as MultiLanguageTitleGroupAttribute;
            if (multiLanguageTitleGroupAttribute == null)
            {
                return;
            }

            if (!TitleData.Equals(multiLanguageTitleGroupAttribute.TitleData))
            {
                TitleData = multiLanguageTitleGroupAttribute.TitleData;
            }

            if (!SubtitleData.Equals(multiLanguageTitleGroupAttribute.SubtitleData))
            {
                SubtitleData = multiLanguageTitleGroupAttribute.SubtitleData;
            }

            if (TitleAlignment != TitleAlignments.Left)
            {
                multiLanguageTitleGroupAttribute.TitleAlignment = TitleAlignment;
            }
            else
            {
                TitleAlignment = multiLanguageTitleGroupAttribute.TitleAlignment;
            }

            if (!HorizontalLine)
            {
                multiLanguageTitleGroupAttribute.HorizontalLine = HorizontalLine;
            }
            else
            {
                HorizontalLine = multiLanguageTitleGroupAttribute.HorizontalLine;
            }

            if (!BoldTitle)
            {
                multiLanguageTitleGroupAttribute.BoldTitle = BoldTitle;
            }
            else
            {
                BoldTitle = multiLanguageTitleGroupAttribute.BoldTitle;
            }

            if (Indent)
            {
                multiLanguageTitleGroupAttribute.Indent = Indent;
            }
            else
            {
                Indent = multiLanguageTitleGroupAttribute.Indent;
            }
        }
    }
}
