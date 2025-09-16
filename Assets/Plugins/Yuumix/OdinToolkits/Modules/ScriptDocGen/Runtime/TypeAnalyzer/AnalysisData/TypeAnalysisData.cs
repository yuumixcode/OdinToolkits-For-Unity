using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Sirenix.Utilities;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules
{
    [Serializable]
    public class TypeAnalysisData
    {
        /// <summary>
        /// 类型种类，可以是类，结构，接口，枚举，委托
        /// </summary>
        public TypeCategory typeCategory;

        /// <summary>
        /// 程序集
        /// </summary>
        public string assemblyName;

        /// <summary>
        /// 命名空间
        /// </summary>
        public string namespaceName;

        /// <summary>
        /// 类型名
        /// </summary>
        public string typeName;

        /// <summary>
        /// 类型声明，包含类型名，继承关系，泛型参数，特性名
        /// </summary>
        [TextArea]
        public string typeDeclaration;

        /// <summary>
        /// 中文描述
        /// </summary>
        [TextArea]
        public string chineseDescription;

        /// <summary>
        /// 英文描述
        /// </summary>
        [TextArea]
        public string englishDescription;

        public bool isGeneric;
        public bool isNested;

        /// <summary>
        /// C# 判断为静态的底层依据是 Sealed (不能继承) 且 Abstract (不能实例化)
        /// </summary>
        public bool isStatic;

        public bool isSealed;

        /// <summary>
        /// 接口是 Abstract (需要继承)，静态也是 Abstract (不能实例化)
        /// /// </summary>
        public bool isAbstract;

        public bool isObsolete;

        /// <summary>
        /// 参考链接数组
        /// </summary>
        public string[] referenceWebLinkArray;

        /// <summary>
        /// 继承链
        /// </summary>
        public string[] inheritanceChain;

        /// <summary>
        /// 接口列表
        /// </summary>
        public string[] interfaceArray;

        /// <summary>
        /// 所有的公共构造函数分析数据数组
        /// </summary>
        public ConstructorAnalysisData[] constructorAnalysisDataArray;

        /// <summary>
        /// 所有的方法分析数据数组
        /// </summary>
        public MethodAnalysisData[] methodAnalysisDataArray;

        /// <summary>
        /// 所有的事件分析数据数组
        /// </summary>
        public EventAnalysisData[] eventAnalysisDataArray;

        /// <summary>
        /// 所有的属性分析数据数组
        /// </summary>
        public PropertyAnalysisData[] propertyAnalysisDataArray;

        /// <summary>
        /// 所有的字段分析数据数组
        /// </summary>
        public FieldAnalysisData[] fieldAnalysisDataArray;

        public static TypeAnalysisData CreateFromType(Type type) =>
            new TypeAnalysisData
            {
                typeCategory = type.GetTypeCategory(),
                assemblyName = type.Assembly.GetName().Name,
                namespaceName = type.Namespace,
                typeDeclaration = type.GetTypeDeclaration(),
                typeName = type.GetReadableTypeName(),
                chineseDescription = type.GetChineseDescription(),
                englishDescription = type.GetEnglishDescription(),
                isGeneric = type.IsGenericType,
                isNested = type.IsNested,
                isStatic = type.IsStatic(),
                isSealed = type.IsSealed,
                isAbstract = type.IsAbstract,
                isObsolete = type.IsDefined(typeof(ObsoleteAttribute)),
                referenceWebLinkArray = type.GetReferenceLinks(),
                inheritanceChain = type.GetInheritanceChain(),
                interfaceArray = type.GetInterfacesArray(),
                constructorAnalysisDataArray = type.CreateAllPublicConstructorAnalysisDataArray(),
                methodAnalysisDataArray = type.CreateRuntimeMethodAnalysisDataArray(),
                eventAnalysisDataArray = type.CreateRuntimeEventAnalysisDataArray(),
                propertyAnalysisDataArray = type.CreateRuntimePropertyAnalysisDataArray(),
                fieldAnalysisDataArray = type.CreateUserDefinedFieldAnalysisDataArray()
            };
    }
}
