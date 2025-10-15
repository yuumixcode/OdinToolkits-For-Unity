using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Yuumix.OdinToolkits.AdvancedTypeAnalyzer;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules
{
    /// <summary>
    /// 类型分析器工具类
    /// </summary>
    public static class TypeAnalyzerUtility
    {
        /// <summary>
        /// 将系统类型名称映射到其 C# 别名的字典
        /// </summary>
        public static readonly IReadOnlyDictionary<Type, string> TypeAliasMap = new Dictionary<Type, string>
        {
            {
                typeof(void),
                "void"
            },
            {
                typeof(float),
                "float"
            },
            {
                typeof(double),
                "double"
            },
            {
                typeof(sbyte),
                "sbyte"
            },
            {
                typeof(short),
                "short"
            },
            {
                typeof(int),
                "int"
            },
            {
                typeof(long),
                "long"
            },
            {
                typeof(byte),
                "byte"
            },
            {
                typeof(ushort),
                "ushort"
            },
            {
                typeof(uint),
                "uint"
            },
            {
                typeof(ulong),
                "ulong"
            },
            {
                typeof(decimal),
                "decimal"
            },
            {
                typeof(string),
                "string"
            },
            {
                typeof(char),
                "char"
            },
            {
                typeof(bool),
                "bool"
            },
            {
                typeof(float[]),
                "float[]"
            },
            {
                typeof(double[]),
                "double[]"
            },
            {
                typeof(sbyte[]),
                "sbyte[]"
            },
            {
                typeof(short[]),
                "short[]"
            },
            {
                typeof(int[]),
                "int[]"
            },
            {
                typeof(long[]),
                "long[]"
            },
            {
                typeof(byte[]),
                "byte[]"
            },
            {
                typeof(ushort[]),
                "ushort[]"
            },
            {
                typeof(uint[]),
                "uint[]"
            },
            {
                typeof(ulong[]),
                "ulong[]"
            },
            {
                typeof(decimal[]),
                "decimal[]"
            },
            {
                typeof(string[]),
                "string[]"
            },
            {
                typeof(char[]),
                "char[]"
            },
            {
                typeof(bool[]),
                "bool[]"
            },
            {
                typeof(object),
                "object"
            }
        };

        

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
        /// 获取格式化的默认值字符串，用于生成字段签名
        /// </summary>
        public static string GetFormattedDefaultValue(Type memberType, object value)
        {
            if (memberType == typeof(Quaternion) ||
                memberType == typeof(Vector3) ||
                memberType == typeof(Vector2) ||
                memberType == typeof(Vector4))
            {
                var typeName = memberType.Name;
                var formattedValue = "new " + typeName + value;
                return formattedValue;
            }

            if (value != null && memberType.IsEnum)
            {
                var enumTypeName = memberType.Name;
                var enumName = Enum.GetName(memberType, value);
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

        #region 解析属性成员方法

        public static AccessModifierType GetPropertyAccessModifierType(PropertyInfo property)
        {
            var getMethod = property.GetGetMethod(true);
            var setMethod = property.GetSetMethod(true);

            AccessModifierType? getAccess = getMethod != null ? getMethod.GetMethodAccessModifierType() : null;
            AccessModifierType? setAccess = setMethod != null ? setMethod.GetMethodAccessModifierType() : null;

            if (!getAccess.HasValue && !setAccess.HasValue)
            {
                return AccessModifierType.None;
            }

            if (!setAccess.HasValue)
            {
                return getAccess.Value;
            }

            if (!getAccess.HasValue)
            {
                return setAccess.Value;
            }

            return (int)getAccess.Value <= (int)setAccess.Value
                ? getAccess.Value
                : setAccess.Value;
        }

        #endregion

        #region 解析事件成员相关方法

        public static AccessModifierType GetEventAccessModifierType(EventInfo eventInfo)
        {
            var addMethod = eventInfo.GetAddMethod(true);
            var removeMethod = eventInfo.GetRemoveMethod(true);
            var invokeMethod = eventInfo.GetRaiseMethod(true);
            if (addMethod != null && removeMethod != null)
            {
                return ((int)addMethod.GetMethodAccessModifierType() <= (int)removeMethod.GetMethodAccessModifierType()
                    ? addMethod
                    : removeMethod).GetMethodAccessModifierType();
            }

            if (addMethod != null)
            {
                return addMethod.GetMethodAccessModifierType();
            }

            if (removeMethod != null)
            {
                return removeMethod.GetMethodAccessModifierType();
            }

            return invokeMethod != null ? invokeMethod.GetMethodAccessModifierType() : AccessModifierType.None;
        }

        public static string GetReadableEventReturnType(EventInfo eventInfo)
        {
            var eventHandlerType = eventInfo.EventHandlerType;
            var invokeMethod = eventHandlerType.GetMethod("Invoke");
            // 这里默认情况下不可能为空，因为事件类型肯定有Invoke方法
            if (invokeMethod == null)
            {
                return "Action";
            }

            var parameters = invokeMethod.GetParameters();
            var paramTypes = parameters.Select(p => p.ParameterType.GetReadableTypeName()).ToList();
            if (invokeMethod.ReturnType != typeof(void))
            {
                var returnType = invokeMethod.ReturnType.GetReadableTypeName();
                if (paramTypes.Count > 0)
                {
                    return $"Func<{string.Join(", ", paramTypes)}, {returnType}>";
                }

                return $"Func<{returnType}>";
            }

            if (paramTypes.Count > 0)
            {
                return $"Action<{string.Join(", ", paramTypes)}>";
            }

            return "Action";
        }

        public static void IsStaticEvent(EventInfo eventInfo, EventAnalysisData eventData)
        {
            if (eventInfo.GetRaiseMethod(true) != null)
            {
                eventData.isStatic = eventInfo.IsStatic();
            }
            else if (eventInfo.GetAddMethod(true) != null)
            {
                eventData.isStatic = eventInfo.GetAddMethod(true).IsStatic;
            }
            else if (eventInfo.GetRemoveMethod(true) != null)
            {
                eventData.isStatic = eventInfo.GetRemoveMethod(true).IsStatic;
            }
        }

        #endregion

        #region 解析字段成员方法

        public static bool IsConstantField(FieldInfo field) => field.IsLiteral && !field.IsInitOnly;

        public static AccessModifierType GetFieldAccessModifierType(FieldInfo field)
        {
            if (field.IsPublic)
            {
                return AccessModifierType.Public;
            }

            if (field.IsPrivate)
            {
                return AccessModifierType.Private;
            }

            if (field.IsFamily)
            {
                return AccessModifierType.Protected;
            }

            if (field.IsAssembly)
            {
                return AccessModifierType.Internal;
            }

            if (field.IsFamilyOrAssembly)
            {
                return AccessModifierType.ProtectedInternal;
            }

            if (field.IsFamilyAndAssembly)
            {
                return AccessModifierType.PrivateProtected;
            }

            return AccessModifierType.None;
        }

        #endregion
    }
}
