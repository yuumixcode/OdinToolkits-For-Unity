using System;
using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Core
{
    public enum PredefinedAssemblyType
    {
        /// <summary>
        /// 程序集CSharp
        /// </summary>
        AssemblyCSharp,

        /// <summary>
        /// 程序集CSharpEditor
        /// </summary>
        AssemblyCSharpEditor,

        /// <summary>
        /// 程序集CSharpEditorFirstPass
        /// </summary>
        AssemblyCSharpEditorFirstPass,

        /// <summary>
        /// 程序集CSharpFirstPass
        /// </summary>
        AssemblyCSharpFirstPass
    }

    public static class PredefinedAssemblyUtility
    {
        public static PredefinedAssemblyType? GetAssemblyType(string assemblyName)
        {
            return assemblyName switch
            {
                "Assembly-CSharp" => PredefinedAssemblyType.AssemblyCSharp,
                "Assembly-CSharp-Editor" => PredefinedAssemblyType.AssemblyCSharpEditor,
                "Assembly-CSharp-Editor-firstpass" => PredefinedAssemblyType.AssemblyCSharpEditorFirstPass,
                "Assembly-CSharp-firstpass" => PredefinedAssemblyType.AssemblyCSharpFirstPass,
                _ => null
            };
        }

        public static List<Type> GetRuntimeTypesWithInterface(Type interfaceType)
        {
            var targetTypes = new List<Type>();
            var assemblyTypes = GetRuntimeTypesMap();
            assemblyTypes.TryGetValue(PredefinedAssemblyType.AssemblyCSharp, out var assemblyCSharpTypes);
            AddTypesFromAssembly(assemblyCSharpTypes, interfaceType, targetTypes);
            assemblyTypes.TryGetValue(PredefinedAssemblyType.AssemblyCSharpFirstPass,
                out var assemblyCSharpFirstPassTypes);
            AddTypesFromAssembly(assemblyCSharpFirstPassTypes, interfaceType, targetTypes);
            return targetTypes;
        }

        static void AddTypesFromAssembly(Type[] assemblyTypes, Type interfaceType, ICollection<Type> results)
        {
            if (assemblyTypes == null)
            {
                return;
            }

            for (var i = 0; i < assemblyTypes.Length; ++i)
            {
                var type = assemblyTypes[i];
                if (type != interfaceType && interfaceType.IsAssignableFrom(type))
                {
                    results.Add(type);
                }
            }
        }

        static Dictionary<PredefinedAssemblyType, Type[]> GetRuntimeTypesMap()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var assemblyTypes = new Dictionary<PredefinedAssemblyType, Type[]>();
            for (var i = 0; i < assemblies.Length; ++i)
            {
                var assemblyType = GetAssemblyType(assemblies[i].GetName().Name);
                if (assemblyType != null)
                {
                    assemblyTypes.Add((PredefinedAssemblyType)assemblyType, assemblies[i].GetTypes());
                }
            }

            return assemblyTypes;
        }
    }
}
