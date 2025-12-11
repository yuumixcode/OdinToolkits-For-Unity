using System;
using System.Diagnostics;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.Core.SafeEditor
{
    [Serializable]
    [InlineProperty]
    [HideLabel]
    [Summary("双语顶部说明控件，用于模块的简单介绍")]
    public class BilingualHeaderWidget
    {
        public BilingualDisplayAsStringWidget HeaderName => headerName;

        [PropertyOrder(0)]
        [PropertySpace(13)]
        [BoxGroup("OuterBox")]
        [HorizontalGroup("OuterBox/HoriTop", 0.75f)]
        [BilingualDisplayAsStringWidgetConfig(false, TextAlignment.Left, 30)]
        [SerializeField]
        BilingualDisplayAsStringWidget headerName;

        [HideIf(nameof(HideHeaderIntroduction))]
        [PropertyOrder(30)]
        [BoxGroup("OuterBox")]
        [HorizontalGroup("OuterBox/HoriBottom", 0.98f)]
        [PropertySpace(10, 8)]
        [BilingualDisplayAsStringWidgetConfig(false, TextAlignment.Left, 14, true)]
        [SerializeField]
        BilingualDisplayAsStringWidget headerIntroduction;

        string _chineseIntroduction;
        string _englishIntroduction;
        string _targetUrl;

        public BilingualHeaderWidget(string chineseName, string englishName = null,
            string chineseIntroduction = null, string englishIntroduction = null, string targetUrl = null)
        {
            headerName = new BilingualDisplayAsStringWidget(chineseName, englishName);
            _chineseIntroduction = chineseIntroduction;
            _englishIntroduction = englishIntroduction ?? chineseIntroduction;
            headerIntroduction =
                new BilingualDisplayAsStringWidget(_chineseIntroduction, _englishIntroduction);
            _targetUrl = targetUrl ?? OdinToolkitsWebLinks.OFFICIAL_WEBSITE;
        }

        static InspectorBilingualismConfigSO BilingualismConfig => InspectorBilingualismConfigSO.Instance;

        bool HideHeaderIntroduction => string.IsNullOrWhiteSpace(_chineseIntroduction) &&
                                       string.IsNullOrWhiteSpace(_englishIntroduction);

        [PropertyOrder(-10)]
        [OnInspectorGUI]
        [BoxGroup("OuterBox", ShowLabel = false)]
        [HorizontalGroup("OuterBox/HoriTop", 0.01f)]
        void PlaceholderMethod1() { }

        [PropertyOrder(5)]
        [BoxGroup("OuterBox")]
        [PropertySpace(8, 5)]
        [HorizontalGroup("OuterBox/HoriTop", 0.22f)]
        [VerticalGroup("OuterBox/HoriTop/VerRight")]
        [BilingualButton("中文", "English", buttonHeight: 24, icon: SdfIconType.Translate)]
        [Conditional("UNITY_EDITOR")]
        void SwitchLanguage()
        {
            BilingualismConfig.CurrentLanguage = InspectorBilingualismConfigSO.IsChinese
                ? InspectorBilingualismConfigSO.LanguageType.English
                : InspectorBilingualismConfigSO.LanguageType.Chinese;
        }

        [PropertyOrder(10)]
        [BoxGroup("OuterBox")]
        [HorizontalGroup("OuterBox/HoriTop", 0.22f)]
        [VerticalGroup("OuterBox/HoriTop/VerRight")]
        [BilingualButton("文档", "Documentation", buttonHeight: 24, icon: SdfIconType.Link45deg)]
        public void OpenUrl()
        {
            var validatedUrl =
                UrlUtility.ValidateAndNormalizeUrl(_targetUrl, OdinToolkitsWebLinks.OFFICIAL_WEBSITE);
            Application.OpenURL(validatedUrl);
        }

        [BoxGroup("OuterBox")]
        [HorizontalGroup("OuterBox/HoriBottom", 0.01f)]
        [OnInspectorGUI]
        void PlaceholderMethod2() { }

        public BilingualHeaderWidget ModifyWidget(string chineseName, string englishName = null,
            string chineseIntroduction = null, string englishIntroduction = null, string targetUrl = null)
        {
            headerName.ChineseDisplay = chineseName;
            headerName.EnglishDisplay = englishName ?? chineseName;
            headerIntroduction.ChineseDisplay = chineseIntroduction;
            headerIntroduction.EnglishDisplay = englishIntroduction;
            _targetUrl = targetUrl ?? OdinToolkitsWebLinks.OFFICIAL_WEBSITE;
            return this;
        }
    }
}
