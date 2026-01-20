using System;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.ScriptDocGenerator
{
    [Summary("方法数据接口，继承自 IDerivedMemberData")]
    public interface IMethodData : IDerivedMemberData
    {
        /// <summary>
        /// 方法的返回类型
        /// </summary>
        [Summary("方法的返回类型")]
        Type ReturnType { get; }

        /// <summary>
        /// 方法的返回类型名称
        /// </summary>
        [Summary("方法的返回类型名称")]
        string ReturnTypeName { get; }

        /// <summary>
        /// 方法的返回类型的完整名称
        /// </summary>
        [Summary("方法的返回类型的完整名称")]
        string ReturnTypeFullName { get; }

        /// <summary>
        /// 是否带有抽象属性，不一定声明时带有 abstract 关键字
        /// </summary>
        [Summary("是否带有抽象属性，不一定声明时带有 abstract 关键字")]
        bool IsAbstract { get; }

        /// <summary>
        /// 是否带有虚方法属性，不一定声明时带有 virtual 关键字
        /// </summary>
        [Summary("是否带有虚方法属性，不一定声明时带有 virtual 关键字")]
        bool IsVirtual { get; }

        /// <summary>
        /// 是否为运算符方法
        /// </summary>
        [Summary("是否为运算符方法")]
        bool IsOperator { get; }

        /// <summary>
        /// 是否属于重写的方法，不一定带有 override 关键字
        /// </summary>
        [Summary("是否属于重写的方法，不一定带有 override 关键字")]
        bool IsOverride { get; }

        /// <summary>
        /// 是否为异步方法
        /// </summary>
        [Summary("是否为异步方法")]
        bool IsAsync { get; }

        /// <summary>
        /// 此方法继承自祖先（不是直接的基类，而是基类的上层）
        /// </summary>
        [Summary("此方法继承自祖先（不是直接的基类，而是基类的上层）")]
        bool IsFromAncestor { get; }

        /// <summary>
        /// 此方法继承自接口，在该类中实现
        /// </summary>
        [Summary("此方法继承自接口，在该类中实现")]
        bool IsFromInterfaceImplement { get; }

        /// <summary>
        /// 此方法在当前类中存在重载方法，需要在 Type 解析时进行处理
        /// </summary>
        [Summary("此方法在当前类中存在重载方法，需要在 Type 解析时进行处理")]
        bool IsOverloadMethodInDeclaringType { get; set; }

        /// <summary>
        /// 方法的参数声明字符串，包含参数名称和类型
        /// </summary>
        [Summary("方法的参数声明字符串，包含参数名称和类型")]
        string ParametersDeclaration { get; }

        /// <summary>
        /// 不包含参数的简单方法签名
        /// </summary>
        [Summary("不包含参数的简单方法签名")]
        string SignatureWithoutParameters { get; }

        /// <summary>
        /// 添加 [Overload] 前缀
        /// </summary>
        [Summary("添加 [Overload] 前缀")]
        void AddOverloadPrefix();
    }
}
