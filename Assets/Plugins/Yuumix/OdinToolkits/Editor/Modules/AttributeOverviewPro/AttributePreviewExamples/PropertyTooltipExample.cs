using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.Editor
{
    [OdinToolkitsAttributeExample]
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
