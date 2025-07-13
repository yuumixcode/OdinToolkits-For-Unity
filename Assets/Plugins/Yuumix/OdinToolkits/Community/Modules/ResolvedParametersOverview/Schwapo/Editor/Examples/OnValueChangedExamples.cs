using Sirenix.OdinInspector;
using UnityEngine;

namespace Community.Schwapo.Editor
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

        void OnValueChanged()
        {
            Debug.Log("On Value Changed");
        }
    }
    // End
}
