using Sirenix.OdinInspector;
using System;
using System.Reflection;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.ScriptDocGenerator
{
    [Summary("字段数据接口，继承自 IDerivedMemberData")]
    public interface IFieldData : IDerivedMemberData
    {
        [Summary("是否为只读字段（readonly）")]
        public bool IsReadOnly { get; }

        [Summary("是否为常量字段（const）")]
        public bool IsConstant { get; }

        [Summary("是否为动态字段（dynamic）")]
        public bool IsDynamic { get; }

        [Summary("字段的默认值")]
        public object DefaultValue { get; }

        [Summary("字段的类型")]
        public Type FieldType { get; }

        [Summary("这个字段的类型的名称")]
        public string FieldTypeName { get; }

        [Summary("这个字段的类型的完整名称")]
        public string FieldTypeFullName { get; }
    }

    [Summary("字段解析数据类，用于存储字段的解析数据")]
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

        #region IDerivedMemberData

        [Summary("指示该字段是否为静态字段")]
        public bool IsStatic { get; }

        [Summary("成员类型，指示该成员的类型")]
        public MemberTypes MemberType { get; }

        [Summary("成员类型名称")]
        public string MemberTypeName { get; }

        [Summary("访问修饰符类型")]
        public AccessModifierType AccessModifier { get; }

        [Summary("访问修饰符名称")]
        public string AccessModifierName { get; }

        [PropertyOrder(60)]
        [ShowEnableProperty]
        [BilingualTitle("字段签名", nameof(Signature))]
        [HideLabel]
        [Summary("字段签名")]
        public string Signature { get; private set; }

        [PropertyOrder(60)]
        [ShowEnableProperty]
        [BilingualTitle("完整字段声明 - 包含特性和签名 - 默认剔除 [Summary] 特性",
            nameof(FullDeclarationWithAttributes) + " - Include Attributes and Signature - Default Exclude [Summary]")]
        [HideLabel]
        [MultiLineProperty]
        [Summary("完整字段声明，包含特性和签名，默认剔除 Summary 特性")]
        public string FullDeclarationWithAttributes { get; }

        #endregion

        #region IFieldData

        [Summary("是否为只读字段")]
        public bool IsReadOnly { get; }

        [Summary("是否为常量字段")]
        public bool IsConstant { get; }

        [Summary("是否为动态类型字段")]
        public bool IsDynamic { get; }

        [Summary("字段的默认值，没有默认值返回 null")]
        public object DefaultValue { get; }

        [Summary("字段的类型")]
        public Type FieldType { get; }

        [Summary("字段类型的名称")]
        public string FieldTypeName { get; }

        [PropertyOrder(60)]
        [ShowEnableProperty]
        [BilingualTitle("字段类型完整名称", nameof(FieldTypeName))]
        [HideLabel]
        [Summary("字段类型的完整名称")]
        public string FieldTypeFullName { get; }

        #endregion
    }
}
