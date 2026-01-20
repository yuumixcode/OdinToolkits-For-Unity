using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.Community.Schwapo.Editor
{
    [ResolvedParameterExample]
    public class OnInspectorGUIExamples_Prepend
    {
        [FoldoutGroup("Method Name Example")]
        [OnInspectorGUI(Prepend = "PrependGUI")]
        public string MethodNameExample;

        void PrependGUI()
        {
            GUILayout.Label("Injected Before");
        }
    }
    // End

    [ResolvedParameterExample]
    public class OnInspectorGUIExamples_Append
    {
        [FoldoutGroup("Method Name Example")]
        [OnInspectorGUI(Append = "AppendGUI")]
        public string MethodNameExample;

        void AppendGUI()
        {
            GUILayout.Label("Injected After");
        }
    }
    // End
}
