using System;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.ScriptDocGenerator
{
    [Serializable]
    public class PropertyData : MemberData, IPropertyData
    {
        public PropertyData(PropertyInfo propertyInfo, IAttributeFilter filter = null) : base(propertyInfo,
            filter)
        {
            // IDerivedMemberData 
            IsStatic = propertyInfo.IsStaticProperty();
            MemberType = propertyInfo.MemberType;
            MemberTypeName = MemberType.ToString();
            AccessModifier = propertyInfo.GetPropertyAccessModifierType();
            AccessModifierName = AccessModifier.ConvertToString();
            // IPropertyData
            PropertyType = propertyInfo.PropertyType;
            PropertyTypeName = PropertyType.GetReadableTypeName();
            PropertyTypeFullName = PropertyType.GetReadableTypeName(true);
            DefaultValue = propertyInfo.TryGetPropertyCustomDefaultValue(out var value) ? value : null;
            Signature = GetPropertySignature(propertyInfo,
                TypeAnalyzerUtility.GetFormattedDefaultValue(PropertyType, DefaultValue));
            FullDeclarationWithAttributes = AttributesDeclaration + Signature;
        }

        string GetPropertySignature(PropertyInfo propertyInfo, string formattedDefaultValue)
        {
            // --- get set ---
            var getSetPart = "{ ";
            var getMethod = propertyInfo.GetGetMethod(true);
            if (getMethod != null)
            {
                getSetPart += getMethod.GetMethodAccessModifierType() == AccessModifierType.Public
                    ? "get; "
                    : getMethod.GetMethodAccessModifierType()
                        .ConvertToString() + " get; ";
            }

            var setMethod = propertyInfo.GetSetMethod(true);
            if (setMethod != null)
            {
                getSetPart += setMethod.GetMethodAccessModifierType() == AccessModifierType.Public
                    ? "set; "
                    : setMethod.GetMethodAccessModifierType()
                        .ConvertToString() + " set; ";
            }

            getSetPart += "}";
            var hasDefaultValue = DefaultValue != null;
            var signatureParts = new List<string>
            {
                IsStatic ? AccessModifierName + " static" : AccessModifierName,
                PropertyTypeName,
                Name,
                hasDefaultValue ? getSetPart + " = " + formattedDefaultValue + ";" : getSetPart
            };
            var signature = string.Join(" ", signatureParts);
            return signature;
        }

        #region IDerivedMemberData

        /// <summary>
        /// 是否为静态属性
        /// </summary>
        [Summary("是否为静态属性")]
        public bool IsStatic { get; }

        /// <summary>
        /// 成员类型
        /// </summary>
        [Summary("成员类型")]
        public MemberTypes MemberType { get; }

        public string MemberTypeName { get; }

        /// <summary>
        /// 访问修饰符
        /// </summary>
        [Summary("访问修饰符")]
        public AccessModifierType AccessModifier { get; }

        /// <summary>
        /// 访问修饰符名称
        /// </summary>
        [Summary("访问修饰符名称")]
        public string AccessModifierName { get; }

        /// <summary>
        /// 属性签名
        /// </summary>
        [Summary("属性签名")]
        [PropertyOrder(60)]
        [ShowEnableProperty]
        [BilingualTitle("属性签名", nameof(Signature))]
        [HideLabel]
        public string Signature { get; private set; }

        /// <summary>
        /// 完整属性声明 - 包含特性和签名 - 默认剔除 [Summary] 特性
        /// </summary>
        [Summary("完整属性声明 - 包含特性和签名 - 默认剔除 [Summary] 特性")]
        [PropertyOrder(60)]
        [ShowEnableProperty]
        [BilingualTitle("完整属性声明 - 包含特性和签名 - 默认剔除 [Summary] 特性",
            nameof(FullDeclarationWithAttributes) +
            " - Include Attributes and Signature - Default Exclude [Summary]")]
        [HideLabel]
        [MultiLineProperty]
        public string FullDeclarationWithAttributes { get; }

        #endregion

        #region IPropertyData

        /// <summary>
        /// 属性类型
        /// </summary>
        [Summary("属性类型")]
        public object DefaultValue { get; }

        /// <summary>
        /// 属性类型
        /// </summary>
        [Summary("属性类型")]
        public Type PropertyType { get; }

        /// <summary>
        /// 属性类型名称
        /// </summary>
        [Summary("属性类型名称")]
        public string PropertyTypeName { get; }

        /// <summary>
        /// 属性类型的完整名称
        /// </summary>
        [Summary("属性类型的完整名称")]
        [PropertyOrder(60)]
        [ShowEnableProperty]
        [BilingualTitle("属性类型的完整名称", nameof(PropertyTypeFullName))]
        [HideLabel]
        public string PropertyTypeFullName { get; }

        #endregion
    }
}
