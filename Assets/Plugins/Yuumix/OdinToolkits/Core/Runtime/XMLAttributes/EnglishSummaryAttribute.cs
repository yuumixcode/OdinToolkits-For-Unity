using System;
using System.Reflection;

namespace Yuumix.OdinToolkits.Core
{
    /// <summary>
    /// 英文注释特性，取代 XML 的 Summary 注释，用于反射时获取注释。
    /// </summary>
    /// <example>
    ///     <code>
    /// [EnglishSummary("int variable")]
    /// public int intVariable;</code>
    /// </example>
    [AttributeUsage(AttributeTargets.All)]
    public class EnglishSummaryAttribute : Attribute, ISummaryAttribute
    {
        readonly string _english;

        #region ISummaryAttribute Members

        string ISummaryAttribute.GetSummary() => _english;

        #endregion

        public string GetEnglish() => _english;

        public static string GetEnglishSummary(MemberInfo memberInfo)
        {
            var attributes = memberInfo.GetCustomAttributes(typeof(EnglishSummaryAttribute), false);
            return attributes.Length > 0 ? ((EnglishSummaryAttribute)attributes[0]).GetEnglish() : null;
        }

        public static string GetEnglishSummary(Type type)
        {
            var attributes = type.GetCustomAttributes(typeof(EnglishSummaryAttribute), false);
            return attributes.Length > 0 ? ((EnglishSummaryAttribute)attributes[0]).GetEnglish() : null;
        }
    }
}
