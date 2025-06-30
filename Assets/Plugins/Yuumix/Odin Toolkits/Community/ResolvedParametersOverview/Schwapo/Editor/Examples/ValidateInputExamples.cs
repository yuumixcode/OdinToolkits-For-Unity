using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.ThirdParty.ResolvedParametersOverview.Schwapo.Editor.Examples
{
    [ResolvedParameterExample]
    public class ValidateInputExamples_DefaultMessage
    {
        public string AlternativeMessage = "Peace, Love & Fenrir";

        [FoldoutGroup("Attribute Expression Example")]
        [ValidateInput("@false",
            "@UseAlternativeMessage ? AlternativeMessage : Message",
            ContinuousValidationCheck = true)]
        public string AttributeExpressionExample;

        [FoldoutGroup("Field Name Example")]
        [ValidateInput("@false", "$Message", ContinuousValidationCheck = true)]
        public string FieldNameExample;

        public string Message = "Peace, Love & Ducks";

        [FoldoutGroup("Method Name Example")]
        [ValidateInput("@false", "$GetMessage", ContinuousValidationCheck = true)]
        public string MethodNameExample;

        [FoldoutGroup("Property Name Example")]
        [ValidateInput("@false", "$AlternativeMessage", ContinuousValidationCheck = true)]
        public string PropertyNameExample;

        public bool UseAlternativeMessage = true;
        public string MessageProperty => UseAlternativeMessage ? AlternativeMessage : Message;

        string GetMessage() => UseAlternativeMessage ? AlternativeMessage : Message;
    }
    // End

    [ResolvedParameterExample]
    public class ValidateInputExamples_Condition
    {
        [FoldoutGroup("Attribute Expression Example")]
        [ValidateInput("@!string.IsNullOrWhiteSpace($value)", "Field can't be empty")]
        public string AttributeExpressionExample;

        [FoldoutGroup("Method Name Example")]
        [ValidateInput("IsValid")]
        public string MethodNameExample;

        bool IsValid(string value, ref string message, ref InfoMessageType messageType)
        {
            message = "Field can't be empty";
            messageType = InfoMessageType.Error;

            return !string.IsNullOrWhiteSpace(value);
        }
    }
    // End
}
