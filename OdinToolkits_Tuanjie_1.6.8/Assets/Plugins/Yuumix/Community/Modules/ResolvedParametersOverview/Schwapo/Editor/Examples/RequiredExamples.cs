using Sirenix.OdinInspector;

namespace Yuumix.Community.Schwapo.Editor
{
    [ResolvedParameterExample]
    public class RequiredExamples_ErrorMessage
    {
        public string AlternativeMessage = "Peace, Love & Fenrir";

        [FoldoutGroup("Attribute Expression Example")]
        [Required(ErrorMessage = "@UseAlternativeMessage ? AlternativeMessage : Message")]
        public string AttributeExpressionExample;

        [FoldoutGroup("Field Name Example")]
        [Required(ErrorMessage = "$Message")]
        public string FieldNameExample;

        public string Message = "Peace, Love & Ducks";

        [FoldoutGroup("Method Name Example")]
        [Required(ErrorMessage = "$GetMessage")]
        public string MethodNameExample;

        [FoldoutGroup("Property Name Example")]
        [Required(ErrorMessage = "$MessageProperty")]
        public string PropertyNameExample;

        public bool UseAlternativeMessage;
        public string MessageProperty => UseAlternativeMessage ? AlternativeMessage : Message;

        string GetMessage() => UseAlternativeMessage ? AlternativeMessage : Message;
    }
    // End
}
