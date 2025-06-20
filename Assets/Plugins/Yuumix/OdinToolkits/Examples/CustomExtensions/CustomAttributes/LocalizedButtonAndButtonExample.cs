using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.Common.InspectorLocalization;

namespace Yuumix.OdinToolkits.Examples.CustomExtensions.CustomAttributes
{
    public class LocalizedButtonAndButtonExample : MonoBehaviour
    {
        public SwitchInspectorLanguageWidget widget = new SwitchInspectorLanguageWidget();

        [LocalizedButtonWidgetConfig(nameof(ChineseName), nameof(EnglishName), ButtonSizes.Large, ButtonStyle.Box,
            SdfIconType.Box, IconAlignment.LeftOfText, 10)]
        public LocalizedButtonWidget localizedButtonWidgetClass =
            new LocalizedButtonWidget(() => Debug.Log("Localized Button"));

        public string ChineseName => "Chinese Name";
        public string EnglishName => "English Name";
    }
}
