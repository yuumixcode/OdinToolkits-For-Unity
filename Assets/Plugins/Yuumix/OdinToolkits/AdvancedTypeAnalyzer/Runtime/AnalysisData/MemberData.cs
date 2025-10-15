using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Yuumix.OdinToolkits.Core;

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
        /// 声明类型的名称
        /// </summary>
        string DeclaringTypeName { get; }

        /// <summary>
        /// 声明类型的完整名称，包括命名空间
        /// </summary>
        string DeclaringTypeFullName { get; }

        /// <summary>
        /// 通过反射获取该成员的类型
        /// </summary>
        Type ReflectedType { get; }

        /// <summary>
        /// 通过反射获取该成员的类型名称
        /// </summary>
        string ReflectedTypeName { get; }

        /// <summary>
        /// 通过反射获取该成员的类型的完整名称，包括命名空间
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
        /// 成员是否从继承中获取，这里的成员不包括 Type 类型
        /// </summary>
        bool IsFromInheritance { get; }
    }

    /// <summary>
    /// 解析成员数据的基类
    /// </summary>
    [Serializable]
    public abstract class MemberData : IMemberData
    {
        /// <summary>
        /// 默认特性过滤器，被过滤的特性不会包含在 AttributesDeclaration 中
        /// </summary>
        public static readonly DefaultAttributeFilter DefaultAttributeFilter = new DefaultAttributeFilter(new[]
        {
            typeof(SummaryAttribute)
        });

        protected MemberData(MemberInfo memberInfo, IAttributeFilter filter = null)
        {
            Name = memberInfo.Name;
            IsObsolete = memberInfo.IsDefined(typeof(ObsoleteAttribute), false);
            DeclaringType = memberInfo.DeclaringType;
            DeclaringTypeName = DeclaringType?.GetReadableTypeName();
            DeclaringTypeFullName = DeclaringType?.GetReadableTypeName(true);
            ReflectedType = memberInfo.ReflectedType;
            ReflectedTypeName = ReflectedType?.GetReadableTypeName();
            ReflectedTypeFullName = ReflectedType?.GetReadableTypeName(true);
            AttributesDeclaration = memberInfo.GetAttributesDeclarationWithMultiLine(filter ?? DefaultAttributeFilter);
            SummaryAttributeValue = memberInfo.GetCustomAttribute<SummaryAttribute>()?.GetSummary();
            IsFromInheritance = memberInfo.IsFromInheritance();
            // PostProcess
            if (memberInfo is Type type)
            {
                ImplementIsType = true;
                Name = type.GetReadableTypeName();
            }
            else if (memberInfo is ConstructorInfo)
            {
                ImplementIsConstructor = true;
                Name = DeclaringTypeName?.Split('<')[0];
            }
            else if (memberInfo is MethodInfo)
            {
                ImplementIsMethod = true;
            }
        }

        #region IMemberData Members

        [BilingualText("成员名", nameof(Name))]
        [ShowEnableProperty]
        public string Name { get; }

        [BilingualText("是否为过时成员", nameof(IsObsolete))]
        [ShowEnableProperty]
        public bool IsObsolete { get; }

        public Type DeclaringType { get; }
        public string DeclaringTypeName { get; }

        [ShowEnableProperty]
        [BilingualText("声明成员的类的完整名称", nameof(DeclaringTypeFullName))]
        [HideIf("ImplementIsType")]
        public string DeclaringTypeFullName { get; }

        public Type ReflectedType { get; }
        public string ReflectedTypeName { get; }
        public string ReflectedTypeFullName { get; }
        public string AttributesDeclaration { get; }

        [PropertyOrder(100)]
        [ShowEnableProperty]
        [BilingualTitle("Summary 注释", nameof(SummaryAttributeValue))]
        [HideLabel]
        [MultiLineProperty]
        public string SummaryAttributeValue { get; }

        public bool IsFromInheritance { get; }

        #endregion

        #region 辅助显示

        bool ImplementIsType { get; }

        bool ImplementIsConstructor { get; }

        bool ImplementIsMethod { get; }

        #endregion
    }
}
