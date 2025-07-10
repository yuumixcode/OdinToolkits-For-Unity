using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Yuumix.OdinToolkits.Common;
using Yuumix.OdinToolkits.Modules.CustomExtensions;

namespace Yuumix.OdinToolkits
{
    [Serializable]
    public class TypeData
    {
        // 类型种类
        [ShowInInspector] public TypeCategory TypeCategory { get; private set; }

        /// <summary>
        /// 类型声明
        /// </summary>
        [ShowInInspector]
        [DisplayAsString(false)]
        public string TypeDeclaration { get; private set; }

        // 三个 bool 属性
        [ShowInInspector] public bool IsGeneric { get; private set; }
        [ShowInInspector] public bool IsNested { get; private set; }
        [ShowInInspector] public bool IsStatic { get; private set; }
        [ShowInInspector] public bool IsSealed { get; private set; }
        [ShowInInspector] public bool IsAbstract { get; private set; }
        [ShowInInspector] public bool IsObsolete { get; private set; }

        // 基本属性
        [ShowInInspector] public string TypeName { get; private set; }
        [ShowInInspector] public string NamespaceName { get; private set; }
        [ShowInInspector] public string AssemblyName { get; private set; }

        // 文档注释
        [ShowInInspector]
        [DisplayAsString(false)]
        public string ChineseComment { get; private set; }

        [ShowInInspector]
        [DisplayAsString(false)]
        public string EnglishComment { get; private set; }

        [ShowInInspector]
        [DisplayAsString(false)]
        public string[] SeeAlsoLinks { get; private set; }

        /// <summary>
        /// 继承链
        /// </summary>
        [ShowInInspector]
        public string[] InheritanceChain { get; private set; }

        /// <summary>
        /// 接口列表
        /// </summary>
        [ShowInInspector]
        public string[] InterfaceList { get; private set; }

        public static TypeData FromType(Type type)
        {
            var data = new TypeData
            {
                TypeCategory = type.GetTypeCategory(),
                TypeDeclaration = type.GetTypeDeclaration(),
                IsGeneric = type.IsGenericType,
                IsNested = type.IsNested,
                IsStatic = type.IsStatic(),
                IsSealed = type.IsSealed,
                IsAbstract = type.IsAbstract,
                IsObsolete = type.IsDefined(typeof(ObsoleteAttribute)),
                TypeName = type.GetReadableTypeName(),
                NamespaceName = type.Namespace,
                AssemblyName = type.Assembly.GetName().Name,
                ChineseComment = GetComment(type),
                EnglishComment = GetComment(type, true),
                SeeAlsoLinks = GetSeeAlsoLink(type),
                InheritanceChain = GetInheritanceChain(type),
                InterfaceList = GetInterfacesList(type),
                Constructors = GetConstructorsString(type),
                CurrentFields = GetFieldsString(type),
                InheritedFields = GetFieldsString(type, false),
                CurrentMethods = GetMethodsString(type),
                OperatorMethods = GetOperatorMethodsString(type),
                InheritedMethods = GetMethodsString(type, false),
                CurrentProperties = GetPropertiesString(type),
                InheritedProperties = GetPropertiesString(type, false),
                CurrentEvents = GetEventsString(type),
                InheritedEvents = GetEventsString(type, false)
            };
            return data;
        }

        public static string GetComment(Type type, bool returnEnglishComment = false)
        {
            IEnumerable<Attribute> attributes = type.GetCustomAttributes();
            if (attributes.First(x => typeof(IMultiLanguageComment).IsAssignableFrom(x.GetType())) is not
                IMultiLanguageComment comment)
            {
                return null;
            }

            return returnEnglishComment ? comment.GetEnglishComment() : comment.GetChineseComment();
        }

        public static string[] GetSeeAlsoLink(Type type)
        {
            var links = type.GetAttributes<SeeAlsoLinkAttribute>();
            return links.Select(x => x.Link).ToArray();
        }

        public static string[] GetInheritanceChain(Type type)
        {
            return type.GetBaseTypes()
                .Where(t => !t.IsInterface)
                .Select(baseType => baseType.GetReadableTypeName(true)).ToArray();
        }

        public static string[] GetInterfacesList(Type type)
        {
            return type.GetInterfaces().Select(i => i.GetReadableTypeName(true)).ToArray();
        }

        /// <summary>
        /// 只获取公共实例构造函数
        /// </summary>
        /// <returns>构造函数声明的字符串列表</returns>
        [MultiLanguageComment("只获取公共实例构造函数",
            "Only get public and instance constructors,exclude static or no-public")]
        public static ConstructorData[] GetConstructorsString(Type type)
        {
            var constructors = type.GetConstructors();
            var dataList = constructors.Select(x => ConstructorData.FromConstructorInfo(x, type))
                .ToList();
            dataList.Sort(new MemberComparer());
            return dataList.ToArray();
        }

