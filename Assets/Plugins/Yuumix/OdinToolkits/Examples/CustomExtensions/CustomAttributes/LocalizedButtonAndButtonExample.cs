using AwesomeAttributes;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.Serialization;
using Yuumix.OdinToolkits.Common.InspectorLocalization.Attributes.WidgetConfigs;
using Yuumix.OdinToolkits.Common.InspectorLocalization.GUIWidgets;
using Yuumix.OdinToolkits.Modules.CustomExtensions.Attributes;
using Yuumix.OdinToolkits.Modules.CustomExtensions.Classes.InspectorGUIWidgets;

namespace Yuumix.OdinToolkits.Examples.CustomExtensions.CustomAttributes
{
    public class LocalizedButtonAndButtonExample : MonoBehaviour
    {
        public SwitchInspectorLanguageWidget widget = new SwitchInspectorLanguageWidget();

        [LocalizedButtonWidgetConfig(nameof(ChineseName), nameof(EnglishName), ButtonSizes.Large, ButtonStyle.Box,
            SdfIconType.Box, IconAlignment.LeftOfText, 10)]
        public LocalizedButtonWidget localizedButtonWidgetClass = new LocalizedButtonWidget(() => Debug.Log("Localized Button"));

        public string ChineseName => "Chinese Name";
        public string EnglishName => "English Name";
    }
}
