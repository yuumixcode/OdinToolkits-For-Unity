using Sirenix.OdinInspector;
using System;
using System.Reflection;
using Yuumix.OdinToolkits.Core;
using Yuumix.OdinToolkits.Modules;

namespace Yuumix.OdinToolkits.AdvancedTypeAnalyzer
{
    /// <summary>
    /// 字段数据接口，继承自 IDerivedMemberData，包含字段特有的数据信息和方法，派生类的通用数据信息和方法
    /// </summary>
    public interface IFieldData : IDerivedMemberData
    {
        /// <summary>
        /// 是否为只读字段（readonly）
        /// </summary>
        public bool IsReadOnly { get; }

        /// <summary>
        /// 是否为常量字段（const）
        /// </summary>
        public bool IsConstant { get; }

        /// <summary>
        /// 是否为静态字段（static）
        /// </summary>
        public bool IsDynamic { get; }

        /// <summary>
        /// 字段的默认值，对于常量字段，这个值是常量的值；对于非常量字段，这个值是字段的自定义默认值
        /// </summary>
        public object DefaultValue { get; }

        /// <summary>
        /// 这个字段的 Type
        /// </summary>
        public Type FieldType { get; }

        /// <summary>
        /// 这个字段的类型名称
        /// </summary>
        public string FieldTypeName { get; }

        /// <summary>
        /// 这个字段的类型的完整名称
        /// </summary>
        public string FieldTypeFullName { get; }
    }

    /// <summary>
    /// 字段解析数据类，用于存储字段的解析数据
    /// </summary>
    [Serializable]
    public class FieldData : MemberData, IFieldData
    {
        public FieldData(FieldInfo fieldInfo, IAttributeFilter filter = null) : base(fieldInfo, filter)
        {
            IsStatic = fieldInfo.IsStatic;
            MemberType = fieldInfo.MemberType;
            MemberTypeName = MemberType.ToString();
            AccessModifier = fieldInfo.GetFieldAccessModifier();
            AccessModifierName = AccessModifier.ConvertToString();
            // ---
            FieldType = fieldInfo.FieldType;
            IsDynamic = fieldInfo.IsDynamicField();
            FieldTypeName = IsDynamic ? "dynamic" : FieldType?.GetReadableTypeName();
            FieldTypeFullName = IsDynamic ? "dynamic" : FieldType?.GetReadableTypeName(true);
            IsReadOnly = fieldInfo.IsInitOnly;
            IsConstant = fieldInfo.IsLiteral && !fieldInfo.IsInitOnly;
            DefaultValue = fieldInfo.TryGetFieldCustomDefaultValue(out var value) ? value : null;
            Signature = GetFieldSignature(
                TypeAnalyzerUtility.GetKeywordSnippetInSignature(IsConstant, IsStatic, IsReadOnly),
                TypeAnalyzerUtility.GetFormattedDefaultValue(FieldType, DefaultValue));
            FullDeclarationWithAttributes = AttributesDeclaration + Signature;
        }

        string GetFieldSignature(string keywordSnippet, string formattedDefaultValue)
        {
            if (DeclaringType.IsEnum)
            {
                return $"{AccessModifierName} {keywordSnippet}{FieldTypeName} {Name};";
            }

            return DefaultValue != null
                ? $"{AccessModifierName} {keywordSnippet}{FieldTypeName} {Name} = {formattedDefaultValue};"
                : $"{AccessModifierName} {keywordSnippet}{FieldTypeName} {Name};";
        }

        #region IFieldData

        public bool IsReadOnly { get; }
        public bool IsConstant { get; }
        public bool IsDynamic { get; }
        public object DefaultValue { get; }
        public Type FieldType { get; }
        public string FieldTypeName { get; }

        [PropertyOrder(60)]
        [ShowEnableProperty]
        [BilingualTitle("字段类型完整名称", nameof(FieldTypeName))]
        [HideLabel]
        public string FieldTypeFullName { get; }

        #endregion

        #region IDerivedMemberData

        public bool IsStatic { get; }
        public MemberTypes MemberType { get; }
        public string MemberTypeName { get; }
        public AccessModifierType AccessModifier { get; }
        public string AccessModifierName { get; }

        [PropertyOrder(60)]
        [ShowEnableProperty]
        [BilingualTitle("字段签名", nameof(Signature))]
        [HideLabel]
        public string Signature { get; private set; }

        [PropertyOrder(60)]
        [ShowEnableProperty]
        [BilingualTitle("完整字段声明 - 包含特性和签名 - 默认剔除 [Summary] 特性",
            nameof(FullDeclarationWithAttributes) + " - Include Attributes and Signature - Default Exclude [Summary]")]
        [HideLabel]
        [MultiLineProperty]
        public string FullDeclarationWithAttributes { get; }

        #endregion
    }
}
