namespace Yuumix.OdinToolkits.Common.InspectorLocalization
{

    public readonly struct MultipleLanguageData
    {
        readonly string _chinese;
        readonly string _english;

        public MultipleLanguageData(string chinese, string english = null)
        {
            _chinese = chinese;
            _english = english ?? string.Empty;
        }

        public string GetCurrentOrFallback()
        {
            if (InspectorLocalizationManagerSO.IsChinese)
            {
                return _chinese;
            }

            if (InspectorLocalizationManagerSO.IsEnglish && !string.IsNullOrEmpty(_english))
            {
                return _english;
            }

            return _chinese;
        }
    }
}
