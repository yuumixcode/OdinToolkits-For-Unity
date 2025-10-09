using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using Yuumix.OdinToolkits.AdvancedTypeAnalyzer;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules
{
    /// <summary>
    /// TypeAnalyzer 的帮助类
    /// </summary>
    public static class TypeAnalyzerHelper
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

        #region 解析方法成员相关方法

        public static AccessModifierType GetMethodAccessModifierType(this MethodBase method)
        {
            if (method.IsPublic)
            {
                return AccessModifierType.Public;
            }

            if (method.IsPrivate)
            {
                return AccessModifierType.Private;
            }

            if (method.IsFamily)
            {
                return AccessModifierType.Protected;
            }

            if (method.IsAssembly)
            {
                return AccessModifierType.Internal;
            }

            if (method.IsFamilyOrAssembly)
            {
                return AccessModifierType.ProtectedInternal;
            }

            return method.IsFamilyAndAssembly ? AccessModifierType.PrivateProtected : AccessModifierType.None;
        }

        /// <summary>
        /// 返回方法的完整名称，基于 Odin Inspector 的扩展方法修改
        /// </summary>
        public static string GetFullMethodName(MethodBase method, string extensionMethodPrefix)
        {
            var stringBuilder = new StringBuilder();
            if (method.IsExtensionMethod())
            {
                stringBuilder.Append(extensionMethodPrefix);
            }

            stringBuilder.Append(method.IsConstructor ? method.DeclaringType?.GetReadableTypeName() : method.Name);
            if (method.IsGenericMethod)
            {
                var genericArguments = method.GetGenericArguments();
                stringBuilder.Append("<");
                for (var index = 0; index < genericArguments.Length; ++index)
                {
                    if (index != 0)
                    {
                        stringBuilder.Append(", ");
                    }

                    stringBuilder.Append(genericArguments[index].GetNiceName());
                }

                stringBuilder.Append(">");
            }

            // 转换运算符要特殊处理
            if (method.Name.Contains("op_Implicit") || method.Name.Contains("op_Explicit"))
            {
                stringBuilder.Append(" " + method.GetReturnType().GetReadableTypeName());
            }

            stringBuilder.Append("(");
            stringBuilder.Append(method.GetParamsNamesWithDefaultValue());
            stringBuilder.Append(")");
            return stringBuilder.ToString();
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
