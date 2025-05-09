using System;
using Sirenix.OdinInspector;

namespace YOGA.Modules.OdinToolkits.Schwapo.Editor.Examples
{
    [ResolvedParameterExample]
    public class OnStateUpdateExamples_Action
    {
        [FoldoutGroup("Attribute Expression Example")]
        [InfoBox("@\"Last Update: \" + lastUpdate")]
        [OnStateUpdate("@lastUpdate = DateTime.Now")]
        public string AttributeExpressionExample;

        private DateTime lastUpdate;

        [FoldoutGroup("Method Name Example")]
        [InfoBox("@\"Last Update: \" + lastUpdate")]
        [OnStateUpdate("OnStateUpdate")]
        public string MethodNameExample;

        private void OnStateUpdate()
        {
            lastUpdate = DateTime.Now;
        }
    }
    // End
}