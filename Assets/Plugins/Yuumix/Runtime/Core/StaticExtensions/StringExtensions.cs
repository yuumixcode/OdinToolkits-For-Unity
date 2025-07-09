using System;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules.Utilities
{
    /// <summary>
    /// 字符串静态扩展类，提供丰富的字符串处理功能
    /// </summary>
    /// <remarks></remarks>
    [MultiLanguageComment("字符串静态扩展类，提供丰富的字符串处理功能", "The Static Extensions For string, provide many features")]
    public static class StringExtensions
    {
        /// <summary>
        /// 将字符串中的特定文本替换为带颜色的富文本格式（Unity 富文本）
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="target">需要替换的目标文本</param>
        /// <param name="hexColor">替换后的颜色（必须为 HEX 格式，如 '#FF0000'）</param>
        /// <returns>带有颜色标记的 Unity 富文本字符串</returns>
        [MultiLanguageComment("将字符串中的特定文本替换为带颜色的富文本格式（Unity 富文本）",
            "Replaces specific text in the string with colored rich text format (Unity Rich Text)")]
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

        #region Custom Color

        /// <summary>
        /// 将字符串转换为Unity富文本格式的红色文本
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>Unity富文本格式的红色文本</returns>
        [MultiLanguageComment("将字符串转换为Unity富文本格式的红色文本", "Converts the string to Unity rich text format in red")]
        public static string ToRed(this string source)
        {
            return $"<color=#FF0000>{source}</color>";
        }

        /// <summary>
        /// 将字符串转换为Unity富文本格式的绿色文本
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>Unity富文本格式的绿色文本</returns>
        [MultiLanguageComment("将字符串转换为Unity富文本格式的绿色文本", "Converts the string to Unity rich text format in green")]
        public static string ToGreen(this string source)
        {
            return $"<color=#00FF00>{source}</color>";
        }

        /// <summary>
        /// 将字符串转换为Unity富文本格式的蓝色文本
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>Unity富文本格式的蓝色文本</returns>
        [MultiLanguageComment("将字符串转换为Unity富文本格式的蓝色文本", "Converts the string to Unity rich text format in blue")]
        public static string ToBlue(this string source)
        {
            return $"<color=#0000FF>{source}</color>";
        }

        /// <summary>
        /// 将字符串转换为Unity富文本格式的黄色文本
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>Unity富文本格式的黄色文本</returns>
        [MultiLanguageComment("将字符串转换为Unity富文本格式的黄色文本", "Converts the string to Unity rich text format in yellow")]
        public static string ToYellow(this string source)
        {
            return $"<color=#FFFF00>{source}</color>";
        }

        /// <summary>
        /// 将字符串转换为Unity富文本格式的橙色文本
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>Unity富文本格式的橙色文本</returns>
        [MultiLanguageComment("将字符串转换为Unity富文本格式的橙色文本", "Converts the string to Unity rich text format in orange")]
        public static string ToOrange(this string source)
        {
            return $"<color=#FFA500>{source}</color>";
        }

        /// <summary>
        /// 将字符串转换为Unity富文本格式的紫色文本
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>Unity富文本格式的紫色文本</returns>
        [MultiLanguageComment("将字符串转换为Unity富文本格式的紫色文本", "Converts the string to Unity rich text format in purple")]
        public static string ToPurple(this string source)
        {
            return $"<color=#800080>{source}</color>";
        }

        /// <summary>
        /// 将字符串转换为Unity富文本格式的粉色文本
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>Unity富文本格式的粉色文本</returns>
        [MultiLanguageComment("将字符串转换为Unity富文本格式的粉色文本", "Converts the string to Unity rich text format in pink")]
        public static string ToPink(this string source)
        {
            return $"<color=#FFC0CB>{source}</color>";
        }

        /// <summary>
        /// 将字符串转换为Unity富文本格式的棕色文本
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>Unity富文本格式的棕色文本</returns>
        [MultiLanguageComment("将字符串转换为Unity富文本格式的棕色文本", "Converts the string to Unity rich text format in brown")]
        public static string ToBrown(this string source)
        {
            return $"<color=#A52A2A>{source}</color>";
        }

        /// <summary>
        /// 将字符串转换为Unity富文本格式的灰色文本
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>Unity富文本格式的灰色文本</returns>
        [MultiLanguageComment("将字符串转换为Unity富文本格式的灰色文本", "Converts the string to Unity rich text format in gray")]
        public static string ToGray(this string source)
        {
            return $"<color=#808080>{source}</color>";
        }

        /// <summary>
        /// 将字符串转换为Unity富文本格式的黑色文本
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>Unity富文本格式的黑色文本</returns>
        [MultiLanguageComment("将字符串转换为Unity富文本格式的黑色文本", "Converts the string to Unity rich text format in black")]
        public static string ToBlack(this string source)
        {
            return $"<color=#000000>{source}</color>";
        }

        /// <summary>
        /// 将字符串转换为Unity富文本格式的银色文本
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>Unity富文本格式的银色文本</returns>
        [MultiLanguageComment("将字符串转换为Unity富文本格式的银色文本", "Converts the string to Unity rich text format in silver")]
        public static string ToSilver(this string source)
        {
            return $"<color=#C0C0C0>{source}</color>";
        }

        /// <summary>
        /// 将字符串转换为Unity富文本格式的青色文本
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>Unity富文本格式的青色文本</returns>
        [MultiLanguageComment("将字符串转换为Unity富文本格式的青色文本", "Converts the string to Unity rich text format in cyan")]
        public static string ToCyan(this string source)
        {
            return $"<color=#00FFFF>{source}</color>";
        }

        /// <summary>
        /// 将字符串转换为Unity富文本格式的亮绿色文本
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>Unity富文本格式的亮绿色文本</returns>
        [MultiLanguageComment("将字符串转换为Unity富文本格式的亮绿色文本", "Converts the string to Unity rich text format in lime")]
        public static string ToLime(this string source)
        {
            return $"<color=#00FF00>{source}</color>";
        }

        /// <summary>
        /// 将字符串转换为Unity富文本格式的深绿色文本
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>Unity富文本格式的深绿色文本</returns>
        [MultiLanguageComment("将字符串转换为Unity富文本格式的深绿色文本", "Converts the string to Unity rich text format in dark green")]
        public static string ToDarkGreen(this string source)
        {
            return $"<color=#006400>{source}</color>";
        }

        /// <summary>
        /// 将字符串转换为Unity富文本格式的深红色文本
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>Unity富文本格式的深红色文本</returns>
        [MultiLanguageComment("将字符串转换为Unity富文本格式的深红色文本", "Converts the string to Unity rich text format in dark red")]
        public static string ToDarkRed(this string source)
        {
            return $"<color=#8B0000>{source}</color>";
        }

        #endregion

        /// <summary>
        /// 将字符串记录到调试控制台窗口
        /// </summary>
        /// <param name="message">要记录的消息</param>
        [MultiLanguageComment("将字符串记录到调试控制台窗口", "Logs the string to the debug console window")]
        public static void QuickLog(this string message)
        {
            Debug.Log(new StringBuilder().Append("[")
                .AppendFormat("{0:yyyy-MM-dd HH:mm:ss}", DateTime.Now)
                .Append("] ")
                .Append(message)
                .ToString());
        }

        /// <summary>
        /// 判断字符串是否为空、null或仅包含空白字符
        /// </summary>
        /// <param name="source">要检查的字符串</param>
        /// <returns>如果字符串为空、null或仅包含空白字符，则返回true；否则返回false</returns>
        [MultiLanguageComment("判断字符串是否为空、null或仅包含空白字符",
            "Determines whether the string is null, empty, or consists only of white-space characters")]
        public static bool IsNullOrWhiteSpace(this string source)
        {
            return string.IsNullOrWhiteSpace(source);
        }

        /// <summary>
        /// 判断字符串是否为空或null
        /// </summary>
        /// <param name="source">要检查的字符串</param>
        /// <returns>如果字符串为空或null，则返回true；否则返回false</returns>
        [MultiLanguageComment("判断字符串是否为空或null", "Determines whether the string is null or empty")]
        public static bool IsNullOrEmpty(this string source)
        {
            return string.IsNullOrEmpty(source);
        }

        /// <summary>
        /// 安全地截取字符串，防止越界异常
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="startIndex">开始截取的索引</param>
        /// <param name="length">要截取的最大长度</param>
        /// <returns>截取后的字符串</returns>
        [MultiLanguageComment("安全地截取字符串，防止越界异常", "Safely truncates the string to prevent out-of-bounds exceptions")]
        public static string SafeSubstring(this string source, int startIndex, int? length = null)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;

            if (startIndex >= source.Length)
                return string.Empty;

            int maxLength = source.Length - startIndex;
            if (length.HasValue && length.Value < maxLength)
                maxLength = length.Value;

            return source.Substring(startIndex, maxLength);
        }

        /// <summary>
        /// 计算字符串的MD5哈希值
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>MD5哈希值的十六进制字符串表示</returns>
        [MultiLanguageComment("计算字符串的MD5哈希值", "Calculates the MD5 hash of the string")]
        public static string ToMd5Hash(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;

            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                var inputBytes = System.Text.Encoding.UTF8.GetBytes(source);
                var hashBytes = md5.ComputeHash(inputBytes);

                return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            }
        }

        /// <summary>
        /// 将字符串转换为标题格式（每个单词首字母大写）
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>标题格式的字符串</returns>
        [MultiLanguageComment("将字符串转换为标题格式（每个单词首字母大写）",
            "Converts the string to title case (capitalizes the first letter of each word)")]
        public static string ToTitleCase(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;

            return System.Globalization.CultureInfo.CurrentCulture.TextInfo.ToTitleCase(source.ToLower());
        }

        /// <summary>
        /// 反转字符串中的字符顺序
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>反转后的字符串</returns>
        [MultiLanguageComment("反转字符串中的字符顺序", "Reverses the order of characters in the string")]
        public static string Reverse(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;

            char[] charArray = source.ToCharArray();
            Array.Reverse(charArray);
            return new string(charArray);
        }

        /// <summary>
        /// 移除字符串中的所有HTML标签
        /// </summary>
        /// <param name="source">包含HTML标签的字符串</param>
        /// <returns>不包含HTML标签的纯文本</returns>
        [MultiLanguageComment("移除字符串中的所有HTML标签", "Removes all HTML tags from the string")]
        public static string StripHtml(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;

            return Regex.Replace(source, "<.*?>", string.Empty);
        }

        /// <summary>
        /// 统计字符串中特定字符的出现次数
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="character">要统计的字符</param>
        /// <returns>字符出现的次数</returns>
        [MultiLanguageComment("统计字符串中特定字符的出现次数", "Counts the number of occurrences of a specific character in the string")]
        public static int CountOccurrences(this string source, char character)
        {
            if (string.IsNullOrEmpty(source))
                return 0;

            int count = 0;
            foreach (char c in source)
            {
                if (c == character)
                    count++;
            }

            return count;
        }

        /// <summary>
        /// 从字符串末尾移除指定数量的字符
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="count">要移除的字符数量</param>
        /// <returns>处理后的字符串</returns>
        [MultiLanguageComment("从字符串末尾移除指定数量的字符", "Removes a specified number of characters from the end of the string")]
        public static string RemoveFromEnd(this string source, int count)
        {
            if (string.IsNullOrEmpty(source) || count <= 0 || count >= source.Length)
                return source;

            return source.Substring(0, source.Length - count);
        }

        /// <summary>
        /// 从字符串开头移除指定数量的字符
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="count">要移除的字符数量</param>
        /// <returns>处理后的字符串</returns>
        [MultiLanguageComment("从字符串开头移除指定数量的字符", "Removes a specified number of characters from the start of the string")]
        public static string RemoveFromStart(this string source, int count)
        {
            if (string.IsNullOrEmpty(source) || count <= 0 || count >= source.Length)
                return source;

            return source.Substring(count);
        }

        /// <summary>
        /// 确保字符串以指定的后缀结尾
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="suffix">要检查的后缀</param>
        /// <returns>如果源字符串已包含指定后缀，则返回原字符串；否则返回添加了后缀的字符串</returns>
        [MultiLanguageComment("确保字符串以指定的后缀结尾", "Ensures the string ends with the specified suffix")]
        public static string EnsureEndsWith(this string source, string suffix)
        {
            if (string.IsNullOrEmpty(source))
                return suffix ?? string.Empty;

            if (string.IsNullOrEmpty(suffix) || source.EndsWith(suffix))
                return source;

            return source + suffix;
        }

        /// <summary>
        /// 确保字符串以指定的前缀开始
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="prefix">要检查的前缀</param>
        /// <returns>如果源字符串已包含指定前缀，则返回原字符串；否则返回添加了前缀的字符串</returns>
        [MultiLanguageComment("确保字符串以指定的前缀开始", "Ensures the string starts with the specified prefix")]
        public static string EnsureStartsWith(this string source, string prefix)
        {
            if (string.IsNullOrEmpty(source))
                return prefix ?? string.Empty;

            if (string.IsNullOrEmpty(prefix) || source.StartsWith(prefix))
                return source;

            return prefix + source;
        }

        /// <summary>
        /// 安全地将字符串转换为整数，转换失败时返回默认值
        /// </summary>
        /// <param name="source">要转换的字符串</param>
        /// <param name="defaultValue">转换失败时返回的默认值</param>
        /// <returns>转换后的整数值，或默认值</returns>
        [MultiLanguageComment("安全地将字符串转换为整数，转换失败时返回默认值",
            "Safely converts the string to an integer, returning a default value if conversion fails")]
        public static int ToInt(this string source, int defaultValue = 0)
        {
            if (int.TryParse(source, out int result))
                return result;

            return defaultValue;
        }

        /// <summary>
        /// 安全地将字符串转换为布尔值，转换失败时返回默认值
        /// </summary>
        /// <param name="source">要转换的字符串</param>
        /// <param name="defaultValue">转换失败时返回的默认值</param>
        /// <returns>转换后的布尔值，或默认值</returns>
        [MultiLanguageComment("安全地将字符串转换为布尔值，转换失败时返回默认值",
            "Safely converts the string to a boolean, returning a default value if conversion fails")]
        public static bool ToBool(this string source, bool defaultValue = false)
        {
            if (string.IsNullOrEmpty(source))
                return defaultValue;

            if (bool.TryParse(source, out bool result))
                return result;

            source = source.Trim().ToLowerInvariant();
            if (source == "1" || source == "yes" || source == "y" || source == "true" || source == "t")
                return true;

            if (source == "0" || source == "no" || source == "n" || source == "false" || source == "f")
                return false;

            return defaultValue;
        }

        /// <summary>
        /// 将字符串中的换行符替换为HTML的br标签
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>替换后的字符串</returns>
        [MultiLanguageComment("将字符串中的换行符替换为HTML的br标签", "Replaces newline characters with HTML br tags")]
        public static string NewLineToBr(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;

            return source.Replace("\r\n", "<br />").Replace("\n", "<br />").Replace("\r", "<br />");
        }

        /// <summary>
        /// 从字符串中提取所有URL
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>包含所有提取出的URL的字符串数组</returns>
        [MultiLanguageComment("从字符串中提取所有URL", "Extracts all URLs from the string")]
        public static string[] ExtractUrls(this string source)
        {
            if (string.IsNullOrEmpty(source))
                return new string[0];

            // 匹配URL的正则表达式
            string pattern = @"((http|https|ftp)://)?([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?";
            MatchCollection matches = Regex.Matches(source, pattern);

            string[] urls = new string[matches.Count];
            for (int i = 0; i < matches.Count; i++)
            {
                urls[i] = matches[i].Value;
            }

            return urls;
        }

        /// <summary>
        /// 将字符串转换为Base64编码
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="encoding">编码方式，默认为UTF-8</param>
        /// <returns>Base64编码的字符串</returns>
        [MultiLanguageComment("将字符串转换为Base64编码", "Converts the string to Base64 encoding")]
        public static string ToBase64(this string source, System.Text.Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;

            encoding = encoding ?? System.Text.Encoding.UTF8;
            byte[] bytes = encoding.GetBytes(source);
            return Convert.ToBase64String(bytes);
        }

        /// <summary>
        /// 从Base64编码的字符串还原为原始字符串
        /// </summary>
        /// <param name="source">Base64编码的字符串</param>
        /// <param name="encoding">编码方式，默认为UTF-8</param>
        /// <returns>原始字符串</returns>
        [MultiLanguageComment("从Base64编码的字符串还原为原始字符串", "Decodes a Base64 encoded string back to its original form")]
        public static string FromBase64(this string source, System.Text.Encoding encoding = null)
        {
            if (string.IsNullOrEmpty(source))
                return string.Empty;

            try
            {
                encoding = encoding ?? System.Text.Encoding.UTF8;
                byte[] bytes = Convert.FromBase64String(source);
                return encoding.GetString(bytes);
            }
            catch
            {
                return source; // 解码失败时返回原字符串
            }
        }
    }
}
