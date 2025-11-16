using System;
using System.Security.Cryptography;
using System.Text;
using UnityEngine;

namespace Yuumix.OdinToolkits.Core
{
    /// <summary>
    /// 字符串静态扩展类，提供丰富的字符串处理功能
    /// </summary>
    /// <remarks></remarks>
    public static class StringExtensions
    {
        /// <summary>
        /// 将字符串记录到调试控制台窗口
        /// </summary>
        /// <param name="message">要记录的消息</param>
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
        public static bool IsNullOrWhiteSpace(this string source) => string.IsNullOrWhiteSpace(source);

        /// <summary>
        /// 判断字符串是否为空或null
        /// </summary>
        /// <param name="source">要检查的字符串</param>
        /// <returns>如果字符串为空或null，则返回true；否则返回false</returns>
        public static bool IsNullOrEmpty(this string source) => string.IsNullOrEmpty(source);

        /// <summary>
        /// 计算字符串的MD5哈希值
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>MD5哈希值的十六进制字符串表示</returns>
        public static string ToMd5Hash(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return string.Empty;
            }

            using (var md5 = MD5.Create())
            {
                var inputBytes = Encoding.UTF8.GetBytes(source);
                var hashBytes = md5.ComputeHash(inputBytes);

                return BitConverter.ToString(hashBytes).Replace("-", "").ToLowerInvariant();
            }
        }

        /// <summary>
        /// 确保字符串以指定的后缀结尾
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="suffix">要检查的后缀</param>
        /// <returns>如果源字符串已包含指定后缀，则返回原字符串；否则返回添加了后缀的字符串</returns>
        public static string EnsureEndsWith(this string source, string suffix)
        {
            if (string.IsNullOrEmpty(source))
            {
                return suffix ?? string.Empty;
            }

            if (string.IsNullOrEmpty(suffix) || source.EndsWith(suffix))
            {
                return source;
            }

            return source + suffix;
        }

        /// <summary>
        /// 确保字符串以指定的前缀开始
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <param name="prefix">要检查的前缀</param>
        /// <returns>如果源字符串已包含指定前缀，则返回原字符串；否则返回添加了前缀的字符串</returns>
        public static string EnsureStartsWith(this string source, string prefix)
        {
            if (string.IsNullOrEmpty(source))
            {
                return prefix ?? string.Empty;
            }

            if (string.IsNullOrEmpty(prefix) || source.StartsWith(prefix))
            {
                return source;
            }

            return prefix + source;
        }

        /// <summary>
        /// 将字符串中的换行符替换为HTML的br标签
        /// </summary>
        /// <param name="source">源字符串</param>
        /// <returns>替换后的字符串</returns>
        public static string NewLineToBr(this string source)
        {
            if (string.IsNullOrEmpty(source))
            {
                return string.Empty;
            }

            return source.Replace("\r\n", "<br />").Replace("\n", "<br />").Replace("\r", "<br />");
        }
    }
}
