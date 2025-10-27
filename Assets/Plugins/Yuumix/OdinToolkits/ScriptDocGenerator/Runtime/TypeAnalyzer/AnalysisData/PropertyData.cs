using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using System.Reflection;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.ScriptDocGenerator
{
    /// <summary>
    /// 属性数据接口，继承自 IDerivedMemberData，包含属性特有的数据信息和方法，派生类的通用数据信息和方法
    /// </summary>
    [Summary("属性数据接口，继承自 IDerivedMemberData，包含属性特有的数据信息和方法，派生类的通用数据信息和方法")]
    public interface IPropertyData : IDerivedMemberData
    {
        /// <summary>
        /// 自定义默认值，如果没有自定义默认值，则为 null
        /// </summary>
        [Summary("自定义默认值，如果没有自定义默认值，则为 null")]
        object DefaultValue { get; }

        /// <summary>
        /// 属性类型
        /// </summary>
        [Summary("属性类型")]
        Type PropertyType { get; }

        /// <summary>
        /// 属性类型名称
        /// </summary>
        [Summary("属性类型名称")]
        string PropertyTypeName { get; }

        /// <summary>
        /// 属性类型的完整名称，包括命名空间
        /// </summary>
        [Summary("属性类型的完整名称，包括命名空间")]
        string PropertyTypeFullName { get; }
    }

    [Serializable]
    public class PropertyData : MemberData, IPropertyData
    {
        public PropertyData(PropertyInfo propertyInfo, IAttributeFilter filter = null) : base(propertyInfo, filter)
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

        /// <summary>
        /// 获取属性的签名，包括访问修饰符、静态修饰符、属性类型和属性名称
        /// </summary>
        [Summary("获取属性的签名，包括访问修饰符、静态修饰符、属性类型和属性名称")]
        string GetPropertySignature(PropertyInfo propertyInfo, string formattedDefaultValue)
        {
            // --- get set ---
            var getSetPart = "{ ";
            var getMethod = propertyInfo.GetGetMethod(true);
            if (getMethod != null)
            {
                getSetPart += getMethod.GetMethodAccessModifierType() == AccessModifierType.Public
                    ? "get; "
                    : getMethod.GetMethodAccessModifierType().ConvertToString() + " get; ";
            }

            var setMethod = propertyInfo.GetSetMethod(true);
            if (setMethod != null)
            {
                getSetPart += setMethod.GetMethodAccessModifierType() == AccessModifierType.Public
                    ? "set; "
                    : setMethod.GetMethodAccessModifierType().ConvertToString() + " set; ";
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

        public bool IsStatic { get; }
        public MemberTypes MemberType { get; }
        public string MemberTypeName { get; }
        public AccessModifierType AccessModifier { get; }
        public string AccessModifierName { get; }

        [PropertyOrder(60)]
        [ShowEnableProperty]
        [BilingualTitle("属性签名", nameof(Signature))]
        [HideLabel]
        public string Signature { get; private set; }

        [PropertyOrder(60)]
        [ShowEnableProperty]
        [BilingualTitle("完整属性声明 - 包含特性和签名 - 默认剔除 [Summary] 特性",
            nameof(FullDeclarationWithAttributes) + " - Include Attributes and Signature - Default Exclude [Summary]")]
        [HideLabel]
        [MultiLineProperty]
        public string FullDeclarationWithAttributes { get; }

        #endregion

        #region IPropertyData

        public object DefaultValue { get; }
        public Type PropertyType { get; }
        public string PropertyTypeName { get; }

        [PropertyOrder(60)]
        [ShowEnableProperty]
        [BilingualTitle("属性类型的完整名称", nameof(PropertyTypeFullName))]
        [HideLabel]
        public string PropertyTypeFullName { get; }

        #endregion
    }
}
