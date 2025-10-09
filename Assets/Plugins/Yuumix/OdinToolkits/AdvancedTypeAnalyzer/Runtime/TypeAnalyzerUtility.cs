using System;
using UnityEngine;

namespace Yuumix.OdinToolkits.AdvancedTypeAnalyzer
{
    /// <summary>
    /// 类型分析器工具类
    /// </summary>
    public static class TypeAnalyzerUtility
    {
        /// <summary>
        /// 获取格式化的完整特性签名字符串，返回 true 表示该特性支持格式化为完整特性签名字符串，返回 false 表示不支持。
        /// </summary>
        public static bool TryGetFormatedAttributeWithFullParameter(object attrInstance,
            out string attributeFullSignature)
        {
            var attributeFullName = GetAttributeNameWithoutSuffix(attrInstance.GetType().FullName);
            switch (attrInstance)
            {
                case ObsoleteAttribute obsoleteAttr:
                    attributeFullSignature = obsoleteAttr.IsError
                        ? $"[{attributeFullName}(\"{obsoleteAttr.Message}\", true)]"
                        : $"[{attributeFullName}(\"{obsoleteAttr.Message}\")]";
                    return true;
                case TooltipAttribute tooltipAttr:
                    attributeFullSignature = $"[{attributeFullName}(\"{tooltipAttr.tooltip}\")]";
                    return true;
                case RangeAttribute rangeAttr:
                    attributeFullSignature =
                        $"[{attributeFullName}({rangeAttr.min}, {rangeAttr.max})]";
                    return true;
                case ColorUsageAttribute colorUsageAttr:
                    attributeFullSignature =
                        $"[{attributeFullName}({colorUsageAttr.showAlpha.ToString().ToLower()}, " +
                        $"{colorUsageAttr.hdr.ToString().ToLower()})]";
                    return true;
            }

            attributeFullSignature = string.Empty;
            return false;
        }

        public static string GetAttributeNameWithoutSuffix(string attributeName)
        {
            if (attributeName.EndsWith("Attribute"))
            {
                attributeName = attributeName[..^"Attribute".Length];
            }

            return attributeName;
        }

        /// <summary>
        /// 提供的值被视为类型的默认值，返回 true 表示被视为类型的默认值，返回 false 表示不是。
        /// </summary>
        /// <param name="value"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool TreatedAsTypeDefaultValue(object value, Type type)
        {
            if (value is string str)
            {
                return string.IsNullOrEmpty(str);
            }

            if (type.IsReferenceTypeExcludeString() || type.IsAbstractOrInterface())
            {
                return true;
            }

            var defaultValue = Activator.CreateInstance(type);
            return defaultValue == null || value.Equals(defaultValue);
        }

        /// <summary>
        /// 获取关键字片段，用于生成字段签名
        /// </summary>
        public static string GetKeywordSnippetInSignature(bool isConst, bool isStatic, bool isReadOnly)
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

            if (fieldType == typeof(Quaternion) ||
                fieldType == typeof(Vector3) ||
                fieldType == typeof(Vector2) ||
                fieldType == typeof(Vector4))
            {
                var typeName = fieldType.Name;
                var formattedValue = "new " + typeName + value;
                return formattedValue;
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
        
    }
}
