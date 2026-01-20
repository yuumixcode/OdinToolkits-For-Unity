using System.Reflection;

namespace Yuumix.OdinToolkits.Module.Editor
{
    /// <summary>
    /// 剔除 OdinToolkits 相关的 Assembly
    /// </summary>
    public class OdinToolkitsAssemblyFilter : IAssemblyFilter
    {
        #region IAssemblyFilter Members

        public bool ShouldFilterOut(Assembly assembly) => assembly.FullName.StartsWith("Yuumix.OdinToolkits");

        #endregion
    }
}
