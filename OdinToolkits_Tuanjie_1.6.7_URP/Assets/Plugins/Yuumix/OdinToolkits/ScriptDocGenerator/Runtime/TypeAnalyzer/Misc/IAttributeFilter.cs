using System;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.ScriptDocGenerator
{
    [Summary("特性过滤器接口，用于过滤掉不需要的特性")]
    public interface IAttributeFilter
    {
        [Summary("排除的特性类型")] Type[] ExcludeTypes { get; }

        [Summary("判断传入的特性类型是否应该被过滤掉")]
        bool ShouldFilterOut(Type type);
    }
}
