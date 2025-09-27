using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules.QoderTypeAnalyzer
{
    /// <summary>
    /// 类型分析扩展方法
    /// </summary>
    public static class TypeAnalysisExtensions
    {
        /// <summary>
        /// 获取类型种类
        /// </summary>
        public static TypeCategory GetTypeCategory(this Type type)
        {
            if (type.IsDelegate())
            {
                return TypeCategory.Delegate;
            }

            if (type.IsClass)
            {
                return type.IsRecord() ? TypeCategory.Record : TypeCategory.Class;
            }

            if (type.IsInterface)
            {
                return TypeCategory.Interface;
            }

            if (type.IsValueType && !type.IsEnum)
            {
                return type.IsRecord() ? TypeCategory.Record : TypeCategory.Struct;
            }

            if (type.IsEnum)
            {
                return TypeCategory.Enum;
            }

            return TypeCategory.Unknown;
        }

        /// <summary>
        /// 判断类型是否为委托
        /// </summary>
        public static bool IsDelegate(this Type type)
        {
            return typeof(Delegate).IsAssignableFrom(type);
        }

        /// <summary>
        /// 判断指定类型是否为 record
        /// </summary>
        public static bool IsRecord(this Type type)
        {
            if (type == null)
                return false;

            // 检查是否存在名为 "<Clone>$" 的方法（record 类型特有）
            MethodInfo cloneMethod = type.GetMethod("<Clone>$",
                BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

            return cloneMethod != null;
        }

        /// <summary>
        /// 获取完整类型声明字符串（支持特性过滤）
        /// </summary>
        public static string GetTypeDeclaration(this Type type)
        {
            var sb = new StringBuilder();
            
            // 1. 添加特性部分（使用当前的特性过滤器）
            var attributes = type.GetCustomAttributes(false)
                .Where(attr => AttributeFilters.CurrentFilter.ShouldInclude(attr.GetType()))
                .ToArray();
                
            foreach (object attr in attributes)
            {
                string attributeName = attr.GetType().Name;
                if (attributeName.EndsWith("Attribute"))
                {
                    attributeName = attributeName[..^"Attribute".Length];
                }
                sb.AppendLine($"[{attributeName}]");
            }

            // 2. 添加访问修饰符
            sb.Append(GetTypeAccessModifierString(type));

            // 3. 添加类修饰符
            if (type.IsStatic())
            {
                sb.Append("static ");
            }
            else if (type.IsAbstract && !type.IsInterface)
            {
                sb.Append("abstract ");
            }
            else if (type.IsSealed && !type.IsEnum && !type.IsDelegate())
            {
                sb.Append("sealed ");
            }

            // 4. 添加类型关键字
            TypeCategory category = type.GetTypeCategory();
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
                case TypeCategory.Record:
                    sb.Append("record ");
                    break;
            }

            // 特殊情况，委托的声明需要单独处理
            if (category == TypeCategory.Delegate)
            {
                MethodInfo invokeMethod = type.GetMethod("Invoke");
                if (invokeMethod != null)
                {
                    sb.Append(invokeMethod.ReturnType.GetReadableTypeName());
                    sb.Append(" ");
                    sb.Append(type.GetReadableTypeName());
                    sb.Append("(");
                    
                    var parameters = invokeMethod.GetParameters();
                    sb.Append(string.Join(", ", parameters.Select(p => 
                        $"{p.ParameterType.GetReadableTypeName()} {p.Name}")));
                    
                    sb.Append(");");
                }
            }
            else
            {
                // 5. 添加类名（处理泛型）
                sb.Append(type.GetReadableTypeName());

                // 6. 添加基类和接口（每个接口另起一行）
                var inheritTypes = new List<string>();

                // 添加直接基类
                if (type.BaseType != null && type.BaseType != typeof(object))
                {
                    inheritTypes.Add(type.BaseType.GetReadableTypeName(true));
                }

                // 添加实现的接口
                var interfaces = type.GetInterfaces()
                    .Where(i => !i.IsDefined(typeof(CompilerGeneratedAttribute), false))
                    .Select(i => i.GetReadableTypeName(true))
                    .ToArray();

                if (inheritTypes.Count > 0 || interfaces.Length > 0)
                {
                    sb.Append(" : ");
                    
                    // 基类和类型声明在同一行
                    if (inheritTypes.Count > 0)
                    {
                        sb.Append(inheritTypes[0]);
                        if (interfaces.Length > 0)
                        {
                            sb.AppendLine(",");
                        }
                    }
                    
                    // 每个接口另起一行
                    for (int i = 0; i < interfaces.Length; i++)
                    {
                        if (inheritTypes.Count > 0 || i > 0)
                        {
                            sb.Append("    "); // 缩进
                        }
                        sb.Append(interfaces[i]);
                        if (i < interfaces.Length - 1)
                        {
                            sb.AppendLine(",");
                        }
                    }
                }
            }

            return sb.ToString();
        }

        /// <summary>
        /// 获取类型的访问修饰符字符串
        /// </summary>
        private static string GetTypeAccessModifierString(Type type)
        {
            if (type.IsNested)
            {
                if (type.IsNestedPublic) return "public ";
                if (type.IsNestedPrivate) return "private ";
                if (type.IsNestedFamily) return "protected ";
                if (type.IsNestedAssembly) return "internal ";
                if (type.IsNestedFamORAssem) return "protected internal ";
                if (type.IsNestedFamANDAssem) return "private protected ";
            }
            else
            {
                return type.IsPublic ? "public " : "internal ";
            }
            return "";
        }

        /// <summary>
        /// 判断类型是否为静态类
        /// </summary>
        public static bool IsStatic(this Type type)
        {
            return type.IsAbstract && type.IsSealed && !type.IsInterface;
        }

        /// <summary>
        /// 获取可读的类型名称
        /// </summary>
        public static string GetReadableTypeName(this Type type, bool useFullName = false)
        {
            if (type == null) return "void";
            
            // 处理泛型类型
            if (type.IsGenericType)
            {
                var genericTypeName = type.Name.Split('`')[0];
                var genericArgs = string.Join(", ", type.GetGenericArguments().Select(t => t.GetReadableTypeName(useFullName)));
                var nameWithNamespace = useFullName && !string.IsNullOrEmpty(type.Namespace) 
                    ? $"{type.Namespace}.{genericTypeName}" 
                    : genericTypeName;
                return $"{nameWithNamespace}<{genericArgs}>";
            }
            
            // 处理数组类型
            if (type.IsArray)
            {
                var elementType = type.GetElementType();
                var rank = type.GetArrayRank();
                var brackets = rank == 1 ? "[]" : $"[{new string(',', rank - 1)}]";
                return $"{elementType.GetReadableTypeName(useFullName)}{brackets}";
            }
            
            // 基本类型别名映射
            var typeAliases = new Dictionary<Type, string>
            {
                { typeof(void), "void" },
                { typeof(bool), "bool" },
                { typeof(byte), "byte" },
                { typeof(sbyte), "sbyte" },
                { typeof(char), "char" },
                { typeof(short), "short" },
                { typeof(ushort), "ushort" },
                { typeof(int), "int" },
                { typeof(uint), "uint" },
                { typeof(long), "long" },
                { typeof(ulong), "ulong" },
                { typeof(float), "float" },
                { typeof(double), "double" },
                { typeof(decimal), "decimal" },
                { typeof(string), "string" },
                { typeof(object), "object" }
            };
            
            if (typeAliases.TryGetValue(type, out string alias))
            {
                return alias;
            }
            
            return useFullName && !string.IsNullOrEmpty(type.Namespace) 
                ? $"{type.Namespace}.{type.Name}" 
                : type.Name;
        }
    }
}