using System;
using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.Core.CompositeAttributes
{
    /// <summary>
    /// 作用于 Property 的特性，显示属性，同时关闭灰度显示
    /// </summary>
    [IncludeMyAttributes]
    [ShowInInspector]
    [EnableGUI]
    [AttributeUsage(AttributeTargets.Property)]
    public class ShowProperty : Attribute { }
}
