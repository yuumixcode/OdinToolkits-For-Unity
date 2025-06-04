using Sirenix.OdinInspector;
using System;
using Yuumix.OdinToolkits.Common.EditorLocalization;
using Yuumix.OdinToolkits.Modules.CustomExtensions.Classes.InspectorGUI;
using Yuumix.OdinToolkits.Modules.CustomExtensions.Structs;

namespace Yuumix.OdinToolkits.Modules.CustomExtensions.Attributes
{
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public class LocalizedButtonConfigAttribute : Attribute
    {
        public ButtonAttributeConfig Config;

        public LocalizedButtonConfigAttribute(string chineseName = null, string englishName = null,
            ButtonSizes buttonSize = ButtonSizes.Medium, ButtonStyle style = ButtonStyle.Box,
            SdfIconType icon = SdfIconType.None, IconAlignment buttonIconAlignment = IconAlignment.LeftOfText,
            int buttonHeight = -1)
        {
            Config = new ButtonAttributeConfig(chineseName, englishName, buttonSize, style, icon,
                buttonIconAlignment, buttonHeight);
        }
    }
}
