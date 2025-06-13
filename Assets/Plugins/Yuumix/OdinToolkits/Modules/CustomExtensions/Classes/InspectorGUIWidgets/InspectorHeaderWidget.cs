using Sirenix.OdinInspector;
using System;
using System.Diagnostics;
using UnityEngine;
using Yuumix.OdinToolkits.Common.InspectorLocalization;
using Yuumix.OdinToolkits.Common.InspectorLocalization.Attributes.WidgetConfigs;
using Yuumix.OdinToolkits.Common.InspectorLocalization.GUIWidgets;
using Yuumix.OdinToolkits.Modules.Utilities.Runtime;

namespace Yuumix.OdinToolkits.Modules.CustomExtensions.Classes.InspectorGUIWidgets
{
    [PropertyOrder(float.MinValue)]
    [Serializable]
    [InlineProperty]
    [HideLabel]
    public class InspectorHeaderWidget
    {
        public InspectorHeaderWidget(string chineseName, string englishName, string chineseIntroduction = null,
            string englishIntroduction = null, string targetUrl = null)
        {
            headerName = new LocalizedDisplayAsStringWidget(chineseName, englishName);
            _targetUrl = targetUrl;
            _chineseIntroduction = chineseIntroduction;
            _englishIntroduction = englishIntroduction;
            headerIntroduction = new LocalizedDisplayAsStringWidget(_chineseIntroduction, _englishIntroduction);
        }

        string _chineseIntroduction;
        string _englishIntroduction;
        string _targetUrl;
        bool HideHeaderIntroduction => _chineseIntroduction == null && _englishIntroduction == null;
        static InspectorLocalizationManagerSO Manager => InspectorLocalizationManagerSO.Instance;
        public bool IsChinese => Manager.IsChinese;
        public bool IsEnglish => Manager.IsEnglish;

        [BoxGroup("Header", false)]
        [HorizontalGroup("Header/Hori", 0.75f)]
        [VerticalGroup("Header/Hori/L")]
        [LocalizedDisplayWidgetConfig(false, TextAlignment.Left, 30)]
        [PropertySpace(5)]
        public LocalizedDisplayAsStringWidget headerName;

        [BoxGroup("Header")]
        [HorizontalGroup("Header/Hori", 0.24f)]
        [VerticalGroup("Header/Hori/R")]
        [PropertySpace(5)]
        [LocalizedButtonWidgetConfig("切换为英语", "Switch To English", buttonHeight: 22,
            icon: SdfIconType.Translate)]
        public LocalizedButtonWidget switchEditorLanguage = new LocalizedButtonWidget(SwitchLanguage);

        [BoxGroup("Header")]
        [HorizontalGroup("Header/Hori", 0.24f)]
        [VerticalGroup("Header/Hori/R")]
        [PropertySpace(2, 5)]
        [ShowIf(nameof(IsChinese), false)]
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
        [ShowIf(nameof(IsEnglish), false)]
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
        [LocalizedDisplayWidgetConfig(false, TextAlignment.Left, 14, true)]
        public LocalizedDisplayAsStringWidget headerIntroduction;

        [PropertyOrder(3)]
        [BoxGroup("Header")]
        [OnInspectorGUI]
        [PropertySpace(0, 3)]
        public void Separate2() { }

        static void SwitchLanguage()
        {
            var manager = InspectorLocalizationManagerSO.Instance;
            manager.CurrentLanguage = manager.IsChinese ? LanguageType.English : LanguageType.Chinese;
        }

        void OpenDocumentationWebsite(string targetUrl = "")
        {
            var validatedUrl = UrlUtil.ValidateAndNormalizeUrl(targetUrl, UrlUtil.OdinToolkitsDocsUrl);
            Application.OpenURL(validatedUrl);
        }
    }
}
