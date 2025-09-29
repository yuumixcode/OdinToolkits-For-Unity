using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.AdvancedTypeAnalyzer
{
    /// <summary>
    /// 字段数据接口，继承自 IDerivedMemberData，包含字段特有的数据信息，派生类的通用数据信息，以及字段数据行为的方法
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
        /// 常量值（如果是常量字段）
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
        /// 获取不是常量字段的自定义默认值
        /// </summary>
        /// <param name="fieldInfo">字段信息</param>
        /// <returns>字段的默认值，如果没有自定义默认值则返回 null</returns>
        object GetNonConstantDefaultValue(FieldInfo fieldInfo);

        /// <summary>
        /// 获取字段访问修饰符
        /// </summary>
        /// <param name="fieldInfo">字段信息</param>
        /// <remarks>
        /// FieldInfo 的访问修饰符属性是互斥的，一个字段只能有一个访问修饰符
        /// </remarks>
        AccessModifierType GetFieldAccessModifier(FieldInfo fieldInfo);

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
    /// 字段数据类，用于分析和存储字段（Field）的详细信息
    /// </summary>
    /// <example>
    ///     <code>
    /// FieldInfo fieldInfo = typeof(MyClass).GetField("myField");
    /// FieldData fieldData = new FieldData(fieldInfo);
    /// </code>
    /// </example>
    [Serializable]
    public class FieldData : MemberData, IFieldData
    {
        public FieldData(FieldInfo fieldInfo) : base(fieldInfo)
        {
            IsStatic = fieldInfo.IsStatic;
            AccessModifier = GetFieldAccessModifier(fieldInfo);
            MemberType = fieldInfo.MemberType;

            // 字段特有信息初始化
            FieldType = fieldInfo.FieldType;
            IsReadOnly = fieldInfo.IsInitOnly;                         // readonly 字段标识
            IsConstant = fieldInfo.IsLiteral && !fieldInfo.IsInitOnly; // const 字段标识

            // 获取常量值（仅对常量字段有效）
            // 注意：GetRawConstantValue() 可能会抛出异常，需要确保字段确实是常量
            DefaultValue = IsConstant ? fieldInfo.GetRawConstantValue() : GetNonConstantDefaultValue(fieldInfo);

            // 生成签名字符串和完整声明
            Signature = GetFieldSignature(GetKeywordSnippetInSignature(IsConstant, IsStatic, IsReadOnly), DefaultValue,
                GetFormattedDefaultValue(FieldType, DefaultValue),
                AccessModifierName, FieldTypeName, Name);
            FullDeclarationWithAttributes = GetFullDeclarationWithAttributes(AttributesDeclaration, Signature);
        }

        #region IFieldData 接口实现

        /// <summary>
        /// 是否为只读字段（readonly）
        /// </summary>
        public bool IsReadOnly { get; }

        /// <summary>
        /// 是否为静态字段（static）
        /// </summary>
        public bool IsStatic { get; }

        /// <summary>
        /// 是否为常量字段（const）
        /// </summary>
        public bool IsConstant { get; }

        /// <summary>
        /// 字段默认值
        /// </summary>
        public object DefaultValue { get; }

        /// <summary>
        /// 字段的类型信息
        /// </summary>
        public Type FieldType { get; }

        /// <summary>
        /// 字段类型的可读名称
        /// </summary>
        public string FieldTypeName => FieldType?.GetReadableTypeName();

        /// <summary>
        /// 字段类型的完整可读名称
        /// </summary>
        public string FieldTypeFullName => FieldType?.GetReadableTypeName(true);

        public object GetNonConstantDefaultValue(FieldInfo fieldInfo)
        {
            if (fieldInfo == null)
            {
                throw new ArgumentNullException(nameof(fieldInfo));
            }

            try
            {
                return fieldInfo.IsStatic
                    ? GetStaticValueTypeFieldDefaultValue(fieldInfo)
                    : GetInstanceFieldDefaultValue(fieldInfo);
            }
            catch (Exception ex)
            {
                Debug.LogWarning($"在获取 {fieldInfo.Name} 的自定义默认值时出现预期之外的错误: {ex.Message}");
                return null;
            }
        }

        public AccessModifierType GetFieldAccessModifier(FieldInfo fieldInfo)
        {
            if (fieldInfo == null)
            {
                throw new ArgumentNullException(nameof(fieldInfo));
            }

            // 按照访问修饰符的优先级进行检查
            if (fieldInfo.IsPublic)
            {
                return AccessModifierType.Public;
            }

            if (fieldInfo.IsPrivate)
            {
                return AccessModifierType.Private;
            }

            if (fieldInfo.IsFamily)
            {
                return AccessModifierType.Protected;
            }

            if (fieldInfo.IsAssembly)
            {
                return AccessModifierType.Internal;
            }

            if (fieldInfo.IsFamilyOrAssembly)
            {
                return AccessModifierType.ProtectedInternal;
            }

            if (fieldInfo.IsFamilyAndAssembly)
            {
                return AccessModifierType.PrivateProtected;
            }

            return AccessModifierType.None;
        }

        public string GetFieldSignature(string keywordSnippet, object defaultValue, string formattedDefaultValue,
            string accessModifierName, string fieldTypeName, string name)
        {
            return defaultValue != null
                ? $"{accessModifierName} {keywordSnippet}{fieldTypeName} {name} = {formattedDefaultValue};"
                : $"{accessModifierName} {keywordSnippet}{fieldTypeName} {name};";
        }

        #region IDerivedMemberData 接口实现

        /// <summary>
        /// 成员类型（字段、属性、方法等）
        /// </summary>
        /// <remarks>
        /// 对于 FieldData，此值固定为 MemberTypes.Field
        /// </remarks>
        public MemberTypes MemberType { get; }

        /// <summary>
        /// 成员类型的字符串表示形式
        /// </summary>
        public string MemberTypeName => MemberType.ToString();

        /// <summary>
        /// 访问修饰符类型
        /// </summary>
        public AccessModifierType AccessModifier { get; }

        /// <summary>
        /// 访问修饰符的字符串表示形式
        /// </summary>
        public string AccessModifierName => AccessModifier.ConvertToString();

        /// <summary>
        /// 字段签名字符串，包含访问修饰符、字段修饰符（static/readonly/const）、类型名称和字段名称，不包含特性声明
        /// </summary>
        /// <example>
        ///     <c>public static readonly int myField;</c>
        /// </example>
        public string Signature { get; }

        /// <summary>
        /// 包含特性的完整声明字符串
        /// </summary>
        /// <example>
        ///     <code>
        /// [SerializeField]
        /// public static readonly int myField;
        /// </code>
        /// </example>
        public string FullDeclarationWithAttributes { get; }

        public string GetFullDeclarationWithAttributes(string attributesDeclaration, string signature)
        {
            // 参数验证
            if (string.IsNullOrEmpty(signature))
            {
                throw new ArgumentException("签名字符串不能为空", nameof(signature));
            }

            // 如果没有特性声明，直接返回签名
            if (string.IsNullOrEmpty(attributesDeclaration))
            {
                return signature;
            }

            // 将特性声明和签名用换行符连接
            return attributesDeclaration + "\n" + signature;
        }

        #endregion

        #endregion

        #region 私有静态方法

        /// <summary>
        /// 获取静态值类型字段的默认值
        /// </summary>
        static object GetStaticValueTypeFieldDefaultValue(FieldInfo fieldInfo)
        {
            var staticValue = fieldInfo.GetValue(null);
            // 检查是否为类型的默认值，如果不是默认值才返回
            if (staticValue == null)
            {
                return null;
            }

            return !IsTypeDefaultValue(staticValue, fieldInfo.FieldType) ? staticValue : null;
        }

        /// <summary>
        /// 只获取值类型和字符串类型的实例字段的默认值
        /// </summary>
        static object GetInstanceFieldDefaultValue(FieldInfo fieldInfo)
        {
            var declaringType = fieldInfo.DeclaringType;
            if (declaringType == null || declaringType.IsAbstract || declaringType.IsInterface)
            {
                return null;
            }

            var instance = CreateDeclaringTypeInstance(declaringType);
            if (instance == null)
            {
                return null;
            }

            var fieldValue = fieldInfo.GetValue(instance);
            // 获取不到值也返回 null
            if (fieldValue == null)
            {
                return null;
            }

            return !IsTypeDefaultValue(fieldValue, fieldInfo.FieldType) ? fieldValue : null;
        }

        /// <summary>
        /// 创建声明该字段的类的实例
        /// </summary>
        /// <param name="type">要创建实例的类型，是包含相关字段的一个类</param>
        /// <returns>创建的实例，如果创建失败则返回 null</returns>
        static object CreateDeclaringTypeInstance(Type type)
        {
            try
            {
                return Activator.CreateInstance(type);
            }
            catch (Exception e)
            {
                Debug.LogWarning($"创建 {type.Name} 的实例失败，错误信息: {e.Message}");
                throw;
            }
        }

        /// <summary>
        /// 检查 value 是否为 FieldType 的默认值
        /// </summary>
        /// <param name="value">要检查的值</param>
        /// <param name="type">字段本身的 Type</param>
        /// <returns>如果等于 FieldType 的默认值返回 true，否则返回 false</returns>
        static bool IsTypeDefaultValue(object value, Type type)
        {
            // 针对 string 类型做特殊处理
            if (type == typeof(string))
            {
                return value is string str && string.IsNullOrEmpty(str);
            }

            // 如果不是值类型且不是 string，直接返回 true，当成 value == 默认值处理
            if (!type.IsValueType && type != typeof(string))
            {
                return true;
            }

            try
            {
                var defaultValue = Activator.CreateInstance(type);
                return value.Equals(defaultValue);
            }
            catch (Exception e)
            {
                Debug.LogError($"类型 {type.Name} 的实例创建失败: {e.Message}");
            }

            // 如果以上情况都没有返回，说明有情况没有考虑到，也给它按照 value == 默认值来处理，不准备显示
            return true;
        }

        /// <summary>
        /// 获取关键字片段，用于生成字段签名
        /// </summary>
        static string GetKeywordSnippetInSignature(bool isConst, bool isStatic, bool isReadOnly)
        {
            var keyword = "";
            if (isConst)
            {
                keyword = "const ";
                return keyword;
            }

            switch (isStatic)
            {
                case true when isReadOnly:
                    keyword = "static readonly ";
                    break;
                case true:
                    keyword = "static ";
                    break;
                default:
                {
                    if (isReadOnly)
                    {
                        keyword = "readonly ";
                    }

                    break;
                }
            }

            return keyword;
        }

        /// <summary>
        /// 格式化默认值以便在代码中显示
        /// </summary>
        /// <param name="fieldType">字段的类型</param>
        /// <param name="value">要格式化的值</param>
        /// <returns>格式化后的字符串</returns>
        public static string GetFormattedDefaultValue(Type fieldType, object value)
        {
            if (value != null && fieldType.IsEnum)
            {
                var enumTypeName = fieldType.Name;
                var enumName = Enum.GetName(fieldType, value);
                return $"{enumTypeName}.{enumName}";
            }

            return value switch
            {
                null => "null",
                string str => $"\"{str}\"",
                bool b => b ? "true" : "false",
                float f => $"{f}f",
                char c => $"'{c}'",
                double d => $"{d}d",
                decimal m => $"{m}m",
                uint u => $"{u}u",
                long l => $"{l}L",
                ulong ul => $"{ul}ul",
                short s => $"{s}",
                ushort us => $"{us}",
                byte b2 => $"{b2}",
                sbyte sb => $"{sb}",
                _ => value.ToString()
            };
        }

        #endregion
    }
}
