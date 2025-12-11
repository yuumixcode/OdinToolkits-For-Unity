using System;
using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.Core
{
    [Summary("显示属性在 Inspector 中，同时关闭灰度显示")]
    [IncludeMyAttributes]
    [ShowInInspector]
    [EnableGUI]
    [AttributeUsage(AttributeTargets.Property)]
    public class ShowEnablePropertyAttribute : Attribute { }
}
