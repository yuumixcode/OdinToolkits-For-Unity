using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Sirenix.OdinInspector;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class ToggleGroupExample : ExampleScriptableObject
    {
        [ToggleGroup(nameof(toggle1), "toggleMemberName 参数")]
        public bool toggle1;

        [ToggleGroup(nameof(toggle1))] public int toggleGroup1;

        [ToggleGroup(nameof(toggle1))] public int toggleGroup2;

        [FoldoutGroup("order", GroupName = "order 参数")]
        [ToggleGroup("order/" + nameof(toggle2), groupTitle: "order 参数", order: 10)]
        public bool toggle2;

        [ToggleGroup("order/" + nameof(toggle2))]
        public int toggleGroup3;

        [ToggleGroup("order/" + nameof(toggle3), groupTitle: "order 参数", order: 1)]
        public bool toggle3;

        [ToggleGroup("order/" + nameof(toggle3))]
        public int toggleGroup4;

        [ToggleGroup(nameof(toggle4), "CollapseOthersOnExpand 参数", CollapseOthersOnExpand = true)]
        public bool toggle4;

        [ToggleGroup(nameof(toggle4))] public int toggleGroup5;
    }
}