using System;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.ScriptDocGenerator
{
    [Summary("字段数据接口，继承自 IDerivedMemberData")]
    public interface IFieldData : IDerivedMemberData
    {
        [Summary("是否为只读字段（readonly）")] public bool IsReadOnly { get; }

        [Summary("是否为常量字段（const）")] public bool IsConstant { get; }

        [Summary("是否为动态字段（dynamic）")] public bool IsDynamic { get; }

        [Summary("字段的默认值")] public object DefaultValue { get; }

        [Summary("字段的类型")] public Type FieldType { get; }

        [Summary("这个字段的类型的名称")] public string FieldTypeName { get; }

        [Summary("这个字段的类型的完整名称")] public string FieldTypeFullName { get; }
    }
}
