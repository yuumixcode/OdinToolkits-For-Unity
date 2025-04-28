using Sirenix.OdinInspector;
using UnityEngine;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
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