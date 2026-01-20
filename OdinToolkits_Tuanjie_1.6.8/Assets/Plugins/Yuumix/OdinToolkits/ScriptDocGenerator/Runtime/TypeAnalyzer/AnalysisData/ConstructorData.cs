using System;
using System.Reflection;
using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.ScriptDocGenerator
{
    /// <summary>
    /// 构造方法解析数据
    /// </summary>
    [Summary("构造方法解析数据")]
    [Serializable]
    public class ConstructorData : MemberData, IConstructorData
    {
        public ConstructorData(ConstructorInfo constructorInfo, IAttributeFilter filter = null) : base(
            constructorInfo, filter)
        {
            IsStatic = constructorInfo.IsStatic;
            MemberType = constructorInfo.MemberType;
            MemberTypeName = MemberType.ToString();
            AccessModifier = constructorInfo.GetMethodAccessModifierType();
            AccessModifierName = AccessModifier.ConvertToString();
            // ---
            ParametersDeclaration = constructorInfo.GetParametersNameWithDefaultValue();
            Signature = GetConstructorFullSignature(AccessModifierName, constructorInfo);
            SignatureWithoutParameters = Signature.Split('(')[0];
            FullDeclarationWithAttributes = AttributesDeclaration + Signature;
        }

        static string GetConstructorFullSignature(string accessModifierName, ConstructorInfo constructorInfo)
        {
            var signature = string.Empty;
            signature += accessModifierName + " ";
            signature += GetKeywordSnippetInSignature(constructorInfo);
            signature += constructorInfo.GetMethodNameAndParameters();
            return signature;
        }

        static string GetKeywordSnippetInSignature(ConstructorInfo constructorInfo)
        {
            var keyword = "";
            if (constructorInfo.IsStatic)
            {
                keyword = "static ";
            }

            return keyword;
        }

        #region IDerivedMemberData

        /// <summary>
        /// 是否为静态构造方法
        /// </summary>
        [ShowEnableProperty]
        [BilingualText("是否为静态构造方法", nameof(IsStatic))]
        [Summary("是否为静态构造方法")]
        public bool IsStatic { get; }

        /// <summary>
        /// 构造方法的成员类型
        /// </summary>
        [ShowEnableProperty]
        [BilingualText("成员类型", nameof(MemberType))]
        [Summary("构造方法的成员类型")]
        public MemberTypes MemberType { get; }

        /// <summary>
        /// 构造方法的成员类型名称字符串
        /// </summary>
        [ShowEnableProperty]
        [BilingualText("成员类型名称", nameof(MemberTypeName))]
        [Summary("构造方法的成员类型名称字符串")]
        public string MemberTypeName { get; }

        /// <summary>
        /// 构造方法的访问修饰符类型
        /// </summary>
        [ShowEnableProperty]
        [BilingualText("访问修饰符类型", nameof(AccessModifier))]
        [Summary("构造方法的访问修饰符类型")]
        public AccessModifierType AccessModifier { get; }

        /// <summary>
        /// 构造方法的访问修饰符名称字符串
        /// </summary>
        [ShowEnableProperty]
        [BilingualText("访问修饰符名称", nameof(AccessModifierName))]
        [Summary("构造方法的访问修饰符名称字符串")]
        public string AccessModifierName { get; }

        /// <summary>
        /// 构造方法的完整签名
        /// </summary>
        [ShowEnableProperty]
        [BilingualTitle("方法签名", nameof(Signature))]
        [HideLabel]
        [Summary("构造方法的完整签名")]
        public string Signature { get; }

        /// <summary>
        /// 包含特性和签名的完整构造方法声明
        /// </summary>
        [ShowEnableProperty]
        [HideLabel]
        [BilingualTitle("完整方法声明 - 包含特性和签名",
            nameof(FullDeclarationWithAttributes) + " - Include Attributes and Signature")]
        [MultiLineProperty]
        [Summary("包含特性和签名的完整构造方法声明")]
        public string FullDeclarationWithAttributes { get; }

        #endregion

        #region IConstructorData

        /// <summary>
        /// 构造方法的参数声明字符串，包含参数名称和类型
        /// </summary>
        [ShowEnableProperty]
        [BilingualText("参数声明", nameof(ParametersDeclaration))]
        [Summary("构造方法的参数声明字符串，包含参数名称和类型")]
        public string ParametersDeclaration { get; }

        /// <summary>
        /// 不包含参数的简单构造方法签名
        /// </summary>
        [ShowEnableProperty]
        [BilingualText("不包含参数的方法签名", nameof(SignatureWithoutParameters))]
        [Summary("不包含参数的简单构造方法签名")]
        public string SignatureWithoutParameters { get; }

        #endregion
    }
}
