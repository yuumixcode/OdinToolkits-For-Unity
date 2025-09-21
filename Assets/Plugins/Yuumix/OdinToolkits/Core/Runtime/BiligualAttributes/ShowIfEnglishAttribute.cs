using System;
using System.Diagnostics;
using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.Core
{
    [IncludeMyAttributes]
    [ShowIf("@" + nameof(InspectorBilingualismConfigSO) + "." +
            nameof(InspectorBilingualismConfigSO.IsEnglish),
        false)]
    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public class ShowIfEnglishAttribute : Attribute { }
}
