using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.AdvancedTypeAnalyzer
{
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

    /// <summary>
    /// 方法解析数据类，用于存储 MethodInfo 的解析结果
    /// </summary>
    [Summary("方法解析数据类，用于存储 MethodInfo 的解析结果")]
    [Serializable]
    public class MethodData : MemberData, IMethodData
    {
        public MethodData(MethodInfo memberInfo, IAttributeFilter filter = null) : base(memberInfo, filter)
        {
            IsStatic = memberInfo.IsStatic;
            MemberType = memberInfo.MemberType;
            MemberTypeName = MemberType.ToString();
            AccessModifier = memberInfo.GetMethodAccessModifierType();
            AccessModifierName = AccessModifier.ConvertToString();
            // ---
            ReturnType = memberInfo.ReturnType;
            ReturnTypeName = ReturnType.GetReadableTypeName();
            ReturnTypeFullName = ReturnType.GetReadableTypeName(true);
            IsAbstract = memberInfo.IsAbstract;
            IsVirtual = memberInfo.IsVirtual;
            IsOperator = memberInfo.IsOperatorMethod();
            IsOverride = memberInfo.IsOverrideMethod();
            IsAsync = memberInfo.IsAsyncMethod();
            ParametersDeclaration = memberInfo.GetParametersNameWithDefaultValue();
            IsFromInterfaceImplement = memberInfo.IsFromInterfaceImplementMethod();
            IsFromAncestor = memberInfo.IsInheritedOverrideFromAncestor(memberInfo.DeclaringType);
            Signature = GetMethodFullSignature(AccessModifierName, memberInfo);
            SignatureWithoutParameters = Signature.Split('(')[0];
            FullDeclarationWithAttributes = AttributesDeclaration + Signature;
        }

        /// <summary>
        /// 获取方法的完整签名，包含访问修饰符、返回类型、方法名称、参数列表和泛型参数
        /// </summary>
        [Summary("获取方法的完整签名，包含访问修饰符、返回类型、方法名称、参数列表和泛型参数")]
        string GetMethodFullSignature(string accessModifierName, MethodInfo methodInfo)
        {
            var signature = string.Empty;
            if (methodInfo.IsExtensionMethod())
            {
                signature = "[Ext] ";
            }

            signature += accessModifierName + " ";
            signature += methodInfo.GetKeywordSnippetInSignature();
            if (!methodInfo.Name.Contains("op_Implicit") && !methodInfo.Name.Contains("op_Explicit"))
            {
                signature += methodInfo.ReturnType.GetReadableTypeName() + " ";
            }

            signature += methodInfo.GetPartMethodSignatureContainsNameAndParameters();
            return signature;
        }

        #region IMethodData

        public Type ReturnType { get; }
        public string ReturnTypeName { get; }

        [PropertyOrder(60)]
        [ShowEnableProperty]
        [BilingualTitle("返回值完整类型名称", nameof(ReturnTypeFullName))]
        [HideLabel]
        public string ReturnTypeFullName { get; }

        public string ParametersDeclaration { get; }
        public string SignatureWithoutParameters { get; }

        public bool IsAbstract { get; }

        public bool IsVirtual { get; }

        public bool IsOperator { get; }

        [PropertyOrder(50)]
        [ShowEnableProperty]
        [BilingualText("是否为重写方法", nameof(IsOverride))]
        public bool IsOverride { get; }

        public bool IsAsync { get; }

        public bool IsFromAncestor { get; }
        public bool IsFromInterfaceImplement { get; }
        public bool IsOverloadMethodInDeclaringType { get; set; }
        public void AddOverloadPrefix() => Signature = "[Overload] " + Signature;

        #endregion

        #region IDerivedMemberData 接口实现

        public bool IsStatic { get; }
        public MemberTypes MemberType { get; }
        public string MemberTypeName { get; }
        public AccessModifierType AccessModifier { get; }
        public string AccessModifierName { get; }

        [PropertyOrder(60)]
        [ShowEnableProperty]
        [BilingualTitle("方法签名", nameof(Signature))]
        [HideLabel]
        public string Signature { get; private set; }

        [PropertyOrder(60)]
        [ShowEnableProperty]
        [BilingualTitle("完整方法声明 - 包含特性和签名 - 默认剔除 [Summary] 特性",
            nameof(FullDeclarationWithAttributes) + " - Include Attributes and Signature - Default Exclude [Summary]")]
        [HideLabel]
        [MultiLineProperty]
        public string FullDeclarationWithAttributes { get; }

        #endregion
    }
}
