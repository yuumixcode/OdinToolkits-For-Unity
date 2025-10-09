using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Modules;

namespace Yuumix.OdinToolkits.AdvancedTypeAnalyzer
{
    public interface IMethodData : IDerivedMemberData
    {
        /// <summary>
        /// 方法的返回类型
        /// </summary>
        Type ReturnType { get; }

        /// <summary>
        /// 方法的返回类型名称
        /// </summary>
        string ReturnTypeName { get; }

        /// <summary>
        /// 方法的返回类型的完整名称
        /// </summary>
        string ReturnTypeFullName { get; }

        /// <summary>
        /// 方法的参数声明字符串，包含参数名称和类型
        /// </summary>
        string ParametersDeclaration { get; }

        /// <summary>
        /// 是否带有抽象属性，不一定声明时带有 abstract 关键字
        /// </summary>
        bool IsAbstract { get; }

        /// <summary>
        /// 是否带有虚方法属性，不一定声明时带有 virtual 关键字
        /// </summary>
        bool IsVirtual { get; }

        /// <summary>
        /// 是否为运算符方法
        /// </summary>
        bool IsOperator { get; }

        /// <summary>
        /// 是否属于重写的方法，不一定带有 override 关键字
        /// </summary>
        bool IsOverride { get; }

        /// <summary>
        /// 是否为异步方法
        /// </summary>
        bool IsAsync { get; }

        /// <summary>
        /// 此方法继承自祖先（不是直接的基类，而是基类的上层）
        /// </summary>
        bool IsFromAncestor { get; }

        /// <summary>
        /// 此方法继承自接口，在该类中实现
        /// </summary>
        bool IsFromInterfaceImplement { get; }

        /// <summary>
        /// 此方法在当前类中存在重载方法，需要在 Type 解析时进行处理
        /// </summary>
        bool IsOverloadMethodInDeclaringType { get; set; }

        /// <summary>
        /// 不包含参数的简单方法签名
        /// </summary>
        string SignatureWithoutParameters { get; }
    }

    public class MethodData : MemberData, IMethodData
    {
        static readonly Dictionary<string, string> OperatorStringMap = new Dictionary<string, string>
        {
            // 算术运算符
            { "op_Addition", "operator +" },
            { "op_Subtraction", "operator -" },
            { "op_Multiply", "operator *" },
            { "op_Division", "operator /" },
            { "op_Modulus", "operator %" },
            { "op_Increment", "operator ++" },
            { "op_Decrement", "operator --" },

            // 位运算符
            { "op_BitwiseAnd", "operator &" },
            { "op_BitwiseOr", "operator |" },
            { "op_ExclusiveOr", "operator ^" },
            { "op_LeftShift", "operator <<" },
            { "op_RightShift", "operator >>" },
            { "op_OnesComplement", "operator ~" },

            // 逻辑运算符
            { "op_LogicalNot", "operator !" },
            { "op_True", "operator true" },
            { "op_False", "operator false" },

            // 比较运算符
            { "op_Equality", "operator ==" },
            { "op_Inequality", "operator !=" },
            { "op_LessThan", "operator <" },
            { "op_GreaterThan", "operator >" },
            { "op_LessThanOrEqual", "operator <=" },
            { "op_GreaterThanOrEqual", "operator >=" },

            // 类型转换
            { "op_Implicit", "implicit operator" },
            { "op_Explicit", "explicit operator" },

            // 赋值运算符 (C# 7.3+)
            { "op_Assign", "operator =" },
            { "op_MultiplyAssign", "operator *=" },
            { "op_DivideAssign", "operator /=" },
            { "op_ModulusAssign", "operator %=" },
            { "op_AdditionAssign", "operator +=" },
            { "op_SubtractionAssign", "operator -=" },
            { "op_LeftShiftAssign", "operator <<=" },
            { "op_RightShiftAssign", "operator >>=" },
            { "op_BitwiseAndAssign", "operator &=" },
            { "op_BitwiseOrAssign", "operator |=" },
            { "op_ExclusiveOrAssign", "operator ^=" },

            // 补充遗漏的标准运算符
            { "op_Coalesce", "operator ??" },
            { "op_MemberAccess", "operator ->" },
            { "op_Index", "operator []" },
            { "op_AddressOf", "operator &" },
            { "op_PointerDereference", "operator * " }
        };

