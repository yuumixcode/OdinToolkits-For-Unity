namespace Yuumix.OdinToolkits.Common.InspectorLocalization.Structs
{
    public readonly struct MultiLanguageTitleData : IGetLocalizationManager
    {
        readonly string _chineseTitle;
        readonly string _englishTitle;
        readonly string _chineseSubTitle;
        readonly string _englishSubTitle;

        public InspectorLocalizationManagerSO GetLocalizationManager() => InspectorLocalizationManagerSO.Instance;

        public MultiLanguageTitleData(string chineseTitle, string englishTitle = null, string chineseSubTitle = null,
            string englishSubTitle = null)
        {
            _chineseTitle = chineseTitle;
            _englishTitle = englishTitle ?? string.Empty;
            _chineseSubTitle = chineseSubTitle ?? string.Empty;
            _englishSubTitle = englishSubTitle ?? string.Empty;
        }

        public string GetCurrentTitleOrFallback()
        {
            var localizationManager = GetLocalizationManager();

            if (localizationManager.IsChinese)
            {
                return _chineseTitle;
            }

            if (localizationManager.IsEnglish && !string.IsNullOrEmpty(_englishTitle))
            {
                return _englishTitle;
            }

            return _chineseTitle;
        }

        public string GetCurrentSubTitleOrFallback()
        {
            var localizationManager = GetLocalizationManager();

            if (localizationManager.IsChinese)
            {
                return _chineseSubTitle;
            }

            if (localizationManager.IsEnglish && !string.IsNullOrEmpty(_englishSubTitle))
            {
                return _englishSubTitle;
            }

            return _chineseSubTitle;
        }
    }
}
