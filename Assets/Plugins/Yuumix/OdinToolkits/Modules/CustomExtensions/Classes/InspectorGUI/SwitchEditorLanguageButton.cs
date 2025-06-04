using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using System;
using System.Diagnostics;
using Yuumix.OdinToolkits.Common.EditorLocalization;
using Debug = UnityEngine.Debug;

namespace Yuumix.OdinToolkits.Modules.CustomExtensions.Classes.InspectorGUI
{
    [Serializable]
    [InlineProperty]
    [HideLabel]
    public class SwitchEditorLanguageButton
    {
        EditorLocalizationManagerSO EditorLocalizationManager => EditorLocalizationManagerSO.Instance;
        bool IsSimplifiedChinese => EditorLocalizationManager.IsSimplifiedChinese;
        bool IsEnglish => EditorLocalizationManager.IsEnglish;

        [ShowIf(nameof(IsSimplifiedChinese), false)]
        [PropertyOrder(float.MinValue)]
        [Button("切换为英语", Icon = SdfIconType.Translate, ButtonHeight = 22)]
        [Conditional("UNITY_EDITOR")]
        public void SwitchToEnglish()
        {
            EditorLocalizationManager.CurrentLanguage = LanguageType.English;
        }

        [ShowIf(nameof(IsEnglish), false)]
        [PropertyOrder(float.MinValue)]
        [Button("Switch To SimplifiedChinese", Icon = SdfIconType.Translate, ButtonHeight = 22)]
        [Conditional("UNITY_EDITOR")]
        public void SwitchToSimplifiedChinese()
        {
            EditorLocalizationManager.CurrentLanguage = LanguageType.SimplifiedChinese;
        }
    }
}
