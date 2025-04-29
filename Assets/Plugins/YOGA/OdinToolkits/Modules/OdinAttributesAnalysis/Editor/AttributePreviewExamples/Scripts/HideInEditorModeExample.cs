using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    [TypeInfoBox("编辑器模式下隐藏字段")]
    public class HideInEditorModeExample : ExampleScriptableObject
    {
        [Title("Hide in edit mode")] [HideInEditorMode]
        public GameObject hideInEditorMode1;

        [HideInEditorMode] public Material hideInEditorMode2;
    }
}