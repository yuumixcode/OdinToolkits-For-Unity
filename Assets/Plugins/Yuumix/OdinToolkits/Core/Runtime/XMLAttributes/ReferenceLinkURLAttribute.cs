using System;

namespace Yuumix.OdinToolkits.Core
{
    /// <summary>
    /// 参考链接，URL 链接
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Struct, AllowMultiple = true,
        Inherited = false)]
    public class ReferenceLinkURLAttribute : Attribute
    {
        public readonly string WebLink;

        public ReferenceLinkURLAttribute(string webLink) => WebLink = webLink;
    }
}
