using System;
using System.Diagnostics;
using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.Core.Runtime
{
    [IncludeMyAttributes]
    [ShowIf("@" + nameof(BilingualSetting) + "." +
            nameof(BilingualSetting.IsChinese),
        false)]
    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public class ShowIfChineseAttribute : Attribute { }
}
