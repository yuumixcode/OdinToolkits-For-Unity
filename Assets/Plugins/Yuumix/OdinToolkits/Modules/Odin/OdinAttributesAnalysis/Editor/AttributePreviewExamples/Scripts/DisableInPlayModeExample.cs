using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class DisableInPlayModeExample : ExampleScriptableObject
    {
        [Title("Disabled in edit mode")]
        [DisableInPlayMode]
        public GameObject disableInPlayMode1;

        [DisableInPlayMode]
        public Material disableInPlayMode2;
    }
}
