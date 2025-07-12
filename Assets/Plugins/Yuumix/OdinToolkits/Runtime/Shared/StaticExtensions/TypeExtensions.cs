using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Yuumix.OdinToolkits.Core;
using UnityEngine;
using Yuumix.OdinToolkits.Shared;
using Yuumix.OdinToolkits.Modules.CustomExtensions;

namespace Yuumix.OdinToolkits.Shared
{
    /// <summary>
    /// Type 类型相关的工具集，包含静态扩展（本身也是一种静态方法）
    /// </summary>
    /// <remarks>
    ///     <c>2025-06-13 Yuumix Zeus 确认注释内容</c><br />
    /// </remarks>
    [MultiLanguageComment("Type 类型相关的工具集，包含静态扩展（本身也是一种静态方法）",
        "Type-related Utility, including static extensions (it is also a static method in itself)")]
    public static class TypeExtensions
    {
        /// <summary>
        /// 将系统类型名称映射到其 C# 别名的字典
        /// <br />
        /// A dictionary mapping system type names to their C# aliases.
        /// </summary>
        [MultiLanguageComment("类型别名映射表", "Type alias mapping table")]
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
#if ODIN_INSPECTOR_3 || ODIN_INSPECTOR_3_1|| ODIN_INSPECTOR_3_2|| ODIN_INSPECTOR_3_3
        /// <summary>
        /// 将反射获取到的系统类型名称转换为人类可读的 C# 风格类型名称
        /// </summary>
        /// <param name="type">想要获取可读性强的类型定义名称字符串的 Type 对象</param>
        /// <param name="useFullName">是否获取包含命名空间的类名</param>
        /// <returns>
        /// 可读性高的类型名称
        /// </returns>
        /// <example>
        ///     <c>Single => float </c><br />
        ///     <c>Enumerable`1 => Enumerable&lt;T&gt;</c>
        /// </example>
        [MultiLanguageComment("将反射获取到的系统类型名称转换为人类可读的 C# 风格类型名称",
            "Convert the system type name obtained by reflection to a human-readable C-style type name")]
        public static string GetReadableTypeName(this Type type, bool useFullName = false)
        {
            // TypeExtensions
            var targetTypeName = type.GetNiceName();
            if (useFullName)
            {
                targetTypeName = type.GetNiceFullName();
            }

            // 移除 obj 的后缀，针对 Unity 的特殊处理
            if (targetTypeName.EndsWith("obj") && targetTypeName.Length > 3)
            {
                targetTypeName = targetTypeName[..^3];
            }

            if (TypeAliasMap.TryGetValue(type, out var alias))
            {
                targetTypeName = alias;
            }

            return targetTypeName;
        }
#endif
        /// <summary>
        /// 获取 Type 对象的类型种类，类型种类枚举 (TypeCategory) 包括 class，interface，struct，enum，delegate
        /// </summary>
        /// <returns>TypeCategory 枚举</returns>
        [MultiLanguageComment("获取 Type 对象的类别种类，类别种类枚举(TypeCategory)包括 class，interface，struct，enum，delegate ",
            "Get TypeCategory of Type instance, Enum TypeCategory contains class, interface, struct, enum, delegate")]
        public static TypeCategory GetTypeCategory(this Type type)
        {
            if (type.IsClass)
            {
                return TypeCategory.Class;
            }

            if (type.IsInterface)
            {
                return TypeCategory.Interface;
            }

            if (type.IsValueType && !type.IsEnum)
            {
                return TypeCategory.Struct;
            }

            if (type.IsEnum)
            {
                return TypeCategory.Enum;
            }

            if (type.IsDelegate())
            {
                return TypeCategory.Delegate;
            }

            Debug.LogError("出现不存在的 TypeCategory，请补充枚举或者修改参数");
            return TypeCategory.Unknown;
        }

