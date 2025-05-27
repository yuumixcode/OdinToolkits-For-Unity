using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.ThirdParty.ResolvedParametersOverview.Schwapo.Editor.Examples
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
