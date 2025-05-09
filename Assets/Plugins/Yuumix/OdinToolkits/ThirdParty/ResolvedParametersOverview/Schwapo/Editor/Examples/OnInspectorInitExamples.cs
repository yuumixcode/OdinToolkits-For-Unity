using Sirenix.OdinInspector;

namespace YOGA.Modules.OdinToolkits.Schwapo.Editor.Examples
{
    [ResolvedParameterExample]
    public class OnInspectorInitExamples_Action
    {
        [FoldoutGroup("Method Name Example")] [OnInspectorInit("OnInspectorInit")]
        public string MethodNameExample;

        private void OnInspectorInit()
        {
            // Initialized, do something
            // [...]
        }
    }
    // End
}