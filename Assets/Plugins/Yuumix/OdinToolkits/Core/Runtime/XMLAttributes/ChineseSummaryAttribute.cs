using System;
using System.Reflection;

namespace Yuumix.OdinToolkits.Core
{
    /// <summary>
    /// 中文注释特性，取代 XML 的 Summary 注释，用于反射时获取注释。
    /// </summary>
    /// <example>
    ///     <code>
    /// [ChineseSummary("int 类型变量")]
    /// public int intVariable;</code>
    /// </example>
    [AttributeUsage(AttributeTargets.All)]
    public class ChineseSummaryAttribute : Attribute, ISummaryAttribute
    {
        readonly string _chinese;

        public ChineseSummaryAttribute(string chinese) => _chinese = chinese;

        public string GetSummaryContent() => _chinese;

        public string GetChinese() => _chinese;

        public static string GetChineseSummary(MemberInfo memberInfo)
        {
            object[] attributes = memberInfo.GetCustomAttributes(typeof(ChineseSummaryAttribute), false);
            return attributes.Length > 0 ? ((ChineseSummaryAttribute)attributes[0]).GetChinese() : null;
        }

        public static string GetChineseSummary(Type type)
        {
            object[] attributes = type.GetCustomAttributes(typeof(ChineseSummaryAttribute), false);
            return attributes.Length > 0 ? ((ChineseSummaryAttribute)attributes[0]).GetChinese() : null;
        }
    }
}
