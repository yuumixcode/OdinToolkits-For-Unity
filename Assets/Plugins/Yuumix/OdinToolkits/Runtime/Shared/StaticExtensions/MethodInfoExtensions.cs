using System;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Sirenix.Utilities;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Shared
{
    public static class MethodInfoExtensions
    {
        /// <summary>
        /// Returns the specified method's full name "methodName(argType1 arg1, argType2 arg2, etc)"
        /// Uses the specified gauntlet to replaces type names, ex: "int" instead of "Int32"
        /// </summary>
        public static string GetFullMethodName(this MethodBase method, string extensionMethodPrefix)
        {
            var stringBuilder = new StringBuilder();
            if (method.IsExtensionMethod())
            {
                stringBuilder.Append(extensionMethodPrefix);
            }

            stringBuilder.Append(method.IsConstructor ? method.DeclaringType?.Name : method.Name);
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

            stringBuilder.Append("(");
            stringBuilder.Append(method.GetParamsNames());
            stringBuilder.Append(")");
            return stringBuilder.ToString();
        }

        /// <summary>
        /// Returns a string representing the passed method parameters names. Ex "int num, float damage, Transform target"
        /// </summary>
        public static string GetParamsNames(this MethodBase method)
        {
            ParameterInfo[] parameterInfoArray = method.IsExtensionMethod()
                ? method.GetParameters().Skip(1).ToArray()
                : method.GetParameters();
            var stringBuilder = new StringBuilder();
            var index = 0;
            for (int length = parameterInfoArray.Length; index < length; ++index)
            {
                ParameterInfo parameterInfo = parameterInfoArray[index];
                string niceName = parameterInfo.ParameterType.GetNiceName();
                stringBuilder.Append(niceName);
                stringBuilder.Append(" ");
                stringBuilder.Append(parameterInfo.Name);
                if (index < length - 1)
                {
                    stringBuilder.Append(", ");
                }
            }

            return stringBuilder.ToString();
        }

        public static string GetFullMethodName(this MethodBase method) => method.GetFullMethodName("[ext] ");

        public static bool IsExtensionMethod(this MethodBase method)
        {
            Type declaringType = method.DeclaringType;
            return declaringType != null &&
                   declaringType.IsSealed && !declaringType.IsGenericType && !declaringType.IsNested &&
                   method.IsDefined(typeof(ExtensionAttribute), false);
        }

        [MultiLanguageComment("获取方法的访问修饰符类型", "Get the access modifier type of a method")]
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
    }
}
