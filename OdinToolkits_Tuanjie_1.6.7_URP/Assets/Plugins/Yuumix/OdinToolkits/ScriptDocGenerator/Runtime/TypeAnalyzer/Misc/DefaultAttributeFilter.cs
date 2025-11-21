using System;
using System.Linq;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.ScriptDocGenerator
{
    [Summary("默认特性过滤器，构造函数中传入需要排除的 Attribute 类型")]
    public class DefaultAttributeFilter : IAttributeFilter
    {
        public DefaultAttributeFilter(Type[] excludeTypes)
        {
            if (excludeTypes != null)
            {
                ExcludeTypes = excludeTypes;
            }
        }

        #region IAttributeFilter Members

        [Summary("排除的特性类型")] public Type[] ExcludeTypes { get; }

        [Summary("判断传入的特性类型是否应该被过滤掉")]
        public bool ShouldFilterOut(Type type) => ExcludeTypes.Contains(type);

        #endregion
    }
}
