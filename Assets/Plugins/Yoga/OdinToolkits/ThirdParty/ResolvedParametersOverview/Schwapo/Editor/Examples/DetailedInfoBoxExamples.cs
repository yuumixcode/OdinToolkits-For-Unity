using Sirenix.OdinInspector;

namespace YOGA.Modules.OdinToolkits.Schwapo.Editor.Examples
{
    [ResolvedParameterExample]
    public class DetailedInfoBoxExamples_VisibleIf
    {
        [FoldoutGroup("Attribute Expression Example")]
        [DetailedInfoBox("Ducks are...", "Ducks are awesome", VisibleIf = "@IsVisible")]
        public string AttributeExpressionExample;

        [FoldoutGroup("Field Name Example")]
        [DetailedInfoBox("Ducks are...", "Ducks are awesome", VisibleIf = "IsVisibles")]
        public string FieldNameExample;

        public bool IsVisible = true;

        [FoldoutGroup("Method Name Example")]
        [DetailedInfoBox("Ducks are...", "Ducks are awesome", VisibleIf = "GetVisibility")]
        public string MethodNameExample;

        [FoldoutGroup("Property Name Example")]
        [DetailedInfoBox("Ducks are...", "Ducks are awesome", VisibleIf = "IsVisibleProperty")]
        public string PropertyNameExample;

        public bool IsVisibleProperty => IsVisible;

        bool GetVisibility() => IsVisible;
    }
    // End

    [ResolvedParameterExample]
    public class DetailedInfoBoxExamples_Message
    {
        [FoldoutGroup("Attribute Expression Example")]
        [DetailedInfoBox("@Message", "Ducks are awesome")]
        public string AttributeExpressionExample;

        [FoldoutGroup("Field Name Example")]
        [DetailedInfoBox("$Message", "Ducks are awesome")]
        public string FieldNameExample;

        public string Message = "Ducks are...";

        [FoldoutGroup("Method Name Example")]
        [DetailedInfoBox("$GetMessage", "Ducks are awesome")]
        public string MethodNameExample;

        [FoldoutGroup("Property Name Example")]
        [DetailedInfoBox("$MessageProperty", "Ducks are awesome")]
        public string PropertyNameExample;

        public string MessageProperty => Message;

        string GetMessage() => Message;
    }
    // End

    [ResolvedParameterExample]
    public class DetailedInfoBoxExamples_Details
    {
        [FoldoutGroup("Attribute Expression Example")]
        [DetailedInfoBox("Ducks are...", "@Details")]
        public string AttributeExpressionExample;

        public string Details = "Ducks are awesome";

        [FoldoutGroup("Field Name Example")]
        [DetailedInfoBox("Ducks are...", "$Details")]
        public string FieldNameExample;

        [FoldoutGroup("Method Name Example")]
        [DetailedInfoBox("Ducks are...", "$GetDetails")]
        public string MethodNameExample;

        [FoldoutGroup("Property Name Example")]
        [DetailedInfoBox("Ducks are...", "$DetailsProperty")]
        public string PropertyNameExample;

        public string DetailsProperty => Details;

        string GetDetails() => Details;
    }
    // End
}