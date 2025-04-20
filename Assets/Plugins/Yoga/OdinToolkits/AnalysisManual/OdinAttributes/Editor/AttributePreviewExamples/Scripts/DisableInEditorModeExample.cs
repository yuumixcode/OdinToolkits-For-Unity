using Sirenix.OdinInspector;
using UnityEngine;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
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