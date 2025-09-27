using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.Community.Schwapo.Editor
{
    [ResolvedParameterExample]
    public class CustomContextMenuExamples_Action
    {
        [FoldoutGroup("Attribute Expression Example")]
        [InfoBox("Right click the field label to execute the context menu item")]
        [CustomContextMenu("Log", "@Debug.Log($value)")]
        public string AttributeExpressionExample = "Peace";

        [FoldoutGroup("Method Name Example")]
        [InfoBox("Right click the field label to execute the context menu item")]
        [CustomContextMenu("Log", "Log")]
        public string MethodNameExample = "Love";

        void Log(string value)
        {
            Debug.Log(value);
        }
    }
    // End
}
