using System.Reflection;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.ScriptDocGenerator
{
    /// <summary>
    /// 类型解析数据接口，继承自 IDerivedMemberData 接口
    /// </summary>
    [Summary("类型解析数据接口，继承自 IDerivedMemberData 接口")]
    public interface ITypeData : IDerivedMemberData
    {
        /// <summary>
        /// Type 种类
        /// </summary>
        [Summary("Type 种类")]
        TypeCategory TypeCategory { get; }

        /// <summary>
        /// 类型所在的程序集
        /// </summary>
        [Summary("类型所在的程序集")]
        Assembly Assembly { get; }

        /// <summary>
        /// 类型所在的程序集名称
        /// </summary>
        [Summary("类型所在的程序集名称")]
        string AssemblyName { get; }

        /// <summary>
        /// 类型所在的命名空间
        /// </summary>
        [Summary("类型所在的命名空间")]
        string NamespaceName { get; }

        /// <summary>
        /// 是否为泛型类型
        /// </summary>
        [Summary("是否为泛型类型")]
        bool IsGenericType { get; }

        /// <summary>
        /// 是否为密封类型
        /// </summary>
        [Summary("是否为密封类型")]
        public bool IsSealed { get; }

        /// <summary>
        /// 是否为抽象类型
        /// </summary>
        [Summary("是否为抽象类型")]
        public bool IsAbstract { get; }

        /// <summary>
        /// 类型的引用链接数组
        /// </summary>
        [Summary("类型的引用链接数组")]
        public string[] ReferenceWebLinkArray { get; }

        /// <summary>
        /// 类型的继承链数组
        /// </summary>
        [Summary("类型的继承链数组")]
        public string[] InheritanceChain { get; }

        /// <summary>
        /// 类型的接口数组
        /// </summary>
        [Summary("类型的接口数组")]
        public string[] InterfaceArray { get; }

        /// <summary>
        /// 分析数据工厂实例对象
        /// </summary>
        [Summary("分析数据工厂实例对象")]
        IAnalysisDataFactory DataFactory { get; }

        /// <summary>
        /// 类型的构造函数解析数据数组，只包含公共构造函数，GetConstructors() 方法
        /// </summary>
        [Summary("类型的构造函数解析数据数组，只包含公共构造函数，GetConstructors() 方法")]
        IConstructorData[] RuntimeReflectedConstructorsData { get; }

        /// <summary>
        /// 类型的方法解析数据数组，GetRuntimeMethods() 方法
        /// </summary>
        [Summary("类型的方法解析数据数组，GetRuntimeMethods() 方法")]
        IMethodData[] RuntimeReflectedMethodsData { get; }

        /// <summary>
        /// 类型的事件解析数据数组，GetRuntimeEvents() 方法
        /// </summary>
        [Summary("类型的事件解析数据数组，GetRuntimeEvents() 方法")]
        IEventData[] RuntimeReflectedEventsData { get; }

        /// <summary>
        /// 类型的属性解析数据数组，GetRuntimeProperties() 方法
        /// </summary>
        [Summary("类型的属性解析数据数组，GetRuntimeProperties() 方法")]
        IPropertyData[] RuntimeReflectedPropertiesData { get; }

        /// <summary>
        /// 类型的字段解析数据数组，GetUserDefinedFields() 方法
        /// </summary>
        [Summary("类型的字段解析数据数组，GetUserDefinedFields() 方法")]
        IFieldData[] RuntimeReflectedFieldsData { get; }
    }
}
