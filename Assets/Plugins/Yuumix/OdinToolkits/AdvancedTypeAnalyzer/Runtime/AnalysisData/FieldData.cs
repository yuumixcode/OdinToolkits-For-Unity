using System;
using System.Reflection;
using Yuumix.OdinToolkits.Core;

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

        /// <summary>
        /// 生成字段签名字符串
        /// </summary>
        /// <param name="keywordSnippet">关键字片段</param>
        /// <param name="defaultValue">是否有自定义默认值，为 null 表示没有</param>
        /// <param name="formattedDefaultValue">格式化的默认值</param>
        /// <param name="accessModifierName">访问修饰符名称</param>
        /// <param name="fieldTypeName">字段类型名称</param>
        /// <param name="name">字段名称</param>
        string GetFieldSignature(string keywordSnippet, object defaultValue, string formattedDefaultValue,
            string accessModifierName, string fieldTypeName,
            string name);
    }

    /// <summary>
    /// 字段解析数据类，用于存储字段的解析数据
    /// </summary>
    [Serializable]
    public class FieldData : MemberData, IFieldData
    {
        public FieldData(FieldInfo fieldInfo) : base(fieldInfo)
        {
            IsStatic = fieldInfo.IsStatic;
            AccessModifier = fieldInfo.GetFieldAccessModifier();
            MemberType = fieldInfo.MemberType;
            // ---
            FieldType = fieldInfo.FieldType;
            IsReadOnly = fieldInfo.IsInitOnly;
            IsConstant = fieldInfo.IsLiteral && !fieldInfo.IsInitOnly;
            IsDynamic = fieldInfo.IsDynamicField();
            DefaultValue = fieldInfo.TryGetFieldCustomDefaultValue(out var value) ? value : null;
            Signature = GetFieldSignature(
                TypeAnalyzerUtility.GetKeywordSnippetInSignature(IsConstant, IsStatic, IsReadOnly),
                DefaultValue,
                TypeAnalyzerUtility.GetFormattedDefaultValue(FieldType, DefaultValue),
                AccessModifierName, FieldTypeName, Name);
        }

        #region IFieldData 接口实现

        public bool IsReadOnly { get; }
        public bool IsConstant { get; }
        public bool IsDynamic { get; }
        public object DefaultValue { get; }
        public Type FieldType { get; }
        public string FieldTypeName => IsDynamic ? "dynamic" : FieldType?.GetReadableTypeName();
        public string FieldTypeFullName => IsDynamic ? "dynamic" : FieldType?.GetReadableTypeName(true);

        public string GetFieldSignature(string keywordSnippet, object defaultValue, string formattedDefaultValue,
            string accessModifierName, string fieldTypeName, string name) =>
            defaultValue != null
                ? $"{accessModifierName} {keywordSnippet}{fieldTypeName} {name} = {formattedDefaultValue};"
                : $"{accessModifierName} {keywordSnippet}{fieldTypeName} {name};";

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
