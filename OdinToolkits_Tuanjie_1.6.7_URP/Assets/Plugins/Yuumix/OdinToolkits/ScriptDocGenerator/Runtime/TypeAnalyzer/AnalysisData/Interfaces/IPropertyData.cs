using System;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.ScriptDocGenerator
{
    /// <summary>
    /// 属性数据接口，继承自 IDerivedMemberData，包含属性特有的数据信息和方法，派生类的通用数据信息和方法
    /// </summary>
    [Summary("属性数据接口，继承自 IDerivedMemberData，包含属性特有的数据信息和方法，派生类的通用数据信息和方法")]
    public interface IPropertyData : IDerivedMemberData
    {
        /// <summary>
        /// 自定义默认值，如果没有自定义默认值，则为 null
        /// </summary>
        [Summary("自定义默认值，如果没有自定义默认值，则为 null")]
        object DefaultValue { get; }

        /// <summary>
        /// 属性类型
        /// </summary>
        [Summary("属性类型")]
        Type PropertyType { get; }

        /// <summary>
        /// 属性类型名称
        /// </summary>
        [Summary("属性类型名称")]
        string PropertyTypeName { get; }

        /// <summary>
        /// 属性类型的完整名称，包括命名空间
        /// </summary>
        [Summary("属性类型的完整名称，包括命名空间")]
        string PropertyTypeFullName { get; }
    }
}
