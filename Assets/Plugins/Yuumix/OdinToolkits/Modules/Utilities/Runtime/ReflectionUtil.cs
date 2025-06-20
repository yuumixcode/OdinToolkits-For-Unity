using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Yuumix.OdinToolkits.Modules.Utilities.Runtime
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

    public static class ReflectionUtil
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
                var assemblies = AppDomain.CurrentDomain.GetAssemblies()
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
            var types = assembly.GetTypes();
            var namespaces = types.Select(type => type.Namespace)
                .Where(ns => ns != null)
                .Distinct()
                .ToList();
            return namespaces;
        }

        public static string GetReadableEventReturnType(this EventInfo eventInfo)
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

        public static AccessModifierType GetFieldAccessModifierType(this FieldInfo field)
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

        public static AccessModifierType GetPropertyAccessModifierType(this PropertyInfo property)
        {
            var getMethod = property.GetGetMethod(true);
            var setMethod = property.GetSetMethod(true);

            AccessModifierType? getAccess = getMethod != null ? GetMethodAccessModifierType(getMethod) : null;
            AccessModifierType? setAccess = setMethod != null ? GetMethodAccessModifierType(setMethod) : null;

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

            if (method.IsFamilyAndAssembly)
            {
                return AccessModifierType.PrivateProtected;
            }

            return AccessModifierType.None;
        }

        public static AccessModifierType GetEventAccessModifierType(this EventInfo eventInfo)
        {
            var addMethod = eventInfo.GetAddMethod(true);
            var removeMethod = eventInfo.GetRemoveMethod(true);
            var invokeMethod = eventInfo.GetRaiseMethod(true);
            if (invokeMethod != null)
            {
                return GetMethodAccessModifierType(invokeMethod);
            }

            if (addMethod != null && removeMethod != null)
            {
                return GetMethodAccessModifierType(
                    (int)GetMethodAccessModifierType(addMethod) <= (int)GetMethodAccessModifierType(removeMethod)
                        ? addMethod
                        : removeMethod);
            }

            if (addMethod != null)
            {
                return GetMethodAccessModifierType(addMethod);
            }

            if (removeMethod != null)
            {
                return GetMethodAccessModifierType(removeMethod);
            }

            return AccessModifierType.None;
        }

        public static string GetMethodFullSignature(this MethodInfo method)
        {
            var parameters = string.Join(", ",
                method.GetParameters().Select(p => $"{p.ParameterType.GetReadableTypeName()} {p.Name}"));
            var returnType = method.ReturnType.GetReadableTypeName();
            var fullSignature = $"{returnType} {method.Name}({parameters})";
            fullSignature = FillSignature(method, fullSignature);
            return fullSignature;

            static string FillSignature(MethodInfo methodInfo, string fullSignature)
            {
                var modifiers = new List<string>();

                if (methodInfo.IsPublic)
                {
                    modifiers.Add("public");
                }
                else if (methodInfo.IsPrivate)
                {
                    modifiers.Add("private");
                }
                else if (methodInfo.IsFamily)
                {
                    modifiers.Add("protected");
                }
                else
                {
                    modifiers.Add("internal");
                }

                if (methodInfo.IsStatic)
                {
                    modifiers.Add("static");
                }

                if (methodInfo.IsAbstract)
                {
                    modifiers.Add("abstract");
                }

                if (methodInfo.IsVirtual)
                {
                    modifiers.Add("virtual");
                }

                return string.Join(" ", modifiers.Concat(new[] { fullSignature }));
            }
        }

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

        public static bool IsConstantField(this FieldInfo field)
        {
            return field.IsLiteral && !field.IsInitOnly;
        }

        public static bool IsStaticEvent(this EventInfo eventInfo)
        {
            var addMethod = eventInfo.GetAddMethod(true);
            var removeMethod = eventInfo.GetRemoveMethod(true);
            var invokeMethod = eventInfo.GetRaiseMethod(true);
            if (invokeMethod != null)
            {
                return invokeMethod.IsStatic;
            }

            if (addMethod != null)
            {
                return addMethod.IsStatic;
            }

            if (removeMethod != null)
            {
                return removeMethod.IsStatic;
            }

            return false;
        }
        
    }
}
