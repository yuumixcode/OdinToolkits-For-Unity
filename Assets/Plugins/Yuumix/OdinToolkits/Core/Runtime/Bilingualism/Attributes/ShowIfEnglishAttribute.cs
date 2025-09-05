using System;
using System.Diagnostics;
using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.Core.Runtime
{
    [IncludeMyAttributes]
    [ShowIf("@" + nameof(BilingualSetting) + "." +
            nameof(BilingualSetting.IsEnglish),
        false)]
    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public class ShowIfEnglishAttribute : Attribute { }
}
