using System;
using System.Collections.Generic;
using System.Reflection;

namespace Yuumix.Universal
{
    [BilingualComment("预定义的程序集类型枚举", "Predefined assembly type enumeration")]
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

    [BilingualComment("预定义程序集工具类", "Predefined assembly utility class")]
    public static class PredefinedAssemblyUtility
    {
        [BilingualComment("根据程序集名称获取程序集类型", "Get the assembly type based on the assembly name")]
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

        [BilingualComment("获取运行时实现指定接口的类型列表",
            "Get a list of types that implement a specified interface at runtime")]
        public static List<Type> GetRuntimeTypesWithInterface(Type interfaceType)
        {
            var targetTypes = new List<Type>();
            Dictionary<PredefinedAssemblyType, Type[]> assemblyTypes = GetRuntimeTypesMap();
            assemblyTypes.TryGetValue(PredefinedAssemblyType.AssemblyCSharp, out Type[] assemblyCSharpTypes);
            AddTypesFromAssembly(assemblyCSharpTypes, interfaceType, targetTypes);
            assemblyTypes.TryGetValue(PredefinedAssemblyType.AssemblyCSharpFirstPass,
                out Type[] assemblyCSharpFirstPassTypes);
            AddTypesFromAssembly(assemblyCSharpFirstPassTypes, interfaceType, targetTypes);
            return targetTypes;
        }

        [BilingualComment("从指定程序集中添加实现指定接口的类型到结果集合",
            "Add types that implement a specified interface from a specified assembly to the result collection")]
        static void AddTypesFromAssembly(Type[] assemblyTypes, Type interfaceType, ICollection<Type> results)
        {
            if (assemblyTypes == null)
            {
                return;
            }

            for (var i = 0; i < assemblyTypes.Length; ++i)
            {
                Type type = assemblyTypes[i];
                if (type != interfaceType && interfaceType.IsAssignableFrom(type))
                {
                    results.Add(type);
                }
            }
        }

        [BilingualComment("获取运行时类型映射", "Get the runtime type mapping")]
        static Dictionary<PredefinedAssemblyType, Type[]> GetRuntimeTypesMap()
        {
            Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var assemblyTypes = new Dictionary<PredefinedAssemblyType, Type[]>();
            for (var i = 0; i < assemblies.Length; ++i)
            {
                PredefinedAssemblyType? assemblyType = GetAssemblyType(assemblies[i].GetName().Name);
                if (assemblyType != null)
                {
                    assemblyTypes.Add((PredefinedAssemblyType)assemblyType, assemblies[i].GetTypes());
                }
            }

            return assemblyTypes;
        }
    }
}
