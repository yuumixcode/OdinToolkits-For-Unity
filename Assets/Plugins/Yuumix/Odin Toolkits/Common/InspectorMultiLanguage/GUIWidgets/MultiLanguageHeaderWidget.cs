using Sirenix.OdinInspector;
using System;
using System.Diagnostics;
using UnityEngine;
using Yuumix.OdinToolkits.Modules.Utilities;

namespace Yuumix.OdinToolkits.Common.InspectorMultiLanguage
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
        public MultiLanguageHeaderWidget(string chineseName, string englishName, string chineseIntroduction = null,
            string englishIntroduction = null, string targetUrl = null)
        {
            headerName = new MultiLanguageDisplayAsStringWidget(chineseName, englishName);
            _targetUrl = targetUrl ?? "https://www.odintoolkits.cn/";
            _chineseIntroduction = chineseIntroduction;
            _englishIntroduction = englishIntroduction;
            headerIntroduction = new MultiLanguageDisplayAsStringWidget(_chineseIntroduction, _englishIntroduction);
        }

        string _chineseIntroduction;
        string _englishIntroduction;
        string _targetUrl;
        bool HideHeaderIntroduction => _chineseIntroduction == null && _englishIntroduction == null;

        [BoxGroup("Header", false)]
        [HorizontalGroup("Header/Hori", 0.75f)]
        [VerticalGroup("Header/Hori/L")]
        [MultiLanguageDisplayWidgetConfig(false, TextAlignment.Left, 30)]
        [PropertySpace(5)]
        public MultiLanguageDisplayAsStringWidget headerName;

        [BoxGroup("Header")]
        [HorizontalGroup("Header/Hori", 0.24f)]
        [VerticalGroup("Header/Hori/R")]
        [PropertySpace(5)]
        [MultiLanguageButtonWidgetConfig("切换为英语", "Switch To Chinese", buttonHeight: 22,
            icon: SdfIconType.Translate)]
        public MultiLanguageButtonWidget switchEditorLanguage = new MultiLanguageButtonWidget(SwitchLanguage);

        [BoxGroup("Header")]
        [HorizontalGroup("Header/Hori", 0.24f)]
        [VerticalGroup("Header/Hori/R")]
        [PropertySpace(2, 5)]
        [ShowIfChinese]
        [Button("跳转文档网页", ButtonHeight = 22, Icon = SdfIconType.Link45deg)]
        [Conditional("UNITY_EDITOR")]
        public void ChineseButton()
        {
            OpenDocumentationWebsite(_targetUrl);
        }

        [BoxGroup("Header")]
        [HorizontalGroup("Header/Hori", 0.24f)]
        [VerticalGroup("Header/Hori/R")]
        [PropertySpace(2, 5)]
        [ShowIfEnglish]
        [Conditional("UNITY_EDITOR")]
        [Button("Documentation", ButtonHeight = 22, Icon = SdfIconType.Link45deg)]
        public void EnglishButton()
        {
            OpenDocumentationWebsite(_targetUrl);
        }

        [PropertyOrder(1)]
        [HideIf(nameof(HideHeaderIntroduction))]
        [BoxGroup("Header")]
        [PropertySpace(3)]
        [OnInspectorGUI]
        public void Separate() { }

        [PropertyOrder(2)]
        [HideIf(nameof(HideHeaderIntroduction))]
        [BoxGroup("Header", false)]
        [MultiLanguageDisplayWidgetConfig(false, TextAlignment.Left, 13, true)]
        public MultiLanguageDisplayAsStringWidget headerIntroduction;

        [PropertyOrder(3)]
        [BoxGroup("Header")]
        [OnInspectorGUI]
        [PropertySpace(0, 3)]
        public void Separate2() { }

        static void SwitchLanguage()
        {
            var manager = InspectorMultiLanguageManagerSO.Instance;
            manager.CurrentLanguage =
                InspectorMultiLanguageManagerSO.IsChinese ? LanguageType.English : LanguageType.Chinese;
        }

        void OpenDocumentationWebsite(string targetUrl = "")
        {
            var validatedUrl = UrlUtil.ValidateAndNormalizeUrl(targetUrl, UrlUtil.OdinToolkitsDocsUrl);
            Application.OpenURL(validatedUrl);
        }
    }
}
