using Sirenix.OdinInspector;
using UnityEngine;

namespace YOGA.Modules.OdinToolkits.Schwapo.Editor.Examples
{
    [ResolvedParameterExample]
    public class OnValueChangedExamples_Action
    {
        [FoldoutGroup("Attribute Expression Example")]
        [InfoBox("Should log to the console whenever the value has changed")]
        [OnValueChanged("@Debug.Log(\"On Value Changed\")")]
        public string AttributeExpressionExample;

        [FoldoutGroup("Method Name Example")]
        [InfoBox("Should log to the console whenever the value has changed")]
        [OnValueChanged("OnValueChanged")]
        public string MethodNameExample;

        private void OnValueChanged()
        {
            Debug.Log("On Value Changed");
        }
    }
    // End
}