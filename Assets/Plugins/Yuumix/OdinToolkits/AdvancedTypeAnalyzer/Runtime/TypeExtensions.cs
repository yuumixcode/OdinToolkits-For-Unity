using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Yuumix.OdinToolkits.Modules;

namespace Yuumix.OdinToolkits.AdvancedTypeAnalyzer
{
    /// <summary>
    /// TypeAnalyzer 中使用的 Type 静态扩展方法
    /// </summary>
    [Obsolete]
    public static class TypeExtensions
    {
        #region 创建分析数据

        /// <summary>
        /// 获取所有公共的，非静态的，构造函数的解析数据
        /// </summary>
        public static ConstructorAnalysisData[] CreateAllPublicConstructorAnalysisDataArray(this Type type)
        {
            var constructors = type.GetConstructors();
            var factory = new ConstructorAnalysisDataFactory();
            var dataList = constructors
                .Select(x => factory.CreateFromConstructorInfo(x, type))
                .ToList();
            dataList.Sort(new ConstructorAnalysisDataComparer());
            return dataList.ToArray();
        }

        public static MethodAnalysisData[] CreateRuntimeMethodAnalysisDataArray(this Type type)
        {
            var methods = type.GetRuntimeMethods();
            var factory = new MethodAnalysisDataFactory();
            // 剔除掉特殊方法，例如 Property 的 get 和 set， Event 的 add 和 remove
            methods = methods.Where(x =>
                x != null && !x.Name.Contains("add_") && !x.Name.Contains("remove_") &&
                !x.Name.Contains("get_") && !x.Name.Contains("set_"));
            var dataList =
                methods.Select(x => factory.CreateFromMethodInfo(x, type))
                    .ToList();
            dataList.Sort(new MethodAnalysisDataComparer());
            return dataList.ToArray();
        }

        public static EventAnalysisData[] CreateRuntimeEventAnalysisDataArray(this Type type)
        {
            var events = type.GetRuntimeEvents();
            var factory = new EventAnalysisDataFactory();
            var dataList = events
                .Select(x => factory.CreateFromEventInfo(x, type))
                .ToList();
            dataList.Sort(new EventAnalysisDataComparer());
            return dataList.ToArray();
        }

        public static PropertyAnalysisData[] CreateRuntimePropertyAnalysisDataArray(this Type type)
        {
            var properties = type.GetRuntimeProperties();
            var factory = new PropertyAnalysisDataFactory();
            var dataList = properties
                .Select(x => factory.CreateFromPropertyInfo(x, type))
                .ToList();
            dataList.Sort(new PropertyAnalysisDataComparer());
            return dataList.ToArray();
        }

        public static FieldAnalysisData[] CreateUserDefinedFieldAnalysisDataArray(this Type type)
        {
            IEnumerable<FieldInfo> fields = type.GetUserDefinedFields();
            var factory = new FieldAnalysisDataFactory();
            var dataList = fields
                .Select(x => factory.CreateFromFieldInfo(x, type))
                .ToList();
            dataList.Sort(new FieldAnalysisDataComparer());
            return dataList.ToArray();
        }

        #endregion
    }
}
