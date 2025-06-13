using Sirenix.OdinInspector;
using System;
using System.Diagnostics;

namespace Yuumix.OdinToolkits.Common.InspectorLocalization.GUIWidgets
{
    [Serializable]
    [InlineProperty]
    [HideLabel]
    public class SwitchInspectorLanguageWidget
    {
        InspectorLocalizationManagerSO InspectorLocalizationManager => InspectorLocalizationManagerSO.Instance;
        bool IsChinese => InspectorLocalizationManager.IsChinese;
        bool IsEnglish => InspectorLocalizationManager.IsEnglish;

        [ShowIf(nameof(IsChinese), false)]
        [PropertyOrder(float.MinValue)]
        [Button("切换为英语", Icon = SdfIconType.Translate, ButtonHeight = 22)]
        [Conditional("UNITY_EDITOR")]
        public void SwitchToEnglish()
        {
            InspectorLocalizationManager.CurrentLanguage = LanguageType.English;
        }

        [ShowIf(nameof(IsEnglish), false)]
        [PropertyOrder(float.MinValue)]
        [Button("Switch To Chinese", Icon = SdfIconType.Translate, ButtonHeight = 22)]
        [Conditional("UNITY_EDITOR")]
        public void SwitchToChinese()
        {
            InspectorLocalizationManager.CurrentLanguage = LanguageType.Chinese;
        }
    }
}
