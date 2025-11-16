using System;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.ScriptDocGenerator
{
    /// <summary>
    /// 事件数据接口，继承自 IDerivedMemberData
    /// </summary>
    [Summary("事件数据接口，继承自 IDerivedMemberData")]
    public interface IEventData : IDerivedMemberData
    {
        /// <summary>
        /// 事件类型
        /// </summary>
        [Summary("事件类型")]
        Type EventType { get; }

        /// <summary>
        /// 事件类型名称
        /// </summary>
        [Summary("事件类型名称")]
        string EventTypeName { get; }

        /// <summary>
        /// 事件类型的完整名称，包括命名空间
        /// </summary>
        [Summary("事件类型的完整名称，包括命名空间")]
        string EventTypeFullName { get; }
    }
}
