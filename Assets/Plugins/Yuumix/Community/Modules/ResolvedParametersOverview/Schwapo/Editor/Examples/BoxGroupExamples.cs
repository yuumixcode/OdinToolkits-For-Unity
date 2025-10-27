using Sirenix.OdinInspector;

namespace Yuumix.Community.Schwapo.Editor
{
    [ResolvedParameterExample]
    public class BoxGroupExamples_LabelText
    {
        public string AlternativeLabel = "Peace, Love & Fenrir";

        [FoldoutGroup("Attribute Expression Example")]
        [BoxGroup("Attribute Expression Example/Box", LabelText = "@UseAlternativeLabel ? AlternativeLabel : Label")]
        public string AttributeExpressionExample;

        [FoldoutGroup("Field Name Example")]
        [BoxGroup("Field Name Example/Box", LabelText = "$Label")]
        public string FieldNameExample;

        public string Label = "Peace, Love & Ducks";

        [FoldoutGroup("Method Name Example")]
        [BoxGroup("Method Name Example/Box", LabelText = "$GetLabelText")]
        public string MethodNameExample;

        [FoldoutGroup("Property Name Example")]
        [BoxGroup("Property Name Example/Box", LabelText = "$LabelProperty")]
        public string PropertyNameExample;

        public bool UseAlternativeLabel;
        public string LabelProperty => UseAlternativeLabel ? AlternativeLabel : Label;

        string GetLabelText() => UseAlternativeLabel ? AlternativeLabel : Label;
    }
    // End
}
