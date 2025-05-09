using Yuumix.OdinToolkits.Common.Editor;

namespace Yuumix.OdinToolkits.Modules.Odin.AttributeOverviewPro.Editor
{
    public class AnalysisData
    {
        private const string URLNameEn = "Documentation";
        private const string URLNameCn = "官方文档";
        private readonly string _attributeName;
        private readonly string _documentationURL;
        private readonly string _descriptionEn;
        private readonly string _descriptionCn;

        public AnalysisData(string attributeName, string documentationURL, string descriptionEn,
            string descriptionCn)
        {
            _attributeName = attributeName;
            _documentationURL = documentationURL;
            _descriptionEn = descriptionEn;
            _descriptionCn = descriptionCn;
        }

        public string GetName() => _attributeName;
        public string GetURL() => _documentationURL;

        public string GetURLName() => LanguageSwitchSO.Instance.CurrentLanguage == Language.Cn
            ? URLNameCn
            : URLNameEn;

        public string GetDescription() => LanguageSwitchSO.Instance.CurrentLanguage == Language.Cn
            ? _descriptionCn
            : _descriptionEn;
    }
}
