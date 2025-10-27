using System;
using System.Linq;

namespace Yuumix.OdinToolkits.ScriptDocGenerator
{
    public interface IAttributeFilter
    {
        Type[] ExcludeTypes { get; }

        bool ShouldFilterOut(Type type);
    }

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

        public Type[] ExcludeTypes { get; }
        public bool ShouldFilterOut(Type type) => ExcludeTypes.Contains(type);

        #endregion
    }
}
