using System;
using System.Diagnostics;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.Core
{
    /// <summary>
    /// 双语头部说明控件，用于模块的简单介绍
    /// </summary>
    [Serializable]
    [InlineProperty]
    [HideLabel]
    public class BilingualHeaderWidget
    {
        BilingualSetting MultiLanguageManager => OdinToolkitsPreferencesSO.Instance.bilingualSetting;

        public BilingualHeaderWidget(string chineseName, string englishName = null,
            string chineseIntroduction = null,
            string englishIntroduction = null, string targetUrl = null)
        {
            headerName = new BilingualDisplayAsStringWidget(chineseName, englishName);
            _chineseIntroduction = chineseIntroduction;
            _englishIntroduction = englishIntroduction ?? chineseIntroduction;
            headerIntroduction = new BilingualDisplayAsStringWidget(_chineseIntroduction, _englishIntroduction);
            _targetUrl = targetUrl ?? OdinToolkitsWebLinks.HOME;
        }

        string _chineseIntroduction;
        string _englishIntroduction;
        string _targetUrl;

        bool HideHeaderIntroduction => string.IsNullOrWhiteSpace(_chineseIntroduction) &&
                                       string.IsNullOrWhiteSpace(_englishIntroduction);

        [PropertyOrder(0)]
        [BoxGroup("B", false)]
        [HorizontalGroup("B/Middle", PaddingLeft = 3f, PaddingRight = 3f)]
        [VerticalGroup("B/Middle/1", PaddingTop = 5f)]
        [HorizontalGroup("B/Middle/1/Top", 0.8f)]
        [BilingualDisplayAsStringWidgetConfig(false, TextAlignment.Left, 30)]
        public BilingualDisplayAsStringWidget headerName;

        [PropertyOrder(5)]
        [BoxGroup("B")]
        [HorizontalGroup("B/Middle")]
        [VerticalGroup("B/Middle/1")]
        [HorizontalGroup("B/Middle/1/Top")]
        [VerticalGroup("B/Middle/1/Top/Btn")]
        [BilingualButton("中文", "English", buttonHeight: 22,
            icon: SdfIconType.Translate)]
        [Conditional("UNITY_EDITOR")]
        void SwitchLanguage()
        {
            MultiLanguageManager.CurrentLanguage =
                BilingualSetting.IsChinese ? LanguageType.English : LanguageType.Chinese;
        }

        [PropertyOrder(10)]
        [BoxGroup("B")]
        [HorizontalGroup("B/Middle")]
        [VerticalGroup("B/Middle/1")]
        [HorizontalGroup("B/Middle/1/Top")]
        [VerticalGroup("B/Middle/1/Top/Btn")]
        [BilingualButton("文档", "Documentation", buttonHeight: 22,
            icon: SdfIconType.Link45deg)]
        [PropertySpace(3)]
        public void OpenUrl()
        {
            string validatedUrl = UrlUtility.ValidateAndNormalizeUrl(_targetUrl, OdinToolkitsWebLinks.HOME);
            Application.OpenURL(validatedUrl);
        }

        [PropertyOrder(30)]
        [HideIf(nameof(HideHeaderIntroduction))]
        [BoxGroup("B")]
        [HorizontalGroup("B/Middle")]
        [VerticalGroup("B/Middle/1", PaddingBottom = 5f)]
        [BilingualDisplayAsStringWidgetConfig(false, TextAlignment.Left, 13, true)]
        public BilingualDisplayAsStringWidget headerIntroduction;

        public BilingualHeaderWidget ModifyWidget(string chineseName, string englishName = null,
            string chineseIntroduction = null,
            string englishIntroduction = null, string targetUrl = null)
        {
            headerName.ChineseDisplay = chineseName;
            headerName.EnglishDisplay = englishName ?? chineseName;
            headerIntroduction.ChineseDisplay = chineseIntroduction;
            headerIntroduction.EnglishDisplay = englishIntroduction;
            _targetUrl = targetUrl ?? "https://www.odintoolkits.cn/";
            return this;
        }
    }
}
