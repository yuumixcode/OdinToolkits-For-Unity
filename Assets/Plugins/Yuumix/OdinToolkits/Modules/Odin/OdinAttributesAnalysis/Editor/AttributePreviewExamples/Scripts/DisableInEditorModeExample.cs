using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class DisableInEditorModeExample : ExampleScriptableObject
    {
        [Title("Disabled in edit mode")]
        [DisableInEditorMode]
        public GameObject disableInEditorMode1;

        [DisableInEditorMode]
        public Material disableInEditorMode2;
    }
}
