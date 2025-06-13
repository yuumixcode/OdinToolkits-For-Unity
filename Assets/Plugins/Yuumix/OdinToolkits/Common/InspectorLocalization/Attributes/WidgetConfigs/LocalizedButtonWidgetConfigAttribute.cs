using Sirenix.OdinInspector;
using System;
using Yuumix.OdinToolkits.Common.InspectorLocalization.Structs;

namespace Yuumix.OdinToolkits.Common.InspectorLocalization.Attributes.WidgetConfigs
{
    /// <summary>
    /// 该特性只添加在类型为 LocalizedButton 的字段上，给 LocalizedButton 添加配置信息
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class LocalizedButtonWidgetConfigAttribute : Attribute
    {
        public ButtonAttributeConfig Config;

        public LocalizedButtonWidgetConfigAttribute(string chineseName = null,
            string englishName = null,
            ButtonSizes buttonSize = ButtonSizes.Medium,
            ButtonStyle style = ButtonStyle.CompactBox,
            SdfIconType icon = SdfIconType.None,
            IconAlignment buttonIconAlignment = IconAlignment.LeftOfText,
            int buttonHeight = -1,
            bool stretch = true,
            bool drawResult = true,
            bool expanded = false,
            float buttonAlignment = 0.5f,
            bool displayParameters = true,
            bool dirtyOnClick = true)
        {
            Config = new ButtonAttributeConfig(chineseName, englishName, buttonSize, style, icon,
                buttonIconAlignment, buttonHeight, stretch, drawResult, expanded, buttonAlignment, displayParameters,
                dirtyOnClick);
        }
    }
}
