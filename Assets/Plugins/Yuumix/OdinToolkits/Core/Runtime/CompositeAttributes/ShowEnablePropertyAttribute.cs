using Sirenix.OdinInspector;
using System;

namespace Yuumix.OdinToolkits.Core
{
    /// <summary>
    /// 作用于 Property 的特性，显示属性，同时关闭灰度显示
    /// </summary>
    [IncludeMyAttributes]
    [ShowInInspector]
    [EnableGUI]
    [AttributeUsage(AttributeTargets.Property)]
    public class ShowEnablePropertyAttribute : Attribute { }
}
