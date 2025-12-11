using System;
using System.Reflection;
using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.ScriptDocGenerator
{
    [Summary("解析成员数据的基类")]
    [Serializable]
    public abstract class MemberData : IMemberData
    {
        [Summary("默认特性过滤器，被过滤的特性不会包含在 AttributesDeclaration 中")]
        public static readonly DefaultAttributeFilter DefaultAttributeFilter = new DefaultAttributeFilter(
            new[]
            {
                typeof(SummaryAttribute)
            });

        bool _implementIsConstructor;
        bool _implementIsMethod;
        bool _implementIsType;

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
            AttributesDeclaration =
                memberInfo.GetAttributesDeclarationWithMultiLine(filter ?? DefaultAttributeFilter);
            SummaryAttributeValue = memberInfo.GetCustomAttribute<SummaryAttribute>()
                ?.GetSummary();
            IsFromInheritance = memberInfo.IsFromInheritance();
            // PostProcess
            if (memberInfo is Type type)
            {
                Name = type.GetReadableTypeName();
            }
            else if (memberInfo is ConstructorInfo)
            {
                Name = DeclaringTypeName?.Split('<')[0];
            }
        }

        #region IMemberData Members

        [BilingualText("成员名", nameof(Name))]
        [ShowEnableProperty]
        [Summary("成员名称")]
        public string Name { get; }

        [BilingualText("是否为过时成员", nameof(IsObsolete))]
        [ShowEnableProperty]
        [Summary("是否已过时")]
        public bool IsObsolete { get; }

        [Summary("声明此成员的类型")]
        public Type DeclaringType { get; }

        [Summary("声明类型的名称")]
        public string DeclaringTypeName { get; }

        [ShowEnableProperty]
        [BilingualText("声明成员的类的完整名称", nameof(DeclaringTypeFullName))]
        [HideIf("_implementIsType")]
        [Summary("声明类型的完整名称，包括命名空间")]
        public string DeclaringTypeFullName { get; }

        [Summary("通过反射获取该成员的类型")]
        public Type ReflectedType { get; }

        [Summary("通过反射获取该成员的类型名称")]
        public string ReflectedTypeName { get; }

        [Summary("通过反射获取该成员的类型的完整名称，包括命名空间")]
        public string ReflectedTypeFullName { get; }

        [Summary("特性声明字符串")]
        public string AttributesDeclaration { get; }

        [PropertyOrder(100)]
        [ShowEnableProperty]
        [BilingualTitle("Summary 注释", nameof(SummaryAttributeValue))]
        [HideLabel]
        [MultiLineProperty]
        [Summary("注释")]
        public string SummaryAttributeValue { get; }

        [Summary("成员是否从继承中获取，这里的成员不包括 Type 类型")]
        public bool IsFromInheritance { get; }

        #endregion
    }
}
