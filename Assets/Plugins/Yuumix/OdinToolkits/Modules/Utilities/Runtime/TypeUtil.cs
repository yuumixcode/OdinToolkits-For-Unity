using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Yuumix.OdinToolkits.Modules.CustomExtensions.Enums;

namespace Yuumix.OdinToolkits.Modules.Utilities.Runtime
{
    /// <summary>
    /// 和 Type 类型相关的工具集
    /// <br/>
    /// Utility related to Type 
    /// </summary>
    /// <remarks>
    /// <c>2025-06-12 Yuumix Zeus 确认注释内容</c><br/>
    /// </remarks>
    public static class TypeUtil
    {
        /// <summary>
        /// 将系统类型名称映射到其 C# 别名的字典
        /// <br/>
        /// A dictionary mapping system type names to their C# aliases.
        /// </summary>
        [LocalizedComment("类型别名映射表", "Type alias mapping table")]
        public static readonly Dictionary<string, string> TypeAliasMap = new Dictionary<string, string>
        {
            { "Single", "float" },
            { "Int32", "int" },
            { "String", "string" },
            { "Boolean", "bool" },
            { "Void", "void" }
        };

        #region static extensions

        /// <summary>
        /// 将反射获取到的系统类型名称转换为人类可读的 C# 风格类型名称
        /// </summary>
        /// <param name="type">想要获取可读性强的类型定义名称字符串的 Type 对象</param>
        /// <returns>
        /// 可读性高的类型名称
        /// </returns>
        /// <example>
        /// <c>Single => float </c><br/>
        /// <c>Enumerable`1 => Enumerable&lt;T&gt;</c>
        /// </example>
        [LocalizedComment("将反射获取到的系统类型名称转换为人类可读的 C# 风格类型名称",
            "Convert the system type name obtained by reflection to a human-readable C-style type name")]
        public static string GetReadableTypeName(this Type type)
        {
            if (!type.IsGenericType)
            {
                var targetTypeName = type.Name;
                if (targetTypeName.EndsWith("obj") && targetTypeName.Length > 3)
                {
                    targetTypeName = targetTypeName[..^3];
                }

                return TypeAliasMap.GetValueOrDefault(targetTypeName, targetTypeName);
            }

            var genericTypeName = type.GetGenericTypeDefinition().Name;
            // 去掉 ` 和数字，Enumerable`1 -> Enumerable
            genericTypeName = genericTypeName[..genericTypeName.IndexOf('`')];
            var genericArguments = type.GetGenericArguments();
            var argumentNames = string.Join(", ", genericArguments.Select(GetReadableTypeName));
            return $"{genericTypeName}<{argumentNames}>";
        }

        /// <summary>
        /// 获取 Type 对象的类型种类，类型种类枚举 (TypeCategory) 包括 class，interface，struct，enum，delegate 
        /// </summary>
        /// <returns>TypeCategory 枚举</returns>
        [LocalizedComment("获取 Type 对象的类别种类，类别种类枚举(TypeCategory)包括 class，interface，struct，enum，delegate ",
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
        /// 判断指定类型是否为委托类型
        /// </summary>
        [LocalizedComment("判断指定类型是否为委托类型", 
            "Determines if the specified type is a delegate type.")]
        public static bool IsDelegate(this Type type)
        {
            return type.IsSubclassOf(typeof(Delegate)) || type.IsSubclassOf(typeof(MulticastDelegate));
        }

        #endregion
    }
}
