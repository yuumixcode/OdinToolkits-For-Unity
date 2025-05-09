using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class DisableInPlayModeExample : ExampleScriptableObject
    {
        [Title("Disabled in edit mode")] [DisableInPlayMode]
        public GameObject disableInPlayMode1;

        [DisableInPlayMode] public Material disableInPlayMode2;
    }
}