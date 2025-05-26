using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
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
