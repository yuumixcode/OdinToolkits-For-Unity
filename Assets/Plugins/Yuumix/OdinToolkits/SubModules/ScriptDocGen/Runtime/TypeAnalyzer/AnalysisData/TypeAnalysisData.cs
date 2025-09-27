using System;
using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.AdvancedTypeAnalyzer;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules
{
    [Serializable]
    public class TypeAnalysisData
    {
        /// <summary>
        /// 类型种类，可以是类，结构，接口，枚举，委托，记录器
        /// </summary>
        [Summary("类型种类，可以是类，结构，接口，枚举，委托，记录器")]
        public TypeCategory typeCategory;

        /// <summary>
        /// 程序集
        /// </summary>
        [Summary("程序集")]
        public string assemblyName;

        /// <summary>
        /// 命名空间
        /// </summary>
        [Summary("命名空间")]
        public string namespaceName;

        /// <summary>
        /// 类型名
        /// </summary>
        [Summary("类型名")]
        public string typeName;

        /// <summary>
        /// 类型声明，包含类型名，继承关系，泛型参数，特性名
        /// </summary>
        [Summary("类型声明，包含类型名，继承关系，泛型参数，特性名")]
        [TextArea]
        public string typeDeclaration;

        /// <summary>
        /// 中文描述
        /// </summary>
        [Summary("中文描述")]
        [HorizontalGroup("Desc")]
        [TextArea]
        public string chineseDescription;

        /// <summary>
        /// 英文描述
        /// </summary>
        [Summary("英文描述")]
        [HorizontalGroup("Desc")]
        [TextArea]
        public string englishDescription;

        /// <summary>
        /// 是否为泛型类
        /// </summary>
        [Summary("是否为泛型类")]
        public bool isGeneric;

        /// <summary>
        /// 是否为嵌套类
        /// </summary>
        [Summary("是否为嵌套类")]
        public bool isNested;

        /// <summary>
        /// C# 判断为静态的底层依据是 Sealed (不能继承) 且 Abstract (不能实例化)
        /// </summary>
        [Summary("C# 判断为静态的底层依据是 Sealed (不能继承) 且 Abstract (不能实例化)")]
        public bool isStatic;

        /// <summary>
        /// 是否为密封类
        /// </summary>
        [Summary("是否为密封类")]
        public bool isSealed;

        /// <summary>
        /// 接口是 Abstract (需要继承)，静态是 Abstract (不能实例化)
        /// ///
        /// </summary>
        [Summary("接口是 Abstract (需要继承)，静态是 Abstract (不能实例化) ///")]
        public bool isAbstract;

        /// <summary>
        /// 是否被 Obsolete 标记
        /// </summary>
        [Summary("是否被 Obsolete 标记")]
        public bool isObsolete;

        /// <summary>
        /// 参考链接数组
        /// </summary>
        [Summary("参考链接数组")]
        public string[] referenceWebLinkArray;

        /// <summary>
        /// 继承链
        /// </summary>
        [Summary("继承链")]
        public string[] inheritanceChain;

        /// <summary>
        /// 接口列表
        /// </summary>
        [Summary("接口列表")]
        public string[] interfaceArray;

        /// <summary>
        /// 所有的公共构造函数分析数据数组
        /// </summary>
        [Summary("所有的公共构造函数分析数据数组")]
        public ConstructorAnalysisData[] constructorAnalysisDataArray;

        /// <summary>
        /// 所有的方法分析数据数组
        /// </summary>
        [Summary("所有的方法分析数据数组")]
        public MethodAnalysisData[] methodAnalysisDataArray;

        /// <summary>
        /// 所有的事件分析数据数组
        /// </summary>
        [Summary("所有的事件分析数据数组")]
        public EventAnalysisData[] eventAnalysisDataArray;

        /// <summary>
        /// 所有的属性分析数据数组
        /// </summary>
        [Summary("所有的属性分析数据数组")]
        public PropertyAnalysisData[] propertyAnalysisDataArray;

        /// <summary>
        /// 所有的字段分析数据数组
        /// </summary>
        [Summary("所有的字段分析数据数组")]
        public FieldAnalysisData[] fieldAnalysisDataArray;
        
        /// <summary>
        /// 标记类型中存在的重载方法，加上 [Overload] 前缀
        /// </summary>
        public static MethodAnalysisData[] MarkOverloadMethod(MethodAnalysisData[] methodAnalysisDataArray)
        {
            var factory = new TypeAnalysisDataFactory();
            return factory.MarkOverloadMethod(methodAnalysisDataArray);
        }
    }
}