        [MultiLanguageComment("获取字段的声明字符串列表",
            "Get fields' declaration string list")]
        public static FieldData[] GetFieldsString(Type type, bool noNeedFromInherit = true)
        {
            var fields = type.GetUserDefinedFields().ToArray();
            var events = type.GetRuntimeEvents().ToArray();
            var eventNames = events.Select(e => e.Name).ToList();

            // 过滤掉已经定义为事件的字段
            fields = fields.Where(f => !eventNames.Contains(f.Name)).ToArray();

            var enumerable = fields.Select(x => FieldData.FromFieldInfo(x, type));
            List<FieldData> fieldDataList;
            if (noNeedFromInherit)
            {
                fieldDataList = enumerable.Where(x => x.NoFromInherit)
                    .ToList();
                fieldDataList.Sort(new MemberComparer());
                return fieldDataList.ToArray();
            }

            fieldDataList = enumerable.Where(x => !x.NoFromInherit)
                .ToList();
            fieldDataList.Sort(new MemberComparer());
            return fieldDataList.ToArray();
        }

        [MultiLanguageComment("获取方法的声明字符串列表",
            "Get methods' declaration string list")]
        public static MethodData[] GetMethodsString(Type type, bool noNeedFromInherit = true)
        {
            var methods = type.GetRuntimeMethods().Where(x => !x.IsSpecialName);
            var enumerable = methods.Select(x => MethodData.FromMethodInfo(x, type));
            List<MethodData> methodDataList;
            if (noNeedFromInherit)
            {
                methodDataList = enumerable.Where(x => x.NoFromInherit)
                    .ToList();
                methodDataList.Sort(new MemberComparer());
                return methodDataList.ToArray();
            }

            methodDataList = enumerable.Where(x => !x.NoFromInherit)
                .ToList();
            methodDataList.Sort(new MemberComparer());
            return methodDataList.ToArray();
        }

        [MultiLanguageComment("获取属性的声明字符串列表",
            "Get properties' declaration string list")]
        public static PropertyData[] GetPropertiesString(Type type, bool noNeedFromInherit = true)
        {
            var properties = type.GetRuntimeProperties().ToArray();
            var enumerable = properties.Select(x => PropertyData.FromPropertyInfo(x, type));
            List<PropertyData> propertyDataList;
            if (noNeedFromInherit)
            {
                propertyDataList = enumerable.Where(x => x.NoFromInherit)
                    .ToList();
                propertyDataList.Sort(new MemberComparer());
                return propertyDataList.ToArray();
            }

            propertyDataList = enumerable.Where(x => !x.NoFromInherit)
                .ToList();
            propertyDataList.Sort(new MemberComparer());
            return propertyDataList.ToArray();
        }

        [MultiLanguageComment("获取事件的声明字符串列表",
            "Get events' declaration string list")]
        public static EventData[] GetEventsString(Type type, bool noNeedFromInherit = true)
        {
            var events = type.GetRuntimeEvents().ToArray();
            var enumerable = events.Select(x => EventData.FromEventInfo(x, type));
            List<EventData> eventDataList;
            if (noNeedFromInherit)
            {
                eventDataList = enumerable.Where(x => x.NoFromInherit)
                    .ToList();
                eventDataList.Sort(new MemberComparer());
                return eventDataList.ToArray();
            }

            eventDataList = enumerable.Where(x => !x.NoFromInherit)
                .ToList();
            eventDataList.Sort(new MemberComparer());
            return eventDataList.ToArray();
        }

        public static MethodData[] GetOperatorMethodsString(Type type)
        {
            return type.GetRuntimeMethods().Where(x => x.IsSpecialName && x.Name.StartsWith("op"))
                .Select(x => MethodData.FromMethodInfo(x, type))
                .ToArray();
        }

        #region 成员部分

        [ShowInInspector] public ConstructorData[] Constructors { get; private set; }
        [ShowInInspector] public FieldData[] CurrentFields { get; private set; }
        [ShowInInspector] public FieldData[] InheritedFields { get; private set; }
        [ShowInInspector] public MethodData[] CurrentMethods { get; private set; }
        [ShowInInspector] public MethodData[] OperatorMethods { get; private set; }
        [ShowInInspector] public MethodData[] InheritedMethods { get; private set; }
        [ShowInInspector] public PropertyData[] CurrentProperties { get; private set; }
        [ShowInInspector] public PropertyData[] InheritedProperties { get; private set; }
        [ShowInInspector] public EventData[] CurrentEvents { get; private set; }
        [ShowInInspector] public EventData[] InheritedEvents { get; private set; }

        #endregion
    }
}
