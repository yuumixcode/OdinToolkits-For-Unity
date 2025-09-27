using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.Community.Schwapo.Editor
{
    [ResolvedParameterExample]
    public class PropertyTooltipExamples_Tooltip
    {
        public string AlternativeTooltip = "Peace, Love & Fenrir";

        [FoldoutGroup("Attribute Expression Example")]
        [InfoBox("Hover over the field label")]
        [PropertyTooltip("@UseAlternativeTooltip ? AlternativeTooltip : Tooltip")]
        public string AttributeExpressionExample;

        [FoldoutGroup("Field Name Example")]
        [InfoBox("Hover over the field label")]
        [PropertyTooltip("$Tooltip")]
        public string FieldNameExample;

        [FoldoutGroup("Method Name Example")]
        [InfoBox("Hover over the field label")]
        [PropertyTooltip("$GetTooltip")]
        public string MethodNameExample;

        [FoldoutGroup("Property Name Example")]
        [InfoBox("Hover over the field label")]
        [PropertyTooltip("$TooltipProperty")]
        public string PropertyNameExample;

        public string Tooltip = "Peace, Love & Ducks";
        public bool UseAlternativeTooltip;
        public string TooltipProperty => UseAlternativeTooltip ? AlternativeTooltip : Tooltip;

        string GetTooltip() => UseAlternativeTooltip ? AlternativeTooltip : Tooltip;
    }
    // End
}
