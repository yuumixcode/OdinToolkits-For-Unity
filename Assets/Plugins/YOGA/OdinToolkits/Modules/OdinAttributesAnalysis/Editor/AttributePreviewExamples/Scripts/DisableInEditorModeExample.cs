using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class DisableInEditorModeExample : ExampleScriptableObject
    {
        [Title("Disabled in edit mode")] [DisableInEditorMode]
        public GameObject disableInEditorMode1;

        [DisableInEditorMode] public Material disableInEditorMode2;
    }
}