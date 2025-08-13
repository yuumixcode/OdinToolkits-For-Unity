using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Yuumix.Universal
{
    [BilingualComment("访问修饰符类型枚举", "Access modifier type enumeration")]
    public enum AccessModifierType
    {
        Public = 0,
        ProtectedInternal = 1,
        Protected = 2,
        Internal = 4,
        PrivateProtected = 8,
        Private = 16,
        None = 32
    }

    [BilingualComment("反射工具类", "Reflection utility class")]
    public static class ReflectionUtility
    {
        [BilingualComment("自定义成员类型映射字典", "Custom member type mapping dictionary")]
        public static readonly Dictionary<MemberTypes, int> CustomMemberTypesMap = new Dictionary<MemberTypes, int>
        {
            { MemberTypes.Method, 0 },
            { MemberTypes.Event, 1 },
            { MemberTypes.Property, 2 },
            { MemberTypes.Field, 3 },
            { MemberTypes.Constructor, 4 },
            { MemberTypes.TypeInfo, 5 },
            { MemberTypes.NestedType, 6 },
            { MemberTypes.Custom, 7 }
        };

        [BilingualComment("根据部分程序集名称获取程序集数组", "Get an array of assemblies based on a partial assembly name")]
        public static Assembly[] GetAssembliesOfNameContainString(string partOfAssemblyName)
        {
            try
            {
                Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies()
                    .Where(assembly => assembly.FullName.Contains(partOfAssemblyName))
                    .ToArray();
                return assemblies.Length > 0 ? assemblies : Array.Empty<Assembly>();
            }
            catch (Exception ex)
            {
                throw new Exception($"An error occurred: {ex.Message}");
            }
        }

        [BilingualComment("获取程序集中的命名空间列表", "Get a list of namespaces in an assembly")]
        public static List<string> GetNamespacesInAssembly(Assembly assembly)
        {
            Type[] types = assembly.GetTypes();
            List<string> namespaces = types.Select(type => type.Namespace)
                .Where(ns => ns != null)
                .Distinct()
                .ToList();
            return namespaces;
        }

        [BilingualComment("获取访问修饰符的字符串表示", "Get the string representation of an access modifier")]
        public static string GetAccessModifierString(this AccessModifierType modifier)
        {
            return modifier switch
            {
                AccessModifierType.Public => "public",
                AccessModifierType.Private => "private",
                AccessModifierType.Protected => "protected",
                AccessModifierType.Internal => "internal",
                AccessModifierType.ProtectedInternal => "protected internal",
                AccessModifierType.PrivateProtected => "private protected",
                _ => ""
            };
        }
    }
}
