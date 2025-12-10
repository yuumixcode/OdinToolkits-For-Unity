using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.ScriptDocGenerator
{
    /// <summary>
    /// 方法解析数据类，用于存储 MethodInfo 的解析结果
    /// </summary>
    [Summary("方法解析数据类，用于存储 MethodInfo 的解析结果")]
    [Serializable]
    public class MethodData : MemberData, IMethodData
    {
        public MethodData(MethodInfo memberInfo, IAttributeFilter filter = null) : base(memberInfo,
            filter)
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
            signature += GetMethodKeywordSnippet(methodInfo);
            if (!methodInfo.Name.Contains("op_Implicit") &&
                !methodInfo.Name.Contains("op_Explicit"))
            {
                signature += methodInfo.ReturnType.GetReadableTypeName() + " ";
            }

            signature += methodInfo.GetMethodNameAndParameters();
            return signature;
        }

        /// <summary>
        /// 获取方法的关键字片段字符串
        /// </summary>
        [Summary("获取方法的关键字片段字符串")]
        public static string GetMethodKeywordSnippet(MethodInfo methodInfo)
        {
            var keyword = "";
            if (methodInfo.IsStatic)
            {
                keyword = "static ";
            }
            else if (methodInfo.IsAbstract)
            {
                keyword = "abstract ";
            }
            else if (methodInfo.IsVirtual && methodInfo.DeclaringType !=
                     methodInfo.GetBaseDefinition().DeclaringType)
            {
                keyword = "override ";
            }
            else if (methodInfo.DeclaringType == methodInfo.GetBaseDefinition().DeclaringType &&
                     methodInfo.IsVirtual && methodInfo.IsFromInterfaceImplementMethod())
            {
                // 这是实现接口的方法
                keyword = "";
            }
            else if (methodInfo.IsVirtual)
            {
                keyword = "virtual ";
            }

            if (methodInfo.GetCustomAttribute<AsyncStateMachineAttribute>() != null)
            {
                keyword += "async ";
            }

            return keyword;
        }

        #region IMethodData

        /// <summary>
        /// 方法的返回类型
        /// </summary>
        [Summary("方法的返回类型")]
        public Type ReturnType { get; }

        /// <summary>
        /// 方法的返回类型名称字符串
        /// </summary>
        [Summary("方法的返回类型名称字符串")]
        public string ReturnTypeName { get; }

        /// <summary>
        /// 方法的返回值完整类型名称字符串
        /// </summary>
        [Summary("方法的返回值完整类型名称字符串")]
        [PropertyOrder(60)]
        [ShowEnableProperty]
        [BilingualTitle("返回值完整类型名称", nameof(ReturnTypeFullName))]
        [HideLabel]
        public string ReturnTypeFullName { get; }

        /// <summary>
        /// 方法的参数声明字符串，包含参数名称和类型
        /// </summary>
        [Summary("方法的参数声明字符串，包含参数名称和类型")]
        public string ParametersDeclaration { get; }

        /// <summary>
        /// 不包含参数的简单方法签名
        /// </summary>
        [Summary("不包含参数的简单方法签名")]
        public string SignatureWithoutParameters { get; }

        /// <summary>
        /// 是否为抽象方法（abstract）
        /// </summary>
        [Summary("是否为抽象方法（abstract）")]
        public bool IsAbstract { get; }

        /// <summary>
        /// 是否有虚拟方法（virtual）的特性，方法签名中不一定带有 virtual 关键字
        /// </summary>
        [Summary("是否有虚拟方法（virtual）的特性，方法签名中不一定带有 virtual 关键字")]
        public bool IsVirtual { get; }

        /// <summary>
        /// 是否为运算符方法（operator）
        /// </summary>
        [Summary("是否为运算符方法（operator）")]
        public bool IsOperator { get; }

        /// <summary>
        /// 是否有重写方法（override）的特性，方法签名中不一定带有 override 关键字
        /// </summary>
        [Summary("是否有重写方法（override）的特性，方法签名中不一定带有 override 关键字")]
        [PropertyOrder(50)]
        [ShowEnableProperty]
        [BilingualText("是否为重写方法", nameof(IsOverride))]
        public bool IsOverride { get; }

        /// <summary>
        /// 是否为异步方法（async）
        /// </summary>
        [Summary("是否为异步方法（async）")]
        public bool IsAsync { get; }

        /// <summary>
        /// 是否从祖先类继承的重写方法，在该子类的方法签名中不一定带有 override 关键字
        /// </summary>
        [Summary("是否从祖先类继承的重写方法，在该子类的方法签名中不一定带有 override 关键字")]
        public bool IsFromAncestor { get; }

        /// <summary>
        /// 是否从接口实现的方法
        /// </summary>
        [Summary("是否从接口实现的方法")]
        public bool IsFromInterfaceImplement { get; }

        /// <summary>
        /// 是否为声明类型中的重载方法
        /// </summary>
        [Summary("是否为声明类型中的重载方法")]
        public bool IsOverloadMethodInDeclaringType { get; set; }

        /// <summary>
        /// 为方法添加重载前缀（[Overload]）
        /// </summary>
        [Summary("为方法添加重载前缀（[Overload]）")]
        public void AddOverloadPrefix() => Signature = "[Overload] " + Signature;

        #endregion

        #region IDerivedMemberData 接口实现

        /// <summary>
        /// 是否为静态方法（static）
        /// </summary>
        [Summary("是否为静态方法（static）")]
        public bool IsStatic { get; }

        /// <summary>
        /// 方法的成员类型
        /// </summary>
        [Summary("方法的成员类型")]
        public MemberTypes MemberType { get; }

        /// <summary>
        /// 方法的成员类型名称字符串
        /// </summary>
        [Summary("方法的成员类型名称字符串")]
        public string MemberTypeName { get; }

        /// <summary>
        /// 方法的访问修饰符类型
        /// </summary>
        [Summary("方法的访问修饰符类型")]
        public AccessModifierType AccessModifier { get; }

        /// <summary>
        /// 方法的访问修饰符名称字符串
        /// </summary>
        [Summary("方法的访问修饰符名称字符串")]
        public string AccessModifierName { get; }

        /// <summary>
        /// 方法的签名字符串
        /// </summary>
        [Summary("方法的签名字符串")]
        [PropertyOrder(60)]
        [ShowEnableProperty]
        [BilingualTitle("方法签名", nameof(Signature))]
        [HideLabel]
        public string Signature { get; private set; }

        /// <summary>
        /// 包含特性的完整方法声明字符串，包含特性声明和方法签名
        /// </summary>
        [Summary("包含特性的完整方法声明字符串，包含特性声明和方法签名")]
        [PropertyOrder(60)]
        [ShowEnableProperty]
        [BilingualTitle("完整方法声明 - 包含特性和签名 - 默认剔除 [Summary] 特性",
            nameof(FullDeclarationWithAttributes) +
            " - Include Attributes and Signature - Default Exclude [Summary]")]
        [HideLabel]
        [MultiLineProperty]
        public string FullDeclarationWithAttributes { get; }

        #endregion
    }
}
