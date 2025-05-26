using Sirenix.OdinInspector;
using System;

namespace Yuumix.OdinToolkits.Common.Runtime.Localization
{
    [Serializable]
    [InlineProperty]
    [HideLabel]
    public class LanguageButton
    {
#if UNITY_EDITOR
        [PropertyOrder(float.MinValue)]
        [Button("切换语言", Icon = SdfIconType.Translate, ButtonHeight = 22)]
        public virtual void SwitchLanguage()
        {
            var language = InspectorLanguageManagerSO.Instance.CurrentLanguage;
            InspectorLanguageManagerSO.Instance.CurrentLanguage = language == InspectorLanguageType.SimplifiedChinese
                ? InspectorLanguageType.English
                : InspectorLanguageType.SimplifiedChinese;
        }
#endif
    }
}
