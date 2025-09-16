using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules
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
        string ISummaryAttribute.GetSummaryContent() => _english;
        public string GetEnglish() => _english;

        public static string GetEnglishSummary(MemberInfo memberInfo)
        {
            object[] attributes = memberInfo.GetCustomAttributes(typeof(EnglishSummaryAttribute), false);
            return attributes.Length > 0 ? ((EnglishSummaryAttribute)attributes[0]).GetEnglish() : null;
        }

        public static string GetEnglishSummary(Type type)
        {
            object[] attributes = type.GetCustomAttributes(typeof(EnglishSummaryAttribute), false);
            return attributes.Length > 0 ? ((EnglishSummaryAttribute)attributes[0]).GetEnglish() : null;
        }
    }
}
