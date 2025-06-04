using AwesomeAttributes;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using UnityEngine.Serialization;
using Yuumix.OdinToolkits.Modules.CustomExtensions.Attributes;
using Yuumix.OdinToolkits.Modules.CustomExtensions.Classes.InspectorGUI;

namespace Yuumix.OdinToolkits.Examples.CustomExtensions.CustomAttributes
{
    public class LocalizedButtonAndButtonExample : MonoBehaviour
    {
        public SwitchEditorLanguageButton button = new SwitchEditorLanguageButton();

        [LocalizedButtonConfig(nameof(ChineseName), nameof(EnglishName), ButtonSizes.Large, ButtonStyle.Box,
            SdfIconType.Box, IconAlignment.LeftOfText, 10)]
        public LocalizedButton localizedButtonClass = new LocalizedButton(() => Debug.Log("Localized Button"));

        public string ChineseName => "Chinese Name";
        public string EnglishName => "English Name";
    }
}
