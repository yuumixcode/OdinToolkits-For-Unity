using Sirenix.OdinInspector;
using UnityEngine;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    [IsChineseAttributeExample]
    public class DisableInPlayModeExample : ExampleScriptableObject
    {
        [Title("Disabled in edit mode")] [DisableInPlayMode]
        public GameObject disableInPlayMode1;

        [DisableInPlayMode] public Material disableInPlayMode2;
    }
}