using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.Common.InspectorMultiLanguage;

namespace Yuumix.OdinToolkits.Examples.CustomExtensions.CustomAttributes
{
    public class MultiLanguageButtonAndButtonExample : MonoBehaviour
    {
        public SwitchInspectorLanguageWidget widget = new SwitchInspectorLanguageWidget();

        [MultiLanguageButtonWidgetConfig(nameof(ChineseName), nameof(EnglishName), ButtonSizes.Large, ButtonStyle.Box,
            SdfIconType.Box, IconAlignment.LeftOfText, 10)]
        public MultiLanguageButtonProperty multiLanguageButtonPropertyClass =
            new MultiLanguageButtonProperty(() => Debug.Log("Localized Button"));

        public string ChineseName => "Chinese Name";
        public string EnglishName => "English Name";
    }
}
