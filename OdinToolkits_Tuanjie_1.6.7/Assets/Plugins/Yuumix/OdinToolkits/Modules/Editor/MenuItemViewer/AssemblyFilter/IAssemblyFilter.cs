using System.Reflection;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Module.Editor
{
    [Summary("Assembly 过滤器接口，判断 Assembly 是否应该剔除")]
    public interface IAssemblyFilter
    {
        bool ShouldFilterOut(Assembly assembly);
    }
}
