using Sirenix.OdinInspector;

namespace YOGA.Modules.OdinToolkits.Schwapo.Editor.Examples
{
    [ResolvedParameterExample]
    public class HorizontalGroupExamples_Title
    {
        public string AlternativeTitle = "Peace, Love & Fenrir";

        [FoldoutGroup("Attribute Expression Example")]
        [HorizontalGroup("Attribute Expression Example/Horizontal",
            Title = "@UseAlternativeTitle ? AlternativeTitle : Title")]
        public string AttributeExpressionExample;

        [FoldoutGroup("Field Name Example")] [HorizontalGroup("Field Name Example/Horizontal", Title = "$Title")]
        public string FieldNameExample;

        [FoldoutGroup("Method Name Example")] [HorizontalGroup("Method Name Example/Horizontal", Title = "$GetTitle")]
        public string MethodNameExample;

        [FoldoutGroup("Property Name Example")]
        [HorizontalGroup("Property Name Example/Horizontal", Title = "$TitleProperty")]
        public string PropertyNameExample;

        public string Title = "Peace, Love & Ducks";
        public bool UseAlternativeTitle;
        public string TitleProperty => UseAlternativeTitle ? AlternativeTitle : Title;

        private string GetTitle()
        {
            return UseAlternativeTitle ? AlternativeTitle : Title;
        }
    }
    // End
}