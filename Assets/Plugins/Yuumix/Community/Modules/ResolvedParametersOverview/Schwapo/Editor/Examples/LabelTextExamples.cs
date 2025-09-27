using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.Community.Schwapo.Editor
{
    [ResolvedParameterExample]
    public class LabelTextExamples_Text
    {
        [FoldoutGroup("Attribute Expression Example")]
        [LabelText("@Label")]
        public string AttributeExpressionExample;

        [FoldoutGroup("Field Name Example")]
        [LabelText("$Label")]
        public string FieldNameExample;

        public string Label = "Peace, Love & Ducks";

        [FoldoutGroup("Method Name Example")]
        [LabelText("$GetLabel")]
        public string MethodNameExample;

        [FoldoutGroup("Property Name Example")]
        [LabelText("$LabelProperty")]
        public string PropertyNameExample;

        public string LabelProperty => Label;

        string GetLabel() => Label;
    }
    // End
}
