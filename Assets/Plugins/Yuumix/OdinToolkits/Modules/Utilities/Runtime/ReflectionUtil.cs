using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Yuumix.OdinToolkits.Modules.Utilities.Runtime
{
    [Flags]
    public enum AccessModifierType
    {
        Public = 0,
        Private = 2,
        Protected = 4,
        Internal = 8,
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

        // Odin 提供的两个关于反射的工具类
        // Sirenix.Utilities.AssemblyUtilities
        // Sirenix.Utilities.TypeExtensions
        // ---
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
                // 记录日志或进行其他异常处理
                Console.WriteLine($"An error occurred: {ex.Message}");
                return Array.Empty<Assembly>();
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

        public static string GetReadableEventReturnType(EventInfo eventInfo)
        {
            var eventHandlerType = eventInfo.EventHandlerType;
            var invokeMethod = eventHandlerType.GetMethod("Invoke");
            // 这里默认情况下不可能为空，因为事件类型肯定有Invoke方法，除非高级手段做过修改
            if (invokeMethod == null)
            {
                return "Action";
            }

            var parameters = invokeMethod.GetParameters();
            var paramTypes = parameters.Select(p => TypeUtil.GetReadableTypeName(p.ParameterType)).ToList();
            if (invokeMethod.ReturnType != typeof(void))
            {
                var returnType = TypeUtil.GetReadableTypeName(invokeMethod.ReturnType);
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

        public static AccessModifierType GetMethodAccessModifierType(MethodInfo methodInfo)
        {
            if (methodInfo.IsPublic)
            {
                return AccessModifierType.Public;
            }

            if (methodInfo.IsPrivate)
            {
                return AccessModifierType.Private;
            }

            return methodInfo.IsFamily ? AccessModifierType.Protected : AccessModifierType.Internal;
        }

        public static string GetFullMethodSignature(MethodInfo method)
        {
            var parameters = string.Join(", ",
                method.GetParameters().Select(p => $"{TypeUtil.GetReadableTypeName(p.ParameterType)} {p.Name}"));
            var returnType = TypeUtil.GetReadableTypeName(method.ReturnType);
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
    }
}