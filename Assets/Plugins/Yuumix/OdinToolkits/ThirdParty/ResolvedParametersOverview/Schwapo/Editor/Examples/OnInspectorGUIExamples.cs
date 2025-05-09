using Sirenix.OdinInspector;
using UnityEngine;

namespace YOGA.Modules.OdinToolkits.Schwapo.Editor.Examples
{
    [ResolvedParameterExample]
    public class OnInspectorGUIExamples_Prepend
    {
        [FoldoutGroup("Method Name Example")] [OnInspectorGUI(Prepend = "PrependGUI")]
        public string MethodNameExample;

        private void PrependGUI()
        {
            GUILayout.Label("Injected Before");
        }
    }
    // End

    [ResolvedParameterExample]
    public class OnInspectorGUIExamples_Append
    {
        [FoldoutGroup("Method Name Example")] [OnInspectorGUI(Append = "AppendGUI")]
        public string MethodNameExample;

        private void AppendGUI()
        {
            GUILayout.Label("Injected After");
        }
    }
    // End
}