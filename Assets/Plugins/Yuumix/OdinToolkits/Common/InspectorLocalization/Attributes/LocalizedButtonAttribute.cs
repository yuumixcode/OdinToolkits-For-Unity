using Sirenix.OdinInspector;
using System;
using System.Diagnostics;
using Yuumix.OdinToolkits.Common.InspectorLocalization.Structs;

namespace Yuumix.OdinToolkits.Common.InspectorLocalization.Attributes
{
    /// <summary>
    /// 此特性可以完美兼容 Odin Inspector 的绘制系统，但是无法实时切换语言，切换语言后需要重新打开面板才能触发
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    [Conditional("UNITY_EDITOR")]
    public class LocalizedButtonAttribute : ShowInInspectorAttribute
    {
        public ButtonAttributeConfig Config;

        public LocalizedButtonAttribute(string chineseName = null, string englishName = null,
            ButtonSizes buttonSize = ButtonSizes.Medium, ButtonStyle style = ButtonStyle.Box,
            SdfIconType icon = SdfIconType.None, IconAlignment buttonIconAlignment = IconAlignment.LeftOfText,
            int buttonHeight = -1)
        {
            Config = new ButtonAttributeConfig(chineseName, englishName, buttonSize, style, icon,
                buttonIconAlignment, buttonHeight);
        }
    }

}
