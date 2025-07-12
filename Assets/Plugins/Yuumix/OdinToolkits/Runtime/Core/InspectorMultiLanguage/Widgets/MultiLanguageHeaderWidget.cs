using System;
using System.Diagnostics;
using Yuumix.OdinToolkits.Core;
using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.LowLevel;
using Yuumix.OdinToolkits.Shared;

namespace Yuumix.OdinToolkits.Core
{
    /// <summary>
    /// 多语言的头部说明控件，用于模块的简单介绍
    /// </summary>
    [Serializable]
    [InlineProperty]
    [HideLabel]
    [MultiLanguageComment("多语言的头部说明控件，用于模块的简单介绍",
        "Multi-language header widget, used for module description")]
    public class MultiLanguageHeaderWidget
    {
        InspectorMultiLanguageManagerSO MultiLanguageManager => InspectorMultiLanguageManagerSO.Instance;

        public MultiLanguageHeaderWidget(string chineseName, string englishName, string chineseIntroduction = null,
            string englishIntroduction = null, string targetUrl = null)
        {
            headerName = new MultiLanguageDisplayAsStringWidget(chineseName, englishName);
            _chineseIntroduction = chineseIntroduction;
            _englishIntroduction = englishIntroduction;
            headerIntroduction = new MultiLanguageDisplayAsStringWidget(_chineseIntroduction, _englishIntroduction);
            _targetUrl = targetUrl ?? "https://www.odintoolkits.cn/";
        }

        string _chineseIntroduction;
        string _englishIntroduction;
        string _targetUrl;

        bool HideHeaderIntroduction => string.IsNullOrWhiteSpace(_chineseIntroduction) &&
                                       string.IsNullOrWhiteSpace(_englishIntroduction);

        [PropertyOrder(-5)]
        [HorizontalGroup("B/Left")]
        [OnInspectorGUI]
        public void HorizontalLeft() { }

        [PropertyOrder(0)]
        [BoxGroup("B", false)]
        [HorizontalGroup("B/Middle", PaddingLeft = 3f, PaddingRight = 3f)]
        [VerticalGroup("B/Middle/1", PaddingTop = 5f)]
        [HorizontalGroup("B/Middle/1/Top", 0.8f)]
        [MultiLanguageDisplayAsStringWidgetConfig(false, TextAlignment.Left, 30)]
        public MultiLanguageDisplayAsStringWidget headerName;

        [PropertyOrder(5)]
        [BoxGroup("B")]
        [HorizontalGroup("B/Middle")]
        [VerticalGroup("B/Middle/1")]
        [HorizontalGroup("B/Middle/1/Top")]
        [VerticalGroup("B/Middle/1/Top/Btn")]
        [MultiLanguageButton("中文", "English", buttonHeight: 22,
            icon: SdfIconType.Translate)]
        [Conditional("UNITY_EDITOR")]
        void SwitchLanguage()
        {
            MultiLanguageManager.CurrentLanguage =
                InspectorMultiLanguageManagerSO.IsChinese ? LanguageType.English : LanguageType.Chinese;
        }

        [PropertyOrder(10)]
        [BoxGroup("B")]
        [HorizontalGroup("B/Middle")]
        [VerticalGroup("B/Middle/1")]
        [HorizontalGroup("B/Middle/1/Top")]
        [VerticalGroup("B/Middle/1/Top/Btn")]
        [MultiLanguageButton("文档", "Documentation", buttonHeight: 22,
            icon: SdfIconType.Link45deg)]
        [PropertySpace(3)]
        public void OpenUrl()
        {
            string validatedUrl = UrlUtil.ValidateAndNormalizeUrl(_targetUrl, OdinToolkitsWebLinks.HOME);
            Application.OpenURL(validatedUrl);
        }

        [PropertyOrder(30)]
        [HideIf(nameof(HideHeaderIntroduction))]
        [BoxGroup("B")]
        [HorizontalGroup("B/Middle")]
        [VerticalGroup("B/Middle/1", PaddingBottom = 5f)]
        [MultiLanguageDisplayAsStringWidgetConfig(false, TextAlignment.Left, 13, true)]
        public MultiLanguageDisplayAsStringWidget headerIntroduction;
    }
}
