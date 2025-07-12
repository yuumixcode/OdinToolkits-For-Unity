using Sirenix.Utilities;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

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
            var parameterInfoArray = method.IsExtensionMethod()
                ? method.GetParameters().Skip(1).ToArray()
                : method.GetParameters();
            var stringBuilder = new StringBuilder();
            var index = 0;
            for (var length = parameterInfoArray.Length; index < length; ++index)
            {
                var parameterInfo = parameterInfoArray[index];
                var niceName = parameterInfo.ParameterType.GetNiceName();
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
            var declaringType = method.DeclaringType;
            return declaringType.IsSealed && !declaringType.IsGenericType && !declaringType.IsNested &&
                   method.IsDefined(typeof(ExtensionAttribute), false);
        }
    }
}
