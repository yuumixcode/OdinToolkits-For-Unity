using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.Community.Schwapo.Editor
{
    [ResolvedParameterExample]
    public class TitleExamples_Title
    {
        public string AlternativeTitle = "Peace, Love & Fenrir";

        [FoldoutGroup("Attribute Expression Example")]
        [Title("@UseAlternativeTitle ? AlternativeTitle : Title")]
        public string AttributeExpressionExample;

        [FoldoutGroup("Field Name Example")]
        [Title("$Title")]
        public string FieldNameExample;

        [FoldoutGroup("Method Name Example")]
        [Title("$GetTitle")]
        public string MethodNameExample;

        [FoldoutGroup("Property Name Example")]
        [Title("$TitleProperty")]
        public string PropertyNameExample;

        public string Title = "Peace, Love & Ducks";
        public bool UseAlternativeTitle;
        public string TitleProperty => UseAlternativeTitle ? AlternativeTitle : Title;

        string GetTitle() => UseAlternativeTitle ? AlternativeTitle : Title;
    }
    // End

    [ResolvedParameterExample]
    public class TitleExamples_Subtitle
    {
        public string AlternativeSubtitle = "Peace, Love & Fenrir";

        [FoldoutGroup("Attribute Expression Example")]
        [Title("Title", "@UseAlternativeSubtitle ? AlternativeSubtitle : Subtitle")]
        public string AttributeExpressionExample;

        [FoldoutGroup("Field Name Example")]
        [Title("Title", "$Subtitle")]
        public string FieldNameExample;

        [FoldoutGroup("Method Name Example")]
        [Title("Title", "$GetSubtitle")]
        public string MethodNameExample;

        [FoldoutGroup("Property Name Example")]
        [Title("Title", "$SubtitleProperty")]
        public string PropertyNameExample;

        public string Subtitle = "Peace, Love & Ducks";
        public bool UseAlternativeSubtitle;
        public string SubtitleProperty => UseAlternativeSubtitle ? AlternativeSubtitle : Subtitle;

        string GetSubtitle() => UseAlternativeSubtitle ? AlternativeSubtitle : Subtitle;
    }
    // End
}
