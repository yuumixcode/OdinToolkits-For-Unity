using Sirenix.OdinInspector;
using System;
using System.Diagnostics;

namespace Yuumix.OdinToolkits.Common
{
    [IncludeMyAttributes]
    [ShowIf("@" + nameof(InspectorMultiLanguageManagerSO) + "." +
            nameof(InspectorMultiLanguageManagerSO.IsEnglish),
        false)]
    [DontApplyToListElements]
    [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
    [Conditional("UNITY_EDITOR")]
    public class ShowIfEnglishAttribute : Attribute { }
}
