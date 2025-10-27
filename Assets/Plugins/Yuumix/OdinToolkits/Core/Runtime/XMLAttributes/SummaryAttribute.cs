using System;
using System.Reflection;

namespace Yuumix.OdinToolkits.Core
{
    public interface ISummaryAttribute
    {
        string GetSummary();
    }

    /// <summary>
    /// 中文注释特性，取代 XML 的 Summary 注释，用于反射时获取注释。
    /// </summary>
    /// <example>
    ///     <code>
    /// [ChineseSummary("int 类型变量")]
    /// public int intVariable;</code>
    /// </example>
    [AttributeUsage(AttributeTargets.All)]
    public class SummaryAttribute : Attribute, ISummaryAttribute
    {
        readonly string _chinese;

        public SummaryAttribute(string chinese) => _chinese = chinese;

        public string GetSummary() => _chinese;

        public static string GetSummaryAttributeValue(MemberInfo memberInfo)
        {
            var attributes = memberInfo.GetCustomAttributes(typeof(SummaryAttribute), false);
            return attributes.Length > 0 ? ((SummaryAttribute)attributes[0]).GetSummary() : null;
        }

        public static string GetSummaryAttributeValue(Type type)
        {
            var attributes = type.GetCustomAttributes(typeof(SummaryAttribute), false);
            return attributes.Length > 0 ? ((SummaryAttribute)attributes[0]).GetSummary() : null;
        }
    }
}
