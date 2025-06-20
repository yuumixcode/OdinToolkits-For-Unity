namespace Yuumix.OdinToolkits.Common.InspectorLocalization
{
    public readonly struct MultipleLanguageTitleData
    {
        readonly string _chineseTitle;
        readonly string _englishTitle;
        readonly string _chineseSubTitle;
        readonly string _englishSubTitle;

        public InspectorLocalizationManagerSO GetLocalizationManager() => InspectorLocalizationManagerSO.Instance;

        public MultipleLanguageTitleData(string chineseTitle, string englishTitle = null, string chineseSubTitle = null,
            string englishSubTitle = null)
        {
            _chineseTitle = chineseTitle;
            _englishTitle = englishTitle ?? string.Empty;
            _chineseSubTitle = chineseSubTitle ?? string.Empty;
            _englishSubTitle = englishSubTitle ?? string.Empty;
        }

        public string GetCurrentTitleOrFallback()
        {
            if (InspectorLocalizationManagerSO.IsChinese)
            {
                return _chineseTitle;
            }

            if (InspectorLocalizationManagerSO.IsEnglish && !string.IsNullOrEmpty(_englishTitle))
            {
                return _englishTitle;
            }

            return _chineseTitle;
        }

        public string GetCurrentSubTitleOrFallback()
        {
            if (InspectorLocalizationManagerSO.IsChinese)
            {
                return _chineseSubTitle;
            }

            if (InspectorLocalizationManagerSO.IsEnglish && !string.IsNullOrEmpty(_englishSubTitle))
            {
                return _englishSubTitle;
            }

            return _chineseSubTitle;
        }
    }
}
