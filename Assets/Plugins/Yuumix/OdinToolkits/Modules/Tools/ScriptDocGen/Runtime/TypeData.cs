using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System;
using System.Linq;
using System.Reflection;
using UnityEngine;
using Yuumix.OdinToolkits.Common.InspectorLocalization.Attributes;
using Yuumix.OdinToolkits.Modules.CustomExtensions.Enums;
using Yuumix.OdinToolkits.Modules.Utilities.Runtime;

namespace Yuumix.OdinToolkits.Modules.Tools.ScriptDocGen.Runtime
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

        #region 成员部分

        [ShowInInspector] public ConstructorData[] Constructors { get; private set; }
        [ShowInInspector] public FieldData[] CurrentFields { get; private set; }
        [ShowInInspector] public FieldData[] InheritedFields { get; private set; }

        #endregion

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
                InheritanceChain = GetInheritanceChain(type),
                InterfaceList = GetInterfacesList(type),
                Constructors = GetConstructorsString(type),
                CurrentFields = GetFieldsString(type),
                InheritedFields = GetFieldsString(type, false)
            };
            return data;
        }

        public static string GetComment(Type type, bool getEnglish = false)
        {
            var comment = type.GetAttribute<LocalizedCommentAttribute>();
            if (comment == null)
            {
                Debug.Log("Comment 为 null");
                return null;
            }

            return getEnglish ? comment.EnglishComment : comment.ChineseComment;
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
        [LocalizedComment("只获取公共实例构造函数",
            "Only get public and instance constructors,exclude static or no-public")]
        public static ConstructorData[] GetConstructorsString(Type type)
        {
            var constructors = type.GetConstructors();
            return constructors.Select(x => ConstructorData.FromConstructorInfo(x, type)).ToArray();
        }

        [LocalizedComment("获取字段的声明字符串列表",
            "Get fields' declaration string list")]
        public static FieldData[] GetFieldsString(Type type, bool noNeedFromInherit = true)
        {
            var fields = type.GetUserDefinedFields().ToArray();
            if (noNeedFromInherit)
            {
                return fields.Select(x => FieldData.FromFieldInfo(x, type))
                    .Where(x => x.NoFromInherit)
                    .ToArray();
            }

            return fields.Select(x => FieldData.FromFieldInfo(x, type))
                .Where(x => !x.NoFromInherit)
                .ToArray();
        }
    }
}
