using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [AttributeOverviewProExample]
    public class PropertyTooltipExample : ExampleSO
    {
        [PropertyTooltip("This is tooltip on an int property.")]
        public int MyInt;

        [InfoBox("Use $ to refer to a member string.")]
        [PropertyTooltip("$tooltip")]
        public string tooltip = "Dynamic tooltip.";

        [Button]
        [PropertyTooltip("Button Tooltip")]
        void ButtonWithTooltip()
        {
            // ...
        }
    }
}