        /// <summary>
        /// 获取类型声明字符串
        /// </summary>
        /// <returns>
        /// 返回字符串，形如：<br />
        /// <c>
        /// [Serializable]
        /// public class ForTestTypeUtilGenericNestedClass&lt;TCollection, TItem&gt;: System.Object
        /// where TCollection : System.Collections.Generic.IEnumerable&lt;Item&gt;
        /// </c>
        /// </returns>
        [MultiLanguageComment("获取类型声明字符串，包含特性，基类，泛型，接口",
            "Get type declaration string, including attribute, base classes, generics, interfaces")]
        public static string GetTypeDeclaration(this Type type)
        {
            var sb = new StringBuilder();
            var attributes = type.GetCustomAttributes(false);
            // 1. 添加特性部分，只包含特性名称，不包含特性参数
            foreach (var attr in attributes)
            {
                var attributeName = attr.GetType().Name;
                if (attributeName.EndsWith("Attribute"))
                {
                    attributeName = attributeName[..^"Attribute".Length];
                }

                sb.AppendLine($"[{attributeName}]");
            }

            // 2. 添加访问修饰符
            sb.Append(GetTypeAccessModifier(type));

            // 3. 添加类修饰符
            // type.IsSealed && type.IsAbstract == static
            if (type.IsStatic())
            {
                sb.Append("static ");
            }
            else if (type.IsAbstract)
            {
                sb.Append("abstract ");
            }
            else if (type.IsSealed)
            {
                sb.Append("sealed ");
            }

            // 4. 添加类型关键字
            var category = type.GetTypeCategory();
            switch (category)
            {
                case TypeCategory.Class:
                    sb.Append("class ");
                    break;
                case TypeCategory.Interface:
                    sb.Append("interface ");
                    break;
                case TypeCategory.Struct:
                    sb.Append("struct ");
                    break;
                case TypeCategory.Enum:
                    sb.Append("enum ");
                    break;
                case TypeCategory.Delegate:
                    sb.Append("delegate ");
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            // 5. 添加类名（处理泛型）
            sb.Append(type.GetReadableTypeName());

            // 6. 添加基类和接口
            var inheritTypes = new List<string>();

            // 添加直接基类
            if (type.BaseType != null)
            {
                inheritTypes.Add(type.BaseType.GetReadableTypeName(true));
            }

            // 添加实现的接口，忽略编译时生成的接口，包含所有从基类继承的接口
            var interfaces = type.GetInterfaces()
                .Where(i => !i.IsDefined(typeof(CompilerGeneratedAttribute), false));

            inheritTypes.AddRange(interfaces.Select(x => x.GetReadableTypeName(true)));

            if (inheritTypes.Count > 0)
            {
                sb.Append(" : " + string.Join(", ", inheritTypes));
            }

            // 7. 添加泛型约束
            if (type.IsGenericType)
            {
                sb.AppendLine(" " + type.GetGenericConstraintsString(true));
            }

            return sb.ToString();
        }

        /// <summary>
        /// 获取类型的访问修饰符字符串
        /// </summary>
        /// <returns>访问修饰符字符串+空格</returns>
        [MultiLanguageComment("获取类型的访问修饰符字符串", "Get the access modifier string of the type")]
        public static string GetTypeAccessModifier(Type type)
        {
            if (type.IsNested)
            {
                if (type.IsNestedPublic)
                {
                    return "public ";
                }

                if (type.IsNestedPrivate)
                {
                    return "private ";
                }

                if (type.IsNestedFamily)
                {
                    return "protected "; // protected
                }

                if (type.IsNestedAssembly)
                {
                    return "internal "; // internal
                }

                if (type.IsNestedFamORAssem)
                {
                    return "protected internal ";
                }
            }
            else
            {
                return type.IsPublic ? "public " : "internal ";
            }

            Debug.LogWarning(nameof(TypeExtensions) + "." + nameof(GetTypeAccessModifier) + " 出现未考虑到的情况");
            return "出现未考虑到的情况";
        }

        /// <summary>
        /// 判断指定类型是否为委托类型
        /// </summary>
        [MultiLanguageComment("判断指定类型是否为委托类型",
            "Determines if the specified type is a delegate type.")]
        public static bool IsDelegate(this Type type) =>
            type.IsSubclassOf(typeof(Delegate)) || type.IsSubclassOf(typeof(MulticastDelegate));

        /// <summary>
        /// 获取开发者声明的字段，剔除自动属性生成的字段
        /// </summary>
        /// <returns>符合条件的 FieldInfo 数组</returns>
        [MultiLanguageComment("获取开发者声明的字段，剔除自动属性生成的字段",
            "Get the fields declared by the developer and remove the fields generated by the automatic attributes")]
        public static FieldInfo[] GetUserDefinedFields(this Type type)
        {
            return type.GetRuntimeFields()
                .Where(f => !f.IsSpecialName && !IsAutoPropertyBackingField(f))
                .ToArray();
        }

        static bool IsAutoPropertyBackingField(FieldInfo field) =>
            field.Name.Contains("k__BackingField") ||
            field.Name.Contains("__BackingField");
    }
}
