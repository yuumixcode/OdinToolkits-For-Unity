using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Sirenix.OdinInspector;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class PropertyTooltipExample : ExampleScriptableObject
    {
        [PropertyTooltip("This is tooltip on an int property.")]
        public int MyInt;

        [InfoBox("Use $ to refer to a member string.")] [PropertyTooltip("$tooltip")]
        public string tooltip = "Dynamic tooltip.";

        [Button]
        [PropertyTooltip("Button Tooltip")]
        private void ButtonWithTooltip()
        {
            // ...
        }
    }
}