using System.Text.RegularExpressions;

namespace Yuumix.OdinToolkits.Core
{
    public static class StringColorExtensions
    {
        /// <summary>
        /// 将字符串中的特定文本替换为带颜色的富文本格式（Unity 富文本）
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="target">需要替换的目标文本</param>
        /// <param name="hexColor">替换后的颜色（必须为 HEX 格式，如 '#FF0000'）</param>
        /// <returns>带有颜色标记的 Unity 富文本字符串</returns>
        public static string Colorize(this string source, string target, string hexColor)
        {
            if (string.IsNullOrEmpty(source) || string.IsNullOrEmpty(target) || string.IsNullOrEmpty(hexColor))
            {
                return source;
            }

            // 确保 hexColor 以 '#' 开头
            if (!hexColor.StartsWith("#"))
            {
                hexColor = "#" + hexColor;
            }

            return Regex.Replace(source, Regex.Escape(target),
                match => $"<color={hexColor}>{match.Value}</color>",
                RegexOptions.IgnoreCase);
        }

        /// <summary>
        /// 将字符串转换为Unity富文本格式的红色文本
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>Unity富文本格式的红色文本</returns>
        public static string ToRed(this string source) => $"<color=#FF0000>{source}</color>";

        /// <summary>
        /// 将字符串转换为Unity富文本格式的绿色文本
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>Unity富文本格式的绿色文本</returns>
        public static string ToGreen(this string source) => $"<color=#00FF00>{source}</color>";

        /// <summary>
        /// 将字符串转换为Unity富文本格式的蓝色文本
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>Unity富文本格式的蓝色文本</returns>
        public static string ToBlue(this string source) => $"<color=#0000FF>{source}</color>";

        /// <summary>
        /// 将字符串转换为Unity富文本格式的黄色文本
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>Unity富文本格式的黄色文本</returns>
        public static string ToYellow(this string source) => $"<color=#FFFF00>{source}</color>";

        /// <summary>
        /// 将字符串转换为Unity富文本格式的橙色文本
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>Unity富文本格式的橙色文本</returns>
        public static string ToOrange(this string source) => $"<color=#FFA500>{source}</color>";

        /// <summary>
        /// 将字符串转换为Unity富文本格式的紫色文本
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>Unity富文本格式的紫色文本</returns>
        public static string ToPurple(this string source) => $"<color=#800080>{source}</color>";

        /// <summary>
        /// 将字符串转换为Unity富文本格式的粉色文本
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>Unity富文本格式的粉色文本</returns>
        public static string ToPink(this string source) => $"<color=#FFC0CB>{source}</color>";

        /// <summary>
        /// 将字符串转换为Unity富文本格式的棕色文本
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>Unity富文本格式的棕色文本</returns>
        public static string ToBrown(this string source) => $"<color=#A52A2A>{source}</color>";

        /// <summary>
        /// 将字符串转换为Unity富文本格式的灰色文本
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>Unity富文本格式的灰色文本</returns>
        public static string ToGray(this string source) => $"<color=#808080>{source}</color>";

        /// <summary>
        /// 将字符串转换为Unity富文本格式的黑色文本
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>Unity富文本格式的黑色文本</returns>
        public static string ToBlack(this string source) => $"<color=#000000>{source}</color>";

        /// <summary>
        /// 将字符串转换为Unity富文本格式的银色文本
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>Unity富文本格式的银色文本</returns>
        public static string ToSilver(this string source) => $"<color=#C0C0C0>{source}</color>";

        /// <summary>
        /// 将字符串转换为Unity富文本格式的青色文本
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>Unity富文本格式的青色文本</returns>
        public static string ToCyan(this string source) => $"<color=#00FFFF>{source}</color>";

        /// <summary>
        /// 将字符串转换为Unity富文本格式的亮绿色文本
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>Unity富文本格式的亮绿色文本</returns>
        public static string ToLime(this string source) => $"<color=#00FF00>{source}</color>";

        /// <summary>
        /// 将字符串转换为Unity富文本格式的深绿色文本
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>Unity富文本格式的深绿色文本</returns>
        public static string ToDarkGreen(this string source) => $"<color=#006400>{source}</color>";

        /// <summary>
        /// 将字符串转换为Unity富文本格式的深红色文本
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>Unity富文本格式的深红色文本</returns>
        public static string ToDarkRed(this string source) => $"<color=#8B0000>{source}</color>";
    }
}
