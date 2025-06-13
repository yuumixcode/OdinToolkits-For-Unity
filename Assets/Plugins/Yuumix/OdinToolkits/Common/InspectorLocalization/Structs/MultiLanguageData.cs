namespace Yuumix.OdinToolkits.Common.InspectorLocalization.Structs
{
    public interface IGetLocalizationManager
    {
        InspectorLocalizationManagerSO GetLocalizationManager();
    }

    public readonly struct MultiLanguageData
    {
        readonly string _chinese;
        readonly string _english;

        public MultiLanguageData(string chinese, string english = null)
        {
            _chinese = chinese;
            _english = english ?? string.Empty;
        }

        static InspectorLocalizationManagerSO GetLocalizationManager() => InspectorLocalizationManagerSO.Instance;

        public string GetCurrentOrFallback()
        {
            var localizationManager = GetLocalizationManager();

            if (localizationManager.IsChinese)
            {
                return _chinese;
            }

            if (localizationManager.IsEnglish && !string.IsNullOrEmpty(_english))
            {
                return _english;
            }

            return _chinese;
        }
    }
}
