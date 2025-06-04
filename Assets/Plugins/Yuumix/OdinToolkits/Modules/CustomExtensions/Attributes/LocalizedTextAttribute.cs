using Sirenix.OdinInspector;
using System;
using System.Diagnostics;

namespace Yuumix.OdinToolkits.Modules.CustomExtensions.Attributes
{
    /// <summary>
    /// 编辑器字段多语言显示，依赖 OdinToolkits，不可以直接提取到其他项目中，不可以与 Odin 的 LabelText 特性一起使用，也不推荐与其他类似的特性使用，因为可能会导致显示错误。
    /// </summary>
    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All)]
    [Conditional("UNITY_EDITOR")]
    public partial class LocalizedTextAttribute : Attribute
    {
        public readonly string SimplifiedChineseText;
        public readonly string EnglishText;
        public readonly bool NicifyEnglishText;
        public SdfIconType Icon;

        /// <summary>
        /// Supports a variety of color formats, including named colors (e.g. "red", "orange", "green", "blue"), hex
        /// codes (e.g. "#FF0000" and "#FF0000FF"), and RGBA (e.g. "RGBA(1,1,1,1)") or RGB (e.g. "RGB(1,1,1)"), including Odin
        /// attribute expressions (e.g "@this.MyColor"). Here are the available named colors: black, blue, clear, cyan, gray,
        /// green, grey, magenta, orange, purple, red, transparent, transparentBlack, transparentWhite, white, yellow, lightblue,
        /// lightcyan, lightgray, lightgreen, lightgrey, lightmagenta, lightorange, lightpurple, lightred, lightyellow, darkblue,
        /// darkcyan, darkgray, darkgreen, darkgrey, darkmagenta, darkorange, darkpurple, darkred, darkyellow.
        /// </summary>
        public string IconColor;

        public LocalizedTextAttribute(string simplifiedChinese, string english)
        {
            SimplifiedChineseText = simplifiedChinese;
            EnglishText = english;
        }

        public LocalizedTextAttribute(string simplifiedChinese, string english, bool nicifyText)
        {
            SimplifiedChineseText = simplifiedChinese;
            EnglishText = english;
            NicifyEnglishText = nicifyText;
        }
    }

    public partial class LocalizedTextAttribute
    {
        // 使用 partial 扩展多语言
    }
}
