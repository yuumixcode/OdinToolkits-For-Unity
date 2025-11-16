using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;

namespace Yuumix.OdinToolkits.Module.Editor
{
    /// <summary>
    /// 剔除 OdinToolkits 相关的 Assembly
    /// </summary>
    public class OdinToolkitsAssemblyFilter : IAssemblyFilter
    {
        public bool ShouldFilterOut(Assembly assembly) => assembly.FullName.StartsWith("Yuumix.OdinToolkits");
    }
}
