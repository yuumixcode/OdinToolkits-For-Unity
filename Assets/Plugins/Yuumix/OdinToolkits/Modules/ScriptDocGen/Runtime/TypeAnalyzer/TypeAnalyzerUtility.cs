using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Sirenix.Utilities;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules
{
    public static class TypeAnalyzerUtility
    {
        /// <summary>
        /// 将系统类型名称映射到其 C# 别名的字典
        /// <br />
        /// A dictionary mapping system type names to their C# aliases.
        /// </summary>
        public static readonly Dictionary<Type, string> TypeAliasMap = new Dictionary<Type, string>
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
            }
        };

        #region 解析方法成员相关方法

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
                Type[] genericArguments = method.GetGenericArguments();
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
            stringBuilder.Append(GetParamsNamesWithDefaultValue(method));
            stringBuilder.Append(")");
            return stringBuilder.ToString();
        }

        public static string GetParamsNamesWithDefaultValue(MethodBase method)
        {
            ParameterInfo[] parameterInfoArray = method.IsExtensionMethod()
                ? method.GetParameters().Skip(1).ToArray()
                : method.GetParameters();
            var stringBuilder = new StringBuilder();
            var index = 0;
            for (int length = parameterInfoArray.Length; index < length; ++index)
            {
                ParameterInfo parameterInfo = parameterInfoArray[index];
                string niceName = TypeExtensions.GetNiceName(parameterInfo.ParameterType);
                stringBuilder.Append(niceName);
                stringBuilder.Append(" ");
                stringBuilder.Append(parameterInfo.Name);
                // 默认值
                if (parameterInfo.HasDefaultValue)
                {
                    object defaultValue = parameterInfo.DefaultValue;
                    stringBuilder.Append(" = ");
                    string defaultValuePart = GetDefaultValueString(defaultValue, parameterInfo.ParameterType);
                    stringBuilder.Append(defaultValuePart);
                }

                if (index < length - 1)
                {
                    stringBuilder.Append(", ");
                }
            }

            return stringBuilder.ToString();
        }

        static string GetDefaultValueString(object defaultValue, Type parameterType)
        {
            if (defaultValue == null)
            {
                return "null";
            }

            if (parameterType == typeof(string))
            {
                return "\"" + defaultValue + "\"";
            }

            if (parameterType == typeof(bool))
            {
                return defaultValue.ToString().ToLower();
            }

            if (parameterType == typeof(char))
            {
                return "'" + defaultValue + "'";
            }

            if (parameterType.IsEnum)
            {
                return parameterType.Name + "." + defaultValue;
            }

            // 其他类型直接转换为字符串
            return defaultValue.ToString();
        }

        #endregion

        #region 解析事件成员相关方法

        public static AccessModifierType GetEventAccessModifierType(EventInfo eventInfo)
        {
            MethodInfo addMethod = eventInfo.GetAddMethod(true);
            MethodInfo removeMethod = eventInfo.GetRemoveMethod(true);
            MethodInfo invokeMethod = eventInfo.GetRaiseMethod(true);
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

            if (invokeMethod != null)
            {
                return invokeMethod.GetMethodAccessModifierType();
            }

            return AccessModifierType.None;
        }

        public static string GetReadableEventReturnType(EventInfo eventInfo)
        {
            Type eventHandlerType = eventInfo.EventHandlerType;
            MethodInfo invokeMethod = eventHandlerType.GetMethod("Invoke");
            // 这里默认情况下不可能为空，因为事件类型肯定有Invoke方法
            if (invokeMethod == null)
            {
                return "Action";
            }

            ParameterInfo[] parameters = invokeMethod.GetParameters();
            List<string> paramTypes = parameters.Select(p => p.ParameterType.GetReadableTypeName()).ToList();
            if (invokeMethod.ReturnType != typeof(void))
            {
                string returnType = invokeMethod.ReturnType.GetReadableTypeName();
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

        #region 解析属性成员方法

        public static AccessModifierType GetPropertyAccessModifierType(PropertyInfo property)
        {
            MethodInfo getMethod = property.GetGetMethod(true);
            MethodInfo setMethod = property.GetSetMethod(true);

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
