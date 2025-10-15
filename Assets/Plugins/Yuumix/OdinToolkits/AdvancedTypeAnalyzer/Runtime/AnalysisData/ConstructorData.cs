using Sirenix.OdinInspector;
using System;
using System.Reflection;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.AdvancedTypeAnalyzer
{
    public interface IConstructorData : IDerivedMemberData
    {
        /// <summary>
        /// 方法的参数声明字符串，包含参数名称和类型
        /// </summary>
        string ParametersDeclaration { get; }

        /// <summary>
        /// 不包含参数的简单方法签名
        /// </summary>
        string SignatureWithoutParameters { get; }

        /// <summary>
        /// 获取构造方法的签名
        /// </summary>
        string GetConstructorFullSignature(string accessModifierName, ConstructorInfo constructorInfo);
    }

    /// <summary>
    /// 构造方法解析数据
    /// </summary>
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

        #region IDerivedMemberData

        [ShowEnableProperty]
        [BilingualText("是否为静态构造方法", nameof(IsStatic))]
        public bool IsStatic { get; }

        [ShowEnableProperty]
        [BilingualText("成员类型", nameof(MemberType))]
        public MemberTypes MemberType { get; }

        public string MemberTypeName { get; }
        public AccessModifierType AccessModifier { get; }
        public string AccessModifierName { get; }

        [ShowEnableProperty]
        [BilingualTitle("方法签名", nameof(Signature))]
        [HideLabel]
        public string Signature { get; }

        [ShowEnableProperty]
        [HideLabel]
        [BilingualTitle("完整方法声明 - 包含特性和签名",
            nameof(FullDeclarationWithAttributes) + " - Include Attributes and Signature")]
        [MultiLineProperty]
        public string FullDeclarationWithAttributes { get; }

        #endregion

        #region IConstructorData

        public string ParametersDeclaration { get; }
        public string SignatureWithoutParameters { get; }

        public string GetConstructorFullSignature(string accessModifierName, ConstructorInfo constructorInfo)
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

        #endregion
    }
}
