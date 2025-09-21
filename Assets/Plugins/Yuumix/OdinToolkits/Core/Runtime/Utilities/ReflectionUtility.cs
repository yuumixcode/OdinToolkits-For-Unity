using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Yuumix.OdinToolkits.Core
{
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

    public static class ReflectionUtility
    {
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

        public static List<string> GetNamespacesInAssembly(Assembly assembly)
        {
            Type[] types = assembly.GetTypes();
            List<string> namespaces = types.Select(type => type.Namespace)
                .Where(ns => ns != null)
                .Distinct()
                .ToList();
            return namespaces;
        }

        public static string ConvertToString(this AccessModifierType modifier)
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
