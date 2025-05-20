using Sirenix.OdinInspector;
using System;

namespace Yuumix.OdinToolkits.Modules.Odin.Customs.Runtime.Attributes.Composite
{
    /// <summary>
    ///     [ShowInInspector] [EnableGUI] [ReadOnly] 组合而成。
    /// </summary>
    [IncludeMyAttributes]
    [ShowInInspector]
    [ReadOnly]
    [EnableGUI]
    public class ShowReadOnlyAttribute : Attribute
    {
    }
}