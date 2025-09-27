using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Core.CompositeAttributes;
using Yuumix.OdinToolkits.Modules;

namespace Yuumix.OdinToolkits.AdvancedTypeAnalyzer
{
    /// <summary>
    /// 成员数据接口
    /// </summary>
    public interface IMemberData
    {
        /// <summary>
        /// 成员名称
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 是否已过时
        /// </summary>
        bool IsObsolete { get; }

        /// <summary>
        /// 声明此成员的类型
        /// </summary>
        Type DeclaringType { get; }

        /// <summary>
        /// 声明类型的短名称
        /// </summary>
        string DeclaringTypeName { get; }

        /// <summary>
        /// 声明类型的完全限定名称
        /// </summary>
        string DeclaringTypeFullName { get; }

        /// <summary>
        /// 用于获取此 MemberInfo 实例的类对象。
        /// </summary>
        Type ReflectedType { get; }

        /// <summary>
        /// 获取此成员的反射类型的短名称。
        /// </summary>
        string ReflectedTypeName { get; }

        /// <summary>
        /// 获取此成员的反射类型的完全限定名称。
        /// </summary>
        string ReflectedTypeFullName { get; }

        /// <summary>
        /// 特性声明字符串
        /// </summary>
        string AttributesDeclaration { get; }

        /// <summary>
        /// 注释
        /// </summary>
        string SummaryAttributeValue { get; }

        /// <summary>
        /// 成员的自定义属性数据
        /// </summary>
        IEnumerable<CustomAttributeData> CustomAttributesData { get; set; }

        /// <summary>
        /// 获取一个用于标识元数据元素的值
        /// </summary>
        int MetadataToken { get; set; }

        /// <summary>
        /// 包含此成员的模块
        /// </summary>
        Module Module { get; set; }
    }

    /// <summary>
    /// 解析成员数据的基类
    /// </summary>
    [Serializable]
    public abstract class MemberData : IMemberData
    {
        /// <summary>
        /// 默认特性过滤器
        /// </summary>
        public static readonly DefaultAttributeFilter DefaultAttributeFilter = new DefaultAttributeFilter(new[]
        {
            typeof(SummaryAttribute)
        });

        protected MemberData(MemberInfo memberInfo, IAttributeFilter filter = null)
        {
            if (memberInfo == null)
            {
                throw new ArgumentNullException(nameof(memberInfo));
            }

            Name = memberInfo.Name;
            IsObsolete = memberInfo.IsDefined(typeof(ObsoleteAttribute), false);
            DeclaringType = memberInfo.DeclaringType;
            ReflectedType = memberInfo.ReflectedType;
            // ---
            SummaryAttributeValue = memberInfo.GetCustomAttribute<SummaryAttribute>()?.GetSummary();
            AttributesDeclaration = GenerateAttributesDeclaration(memberInfo, filter ?? DefaultAttributeFilter);
            // ---
            MetadataToken = memberInfo.MetadataToken;
            Module = memberInfo.Module;
            CustomAttributesData = memberInfo.GetCustomAttributesData();
        }

        [ShowProperty] public string Name { get; set; }
        [ShowProperty] public bool IsObsolete { get; set; }
        public Type DeclaringType { get; set; }
        [ShowProperty] public string DeclaringTypeName => DeclaringType?.GetReadableTypeName();
        [ShowProperty] public string DeclaringTypeFullName => DeclaringType?.GetReadableTypeName(true);
        [ShowProperty] public Type ReflectedType { get; set; }
        [ShowProperty] public string ReflectedTypeName => ReflectedType?.GetReadableTypeName();
        [ShowProperty] public string ReflectedTypeFullName => ReflectedType?.GetReadableTypeName(true);
        [ShowProperty] public string AttributesDeclaration { get; set; }
        [ShowProperty] public string SummaryAttributeValue { get; set; }
        [ShowProperty] public IEnumerable<CustomAttributeData> CustomAttributesData { get; set; }
        [ShowProperty] public int MetadataToken { get; set; }
        [ShowProperty] public Module Module { get; set; }

        #region 静态方法

        /// <summary>
        /// 生成特性声明字符串
        /// </summary>
        /// <param name="member">成员信息</param>
        /// <param name="filter">特性过滤器</param>
        /// <returns>特性声明字符串</returns>
        static string GenerateAttributesDeclaration(MemberInfo member, IAttributeFilter filter = null)
        {
            object[] attributes = member.GetCustomAttributes(false);
            if (attributes.Length == 0)
            {
                return string.Empty;
            }

            var attributesStringBuilder = new StringBuilder();
            foreach (object attr in attributes)
            {
                Type attributeType = attr.GetType();
                if (filter != null && filter.ShouldFilterOut(attributeType))
                {
                    continue;
                }

                string attributeName = attributeType.Name;
                if (attributeName.EndsWith("Attribute"))
                {
                    attributeName = attributeName[..^"Attribute".Length];
                }

                attributesStringBuilder.AppendLine($"[{attributeName}]");
            }

            return attributesStringBuilder.ToString();
        }

        #endregion
    }
}
