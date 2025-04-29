using Sirenix.OdinInspector;
using System;

namespace Plugins.YOGA.OdinToolkits.Modules.CustomExtension.Runtime.Attributes.Composite
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