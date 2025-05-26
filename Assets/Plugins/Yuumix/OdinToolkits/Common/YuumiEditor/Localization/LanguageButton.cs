using Sirenix.OdinInspector;
using System;
#if !UNITY_EDITOR
using UnityEngine;
#endif

namespace Yuumix.OdinToolkits.Common.YuumiEditor.Localization
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
#if !UNITY_EDITOR
            Debug.Log("非编辑器阶段，按钮无效！");
#endif
        }
    }
}
