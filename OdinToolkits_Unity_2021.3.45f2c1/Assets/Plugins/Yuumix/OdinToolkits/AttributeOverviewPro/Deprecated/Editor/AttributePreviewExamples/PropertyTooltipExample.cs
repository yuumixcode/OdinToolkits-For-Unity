using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    [AttributeOverviewProExample]
    public class PropertyTooltipExample : ExampleSO
    {
        [Button]
        [PropertyTooltip("Button Tooltip")]
        void ButtonWithTooltip()
        {
            // ...
        }

        #region Serialized Fields

        [PropertyTooltip("This is tooltip on an int property.")]
        public int MyInt;

        [InfoBox("Use $ to refer to a member string.")]
        [PropertyTooltip("$tooltip")]
        public string tooltip = "Dynamic tooltip.";

        #endregion
    }
}
