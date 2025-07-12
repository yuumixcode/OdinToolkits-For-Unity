using System;
using System.Collections.Generic;

namespace Yuumix.OdinToolkits.LowLevel
{
    /// <summary>
    /// 预定义的程序集类型枚举
    /// </summary>
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

    /// <summary>
    /// 预定义程序集工具类
    /// </summary>
    public static class PredefinedAssemblyUtil
    {
        /// <summary>
        /// 根据程序集名称获取程序集类型
        /// </summary>
        /// <param name="assemblyName">程序集名称</param>
        /// <returns>程序集类型或null</returns>
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

        /// <summary>
        /// 获取运行时实现指定接口的类型列表
        /// </summary>
        /// <param name="interfaceType">指定的接口类型</param>
        /// <returns>实现指定接口的类型列表</returns>
        public static List<Type> GetRuntimeTypesWithInterface(Type interfaceType)
        {
            var targetTypes = new List<Type>();
            var assemblyTypes = GetRuntimeTypesMap();
            assemblyTypes.TryGetValue(PredefinedAssemblyType.AssemblyCSharp, out var assemblyCSharpTypes);
            AddTypesFromAssembly(assemblyCSharpTypes, interfaceType, targetTypes);
            assemblyTypes.TryGetValue(PredefinedAssemblyType.AssemblyCSharpFirstPass, out var assemblyCSharpFirstPassTypes);
            AddTypesFromAssembly(assemblyCSharpFirstPassTypes, interfaceType, targetTypes);
            return targetTypes;
        }

        /// <summary>
        /// 从指定程序集中添加实现指定接口的类型到结果集合
        /// </summary>
        /// <param name="assemblyTypes">程序集中的类型数组</param>
        /// <param name="interfaceType">指定的接口类型</param>
        /// <param name="results">用于存储结果的集合</param>
        static void AddTypesFromAssembly(Type[] assemblyTypes, Type interfaceType, ICollection<Type> results)
        {
            if (assemblyTypes == null)
            {
                return;
            }

            for (var i = 0; i < assemblyTypes.Length; i++)
            {
                var type = assemblyTypes[i];
                if (type != interfaceType && interfaceType.IsAssignableFrom(type))
                {
                    results.Add(type);
                }
            }
        }

        /// <summary>
        /// 获取运行时类型映射
        /// </summary>
        /// <returns>包含预定义程序集类型及其对应类型的字典</returns>
        static Dictionary<PredefinedAssemblyType, Type[]> GetRuntimeTypesMap()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            var assemblyTypes = new Dictionary<PredefinedAssemblyType, Type[]>();
            for (var i = 0; i < assemblies.Length; i++)
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
