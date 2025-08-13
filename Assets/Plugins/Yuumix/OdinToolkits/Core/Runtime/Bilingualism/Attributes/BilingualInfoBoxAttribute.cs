using System;
using System.Diagnostics;
using Sirenix.OdinInspector;
using Yuumix.Universal;

namespace Yuumix.OdinToolkits.Core
{
    /// <summary>
    /// 多语言的 InfoBox 特性
    /// </summary>
    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    [BilingualComment("多语言 InfoBox 特性", "Multi-language InfoBox")]
    public class BilingualInfoBoxAttribute : Attribute
    {
        public BilingualData BilingualData;

        public readonly InfoMessageType InfoMessageType;

        public readonly string VisibleIf;

        public readonly bool GUIAlwaysEnabled;

        /// <summary>
        /// Supports a variety of color formats, including named colors (e.g. "red", "orange", "green", "blue"), hex codes
        /// (e.g. "#FF0000" and "#FF0000FF"), and RGBA (e.g. "RGBA(1,1,1,1)") or RGB (e.g. "RGB(1,1,1)"), including Odin attribute
        /// expressions (e.g "@this.MyColor"). Here are the available named colors: black, blue, clear, cyan, gray, green, grey,
        /// magenta, orange, purple, red, transparent, transparentBlack, transparentWhite, white, yellow, lightblue, lightcyan,
        /// lightgray, lightgreen, lightgrey, lightmagenta, lightorange, lightpurple, lightred, lightyellow, darkblue, darkcyan,
        /// darkgray, darkgreen, darkgrey, darkmagenta, darkorange, darkpurple, darkred, darkyellow.
        /// </summary>
        public readonly string IconColor;

        public readonly SdfIconType Icon;

        public BilingualInfoBoxAttribute(string chinese, string english = null,
            InfoMessageType infoMessageType = InfoMessageType.Info, SdfIconType icon = SdfIconType.None,
            string visibleIf = "",
            string iconColor = null,
            bool guiAlwaysEnabled = false)
        {
            BilingualData = new BilingualData(chinese, english);
            InfoMessageType = infoMessageType;
            Icon = icon;
            VisibleIf = visibleIf;
            IconColor = iconColor;
            GUIAlwaysEnabled = guiAlwaysEnabled;
        }

        public bool HasDefinedIcon => Icon != SdfIconType.None;
    }
}
