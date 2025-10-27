using Sirenix.OdinInspector;
using System;
using System.Reflection;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.ScriptDocGenerator
{
    [Summary("构造方法数据接口，继承自 IDerivedMemberData")]
    public interface IConstructorData : IDerivedMemberData
    {
        [Summary("方法的参数声明字符串，包含参数名称和类型")]
        string ParametersDeclaration { get; }

        [Summary("不包含参数的简单方法签名")]
        string SignatureWithoutParameters { get; }
    }

    [Summary("构造方法解析数据")]
    [Serializable]
    public class ConstructorData : MemberData, IConstructorData
    {
        public ConstructorData(ConstructorInfo constructorInfo, IAttributeFilter filter = null)
            : base(constructorInfo, filter)
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
            signature += constructorInfo.GetPartMethodSignatureContainsNameAndParameters();
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

        [ShowEnableProperty]
        [BilingualText("是否为静态构造方法", nameof(IsStatic))]
        [Summary("是否为静态构造方法")]
        public bool IsStatic { get; }

        [ShowEnableProperty]
        [BilingualText("成员类型", nameof(MemberType))]
        [Summary("构造方法的成员类型")]
        public MemberTypes MemberType { get; }

        [Summary("成员类型名称")]
        public string MemberTypeName { get; }

        [Summary("访问修饰符类型")]
        public AccessModifierType AccessModifier { get; }

        [Summary("访问修饰符名称字符串")]
        public string AccessModifierName { get; }

        [ShowEnableProperty]
        [BilingualTitle("方法签名", nameof(Signature))]
        [HideLabel]
        [Summary("构造方法的完整签名")]
        public string Signature { get; }

        [ShowEnableProperty]
        [HideLabel]
        [BilingualTitle("完整方法声明 - 包含特性和签名",
            nameof(FullDeclarationWithAttributes) + " - Include Attributes and Signature")]
        [MultiLineProperty]
        [Summary("包含特性和签名的完整构造方法声明")]
        public string FullDeclarationWithAttributes { get; }

        #endregion

        #region IConstructorData

        [Summary("参数声明字符串")]
        public string ParametersDeclaration { get; }

        [Summary("不包含参数的方法签名")]
        public string SignatureWithoutParameters { get; }

        #endregion
    }
}
