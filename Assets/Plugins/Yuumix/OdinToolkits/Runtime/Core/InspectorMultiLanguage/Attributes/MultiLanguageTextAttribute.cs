using System;
using System.Diagnostics;
using Yuumix.OdinToolkits.Core;
using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.Core
{
    /// <summary>
    /// 编辑器面板 Label 多语言显示，兼容 Odin 绘制系统
    /// </summary>
    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public class MultiLanguageTextAttribute : Attribute
    {
        public MultiLanguageData MultiLanguageData;
        public readonly bool NicifyEnglishText;
        public readonly SdfIconType Icon;

        /// <summary>
        /// Supports a variety of color formats, including named colors (e.g. "red", "orange", "green", "blue"), hex
        /// codes (e.g. "#FF0000" and "#FF0000FF"), and RGBA (e.g. "RGBA(1,1,1,1)") or RGB (e.g. "RGB(1,1,1)"), including Odin
        /// attribute expressions (e.g "@this.MyColor"). Here are the available named colors: black, blue, clear, cyan, gray,
        /// green, grey, magenta, orange, purple, red, transparent, transparentBlack, transparentWhite, white, yellow, lightblue,
        /// lightcyan, lightgray, lightgreen, lightgrey, lightmagenta, lightorange, lightpurple, lightred, lightyellow, darkblue,
        /// darkcyan, darkgray, darkgreen, darkgrey, darkmagenta, darkorange, darkpurple, darkred, darkyellow.
        /// </summary>
        public readonly string IconColor;

        public MultiLanguageTextAttribute(string chinese, string english = null, bool nicifyEnglishText = true,
            SdfIconType icon = SdfIconType.None, string iconColor = null)
        {
            MultiLanguageData = new MultiLanguageData(chinese, english);
            NicifyEnglishText = nicifyEnglishText;
            Icon = icon;
            IconColor = iconColor;
        }
    }
}
