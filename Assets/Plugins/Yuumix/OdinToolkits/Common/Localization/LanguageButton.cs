using Sirenix.OdinInspector;
using System;

namespace Yuumix.OdinToolkits.Common.Localization
{
    [Serializable]
    [InlineProperty]
    [HideLabel]
    public class LanguageButton
    {
        [PropertyOrder(float.MinValue)]
        [Button("切换语言", Icon = SdfIconType.Translate, ButtonHeight = 22)]
        public void SwitchLanguage()
        {
#if UNITY_EDITOR
            var language = InspectorLanguageManagerSO.Instance.CurrentLanguage;
            InspectorLanguageManagerSO.Instance.CurrentLanguage = language == InspectorLanguageType.SimplifiedChinese
                ? InspectorLanguageType.English
                : InspectorLanguageType.SimplifiedChinese;
#endif
        }
    }
}
