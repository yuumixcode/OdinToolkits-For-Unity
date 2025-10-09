using System;
using System.Collections.Generic;
using System.Reflection;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Modules;

namespace Yuumix.OdinToolkits.AdvancedTypeAnalyzer
{
    /// <summary>
    /// 属性数据接口，继承自 IDerivedMemberData，包含属性特有的数据信息和方法，派生类的通用数据信息和方法
    /// </summary>
    public interface IPropertyData : IDerivedMemberData
    {
        /// <summary>
        /// 自定义默认值，如果没有自定义默认值，则为 null
        /// </summary>
        object DefaultValue { get; }

        /// <summary>
        /// 属性类型
        /// </summary>
        Type PropertyType { get; }

        /// <summary>
        /// 属性类型名称
        /// </summary>
        string PropertyTypeName { get; }

        /// <summary>
        /// 属性类型的完整名称，包括命名空间
        /// </summary>
        string PropertyTypeFullName { get; }

        /// <summary>
        /// 获取属性的签名，包括访问修饰符、静态修饰符、属性类型和属性名称
        /// </summary>
        /// <param name="accessModifier">访问修饰符字符串</param>
        /// <param name="isStatic">是否为静态</param>
        /// <param name="propertyTypeName">属性类型名称</param>
        /// <param name="propertyName">属性名称</param>
        /// <param name="propertyInfo">PropertyInfo 实例</param>
        /// <param name="defaultValue">默认值，没有则为 null</param>
        /// <param name="formattedDefaultValue">格式化的默认值字符串</param>
        string GetPropertySignature(string accessModifier, bool isStatic, string propertyTypeName,
            string propertyName, PropertyInfo propertyInfo, object defaultValue, string formattedDefaultValue);
    }

    public class PropertyData : MemberData, IPropertyData
    {
        public PropertyData(PropertyInfo propertyInfo) : base(propertyInfo)
        {
            IsStatic = propertyInfo.IsStaticProperty();
            MemberType = propertyInfo.MemberType;
            AccessModifier = propertyInfo.GetPropertyAccessModifierType();
            // --- 属性特有信息 ---
            PropertyType = propertyInfo.PropertyType;
            DefaultValue = propertyInfo.TryGetPropertyCustomDefaultValue(out var value) ? value : null;
            Signature = GetPropertySignature(AccessModifierName, IsStatic, PropertyTypeName, Name,
                propertyInfo, DefaultValue, TypeAnalyzerUtility.GetFormattedDefaultValue(PropertyType, DefaultValue));
        }

        #region IPropertyData 接口实现

        public object DefaultValue { get; }
        public Type PropertyType { get; }
        public string PropertyTypeName => PropertyType.GetReadableTypeName();
        public string PropertyTypeFullName => PropertyType.GetReadableTypeName(true);

        public string GetPropertySignature(string accessModifier, bool isStatic, string propertyTypeName,
            string propertyName, PropertyInfo propertyInfo, object defaultValue, string formattedDefaultValue)
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
            var hasDefaultValue = defaultValue != null;
            var signatureParts = new List<string>
            {
                isStatic ? accessModifier + " static" : accessModifier,
                propertyTypeName,
                propertyName,
                hasDefaultValue ? getSetPart + " = " + formattedDefaultValue + ";" : getSetPart
            };
            var signature = string.Join(" ", signatureParts);
            return signature;
        }

        #endregion

        #region IDerivedMemberData 接口实现

        public bool IsStatic { get; }
        public MemberTypes MemberType { get; }
        public string MemberTypeName => MemberType.ToString();
        public AccessModifierType AccessModifier { get; }
        public string AccessModifierName => AccessModifier.ConvertToString();
        public string Signature { get; }
        public string FullDeclarationWithAttributes => AttributesDeclaration + Signature;

        #endregion
    }
}
