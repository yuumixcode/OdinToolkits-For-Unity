using System;
using System.Collections.Generic;
using System.Reflection;

namespace Yuumix.OdinToolkits.Editor
{
    public static class TypeDataAnalyzer
    {
        public static TypeData Analyze(Type type) => TypeData.FromType(type);

        public static TypeData[] Analyze(Type[] types)
        {
            var results = new TypeData[types.Length];
            for (var i = 0; i < results.Length; ++i)
            {
                results[i] = Analyze(types[i]);
            }

            return results;
        }

        public static TypeData[] Analyze(Assembly assembly) => Analyze(assembly.GetTypes());

        public static TypeData[] Analyze(Assembly[] assemblies)
        {
            var types = new List<Type>();
            foreach (Assembly assembly in assemblies)
            {
                types.AddRange(assembly.GetTypes());
            }

            return Analyze(types.ToArray());
        }
    }
}
