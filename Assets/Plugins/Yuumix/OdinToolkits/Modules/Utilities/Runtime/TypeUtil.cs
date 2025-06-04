using System;
using System.Collections.Generic;
using System.Linq;

namespace Yuumix.OdinToolkits.Modules.Utilities.Runtime
{
    public static class TypeUtil
    {
        public static readonly Dictionary<string, string> TypeAliasMap = new Dictionary<string, string>
        {
            { "Single", "float" },
            { "Int32", "int" },
            { "String", "string" },
            { "Boolean", "bool" },
            { "Void", "void" }
        };

        public static string GetReadableTypeName(Type type)
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
            genericTypeName = genericTypeName[..genericTypeName.IndexOf('`')]; // 去掉 ` 和数字
            var genericArguments = type.GetGenericArguments();
            var argumentNames = string.Join(", ", genericArguments.Select(GetReadableTypeName));
            return $"{genericTypeName}<{argumentNames}>";
        }
    }
}