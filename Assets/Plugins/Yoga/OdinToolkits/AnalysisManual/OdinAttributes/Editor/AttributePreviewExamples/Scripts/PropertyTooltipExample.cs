using Sirenix.OdinInspector;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    [IsChineseAttributeExample]
    public class PropertyTooltipExample : ExampleScriptableObject
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