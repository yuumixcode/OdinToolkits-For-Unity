using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.Community.Schwapo.Editor
{
    [ResolvedParameterExample]
    public class OnInspectorInitExamples_Action
    {
        [FoldoutGroup("Method Name Example")]
        [OnInspectorInit("OnInspectorInit")]
        public string MethodNameExample;

        void OnInspectorInit()
        {
            // Initialized, do something
            // [...]
        }
    }
    // End
}
