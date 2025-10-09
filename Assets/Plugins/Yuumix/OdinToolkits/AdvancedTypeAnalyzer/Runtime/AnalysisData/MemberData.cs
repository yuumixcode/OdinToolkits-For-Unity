using System;
using System.Collections.Generic;
using System.Reflection;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Core.CompositeAttributes;

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
        IEnumerable<CustomAttributeData> CustomAttributesData { get; }

        /// <summary>
        /// 获取一个用于标识元数据元素的值
        /// </summary>
        int MetadataToken { get; }

        /// <summary>
        /// 包含此成员的模块
        /// </summary>
        Module Module { get; }
    }

    /// <summary>
    /// 解析成员数据的基类
    /// </summary>
    [Serializable]
    public abstract class MemberData : IMemberData
    {
        public static readonly DefaultAttributeFilter DefaultAttributeFilter = new DefaultAttributeFilter(new[]
        {
            typeof(SummaryAttribute)
        });

        protected MemberData(MemberInfo methodInfo, IAttributeFilter filter = null)
        {
            Name = methodInfo.Name;
            IsObsolete = methodInfo.IsDefined(typeof(ObsoleteAttribute), false);
            DeclaringType = methodInfo.DeclaringType;
            ReflectedType = methodInfo.ReflectedType;
            // ---
            SummaryAttributeValue = methodInfo.GetCustomAttribute<SummaryAttribute>()?.GetSummary();
            AttributesDeclaration = methodInfo.GetAttributesDeclarationWithMultiLine(filter ?? DefaultAttributeFilter);
            // ---
            MetadataToken = methodInfo.MetadataToken;
            Module = methodInfo.Module;
            CustomAttributesData = methodInfo.GetCustomAttributesData();
        }

        [ShowProperty] public string Name { get; }
        [ShowProperty] public bool IsObsolete { get; }
        public Type DeclaringType { get; }
        [ShowProperty] public string DeclaringTypeName => DeclaringType?.GetReadableTypeName();
        [ShowProperty] public string DeclaringTypeFullName => DeclaringType?.GetReadableTypeName(true);
        public Type ReflectedType { get; }
        [ShowProperty] public string ReflectedTypeName => ReflectedType?.GetReadableTypeName();
        [ShowProperty] public string ReflectedTypeFullName => ReflectedType?.GetReadableTypeName(true);
        [ShowProperty] public string AttributesDeclaration { get; }
        [ShowProperty] public string SummaryAttributeValue { get; }
        [ShowProperty] public IEnumerable<CustomAttributeData> CustomAttributesData { get; }
        [ShowProperty] public int MetadataToken { get; }
        [ShowProperty] public Module Module { get; }
    }
}
