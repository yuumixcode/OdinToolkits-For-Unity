using System;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.ScriptDocGenerator
{
    [Summary("成员数据接口")]
    public interface IMemberData
    {
        [Summary("成员名称")]
        string Name { get; }

        [Summary("是否已过时")]
        bool IsObsolete { get; }

        [Summary("声明此成员的类型")]
        Type DeclaringType { get; }

        [Summary("声明类型的名称")]
        string DeclaringTypeName { get; }

        [Summary("声明类型的完整名称，包括命名空间")]
        string DeclaringTypeFullName { get; }

        [Summary("通过反射获取该成员的类型")]
        Type ReflectedType { get; }

        [Summary("通过反射获取该成员的类型名称")]
        string ReflectedTypeName { get; }

        [Summary("通过反射获取该成员的类型的完整名称，包括命名空间")]
        string ReflectedTypeFullName { get; }

        [Summary("特性声明字符串")]
        string AttributesDeclaration { get; }

        [Summary("注释")]
        string SummaryAttributeValue { get; }

        [Summary("成员是否从继承中获取，这里的成员不包括 Type 类型")]
        bool IsFromInheritance { get; }
    }
}
