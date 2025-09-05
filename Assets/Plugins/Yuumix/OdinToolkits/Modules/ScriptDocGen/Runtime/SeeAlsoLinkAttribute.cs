using System;

namespace Yuumix.OdinToolkits.Core.Runtime
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Struct, AllowMultiple = true,
        Inherited = false)]
    public class SeeAlsoLinkAttribute : Attribute
    {
        public readonly string Link;

        public SeeAlsoLinkAttribute(string link) => Link = link;
    }
}