        public MethodData(MethodInfo methodInfo) : base(methodInfo)
        {
            IsStatic = methodInfo.IsStatic;
            MemberType = methodInfo.MemberType;
            AccessModifier = methodInfo.GetMethodAccessModifierType();
            // ---
            ReturnType = methodInfo.ReturnType;
            IsAbstract = methodInfo.IsAbstract;
            IsVirtual = methodInfo.IsVirtual;
            IsOperator = methodInfo.IsSpecialName && methodInfo.Name.StartsWith("op_");
            IsOverride = (methodInfo.IsVirtual &&
                          methodInfo.DeclaringType != methodInfo.GetBaseDefinition().DeclaringType)
                         || (methodInfo.DeclaringType == methodInfo.GetBaseDefinition().DeclaringType &&
                             methodInfo.IsVirtual && methodInfo.IsFromInterfaceMethod());
            IsAsync = methodInfo.GetCustomAttribute<AsyncStateMachineAttribute>() != null;
            ParametersDeclaration = methodInfo.GetParamsNamesWithDefaultValue();
            IsFromInterfaceImplement = methodInfo.IsFromInterfaceMethod();
            IsFromAncestor = methodInfo.IsInheritedOverrideFromAncestor(methodInfo.DeclaringType);
            Signature = GetMethodFullSignature(AccessModifierName, methodInfo);
            SignatureWithoutParameters = Signature.Split('(')[0];
            FullDeclarationWithAttributes = AttributesDeclaration + Signature;
        }

        public void AddOverloadPrefix() => Signature = "[Overload] " + Signature;

        static string GetKeywordSnippetInSignature(MethodInfo methodInfo)
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
            else if (methodInfo.IsVirtual && methodInfo.DeclaringType != methodInfo.GetBaseDefinition().DeclaringType)
            {
                keyword = "override ";
            }
            else if (methodInfo.DeclaringType == methodInfo.GetBaseDefinition().DeclaringType &&
                     methodInfo.IsVirtual && methodInfo.IsFromInterfaceMethod())
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

        static string GetMethodFullSignature(string accessModifierName, MethodInfo methodInfo)
        {
            var signature = accessModifierName + " ";
            signature += GetKeywordSnippetInSignature(methodInfo);
            signature += methodInfo.ReturnType.GetReadableTypeName() + " ";
            signature += TypeAnalyzerHelper.GetFullMethodName(methodInfo, "");
            return signature;
        }

        #region IMethodData 接口实现

        public Type ReturnType { get; }
        public string ReturnTypeName => ReturnType.GetReadableTypeName();
        public string ReturnTypeFullName => ReturnType.GetReadableTypeName(true);
        public string ParametersDeclaration { get; }
        public string SignatureWithoutParameters { get; }
        public bool IsAbstract { get; }
        public bool IsVirtual { get; }
        public bool IsOperator { get; }
        public bool IsOverride { get; }
        public bool IsAsync { get; }
        public bool IsFromAncestor { get; }
        public bool IsFromInterfaceImplement { get; }
        public bool IsOverloadMethodInDeclaringType { get; set; }

        #endregion

        #region IDerivedMemberData 接口实现

        public bool IsStatic { get; }
        public MemberTypes MemberType { get; }
        public string MemberTypeName => MemberType.ToString();
        public AccessModifierType AccessModifier { get; }
        public string AccessModifierName => AccessModifier.ConvertToString();
        public string Signature { get; set; }
        public string FullDeclarationWithAttributes { get; }

        #endregion
    }
}
