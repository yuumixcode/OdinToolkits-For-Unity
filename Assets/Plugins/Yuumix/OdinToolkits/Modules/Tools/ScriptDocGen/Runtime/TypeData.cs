using Sirenix.OdinInspector;
using Sirenix.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using Yuumix.OdinToolkits.Modules.CustomExtensions.Enums;
using Yuumix.OdinToolkits.Modules.Utilities.Runtime;

namespace Yuumix.OdinToolkits.Modules.Tools.ScriptDocGen.Runtime
{
    [Serializable]
    public class TypeData
    {
        // 类型种类
        [ShowInInspector] public TypeCategory TypeCategory { get; protected set; }

        /// <summary>
        /// 类型声明
        /// </summary>
        public string typeDeclaration;

        // 三个 bool 属性
        [ShowInInspector] public bool IsGeneric { get; protected set; }
        [ShowInInspector] public bool IsNested { get; protected set; }
        [ShowInInspector] public bool IsStatic { get; protected set; }

        // 基本属性
        [ShowInInspector] public string TypeName { get; protected set; }
        [ShowInInspector] public string NamespaceName { get; protected set; }
        [ShowInInspector] public string AssemblyName { get; protected set; }

        // 文档注释
        [ShowInInspector] public string ChineseComment { get; protected set; }
        [ShowInInspector] public string EnglishComment { get; protected set; }

        /// <summary>
        /// 继承链
        /// </summary>
        public List<string> inheritanceChain;

        /// <summary>
        /// 接口列表
        /// </summary>
        public List<string> interfaceList;

        public static TypeData FromType(Type type)
        {
            var data = new TypeData
            {
                TypeCategory = type.GetTypeCategory(),
                typeDeclaration = type.GetTypeDeclaration(),
                IsGeneric = type.IsGenericType,
                IsNested = type.IsNested,
                IsStatic = type.IsStatic()
            };
            return data;
        }
    }
}
