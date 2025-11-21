using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [AttributeOverviewProExample]
    public class ToggleGroupExample : ExampleSO
    {
        [ToggleGroup(nameof(toggle1), "toggleMemberName 参数")]
        public bool toggle1;

        [ToggleGroup(nameof(toggle1))]
        public int toggleGroup1;

        [ToggleGroup(nameof(toggle1))]
        public int toggleGroup2;

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

        [ToggleGroup(nameof(toggle4))]
        public int toggleGroup5;
    }
}
