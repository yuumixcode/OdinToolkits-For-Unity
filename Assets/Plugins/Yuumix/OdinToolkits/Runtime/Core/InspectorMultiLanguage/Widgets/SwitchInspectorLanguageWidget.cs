using System;
using System.Diagnostics;
using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.Core
{
    [Serializable]
    [InlineProperty]
    [HideLabel]
    public class SwitchInspectorLanguageWidget
    {
        InspectorMultiLanguageManagerSO InspectorMultiLanguageManager => InspectorMultiLanguageManagerSO.Instance;
        bool IsChinese => InspectorMultiLanguageManagerSO.IsChinese;
        bool IsEnglish => InspectorMultiLanguageManagerSO.IsEnglish;

        [ShowIf(nameof(IsChinese), false)]
        [PropertyOrder(float.MinValue)]
        [Button("中文", Icon = SdfIconType.Translate, ButtonHeight = 22)]
        [Conditional("UNITY_EDITOR")]
        public void SwitchToEnglish()
        {
            InspectorMultiLanguageManager.CurrentLanguage = LanguageType.English;
        }

        [ShowIf(nameof(IsEnglish), false)]
        [PropertyOrder(float.MinValue)]
        [Button("English", Icon = SdfIconType.Translate, ButtonHeight = 22)]
        [Conditional("UNITY_EDITOR")]
        public void SwitchToChinese()
        {
            InspectorMultiLanguageManager.CurrentLanguage = LanguageType.Chinese;
        }
    }
}
