using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.Community.Schwapo.Editor
{
    [ResolvedParameterExample]
    public class SuffixLabelExamples_Label
    {
        public string AlternativeLabel = "Peace, Love & Fenrir";

        [FoldoutGroup("Attribute Expression Example")]
        [SuffixLabel("@UseAlternativeLabel ? AlternativeLabel : Label")]
        public string AttributeExpressionExample;

        [FoldoutGroup("Field Name Example")]
        [SuffixLabel("$Label")]
        public string FieldNameExample;

        public string Label = "Peace, Love & Ducks";

        [FoldoutGroup("Method Name Example")]
        [SuffixLabel("$GetLabel")]
        public string MethodNameExample;

        [FoldoutGroup("Property Name Example")]
        [SuffixLabel("$LabelProperty")]
        public string PropertyNameExample;

        public bool UseAlternativeLabel;
        public string LabelProperty => UseAlternativeLabel ? AlternativeLabel : Label;

        string GetLabel() => UseAlternativeLabel ? AlternativeLabel : Label;
    }
    // End
}
