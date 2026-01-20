using System;

namespace Yuumix.OdinToolkits.Core
{
    /// <summary>
    /// 参考链接，URL 链接
    /// </summary>
    [Summary("参考链接，URL 链接")]
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Struct,
        AllowMultiple = true, Inherited = false)]
    public class ReferenceLinkURLAttribute : Attribute
    {
        public readonly string WebUrl;

        public ReferenceLinkURLAttribute(string webUrl) => WebUrl = webUrl;
    }
}
