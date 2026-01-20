using Sirenix.OdinInspector;

namespace Yuumix.Community.Schwapo.Editor
{
    [ResolvedParameterExample]
    public class TitleGroupExamples_GroupName
    {
        public string AlternativeTitle = "Peace, Love & Fenrir";

        [FoldoutGroup("Attribute Expression Example")]
        [TitleGroup("Attribute Expression Example/Title",
            GroupName = "@UseAlternativeTitle ? AlternativeTitle : Title")]
        public string AttributeExpressionExample;

        [FoldoutGroup("Field Name Example")]
        [TitleGroup("Field Name Example/Title", GroupName = "$Title")]
        public string FieldNameExample;

        [FoldoutGroup("Method Name Example")]
        [TitleGroup("Method Name Example/Title", GroupName = "$GetTitle")]
        public string MethodNameExample;

        [FoldoutGroup("Property Name Example")]
        [TitleGroup("Property Name Example/Title", GroupName = "$TitleProperty")]
        public string PropertyNameExample;

        public string Title = "Peace, Love & Ducks";
        public bool UseAlternativeTitle;
        public string TitleProperty => UseAlternativeTitle ? AlternativeTitle : Title;

        string GetTitle() => UseAlternativeTitle ? AlternativeTitle : Title;
    }
    // End

    [ResolvedParameterExample]
    public class TitleGroupExamples_Subtitle
    {
        public string AlternativeSubtitle = "Peace, Love & Fenrir";

        [FoldoutGroup("Attribute Expression Example")]
        [TitleGroup("Attribute Expression Example/Title",
            Subtitle = "@UseAlternativeSubtitle ? AlternativeSubtitle : Subtitle")]
        public string AttributeExpressionExample;

        [FoldoutGroup("Field Name Example")]
        [TitleGroup("Field Name Example/Title", Subtitle = "$Subtitle")]
        public string FieldNameExample;

        [FoldoutGroup("Method Name Example")]
        [TitleGroup("Method Name Example/Title", Subtitle = "$GetSubtitle")]
        public string MethodNameExample;

        [FoldoutGroup("Property Name Example")]
        [TitleGroup("Property Name Example/Title", Subtitle = "$SubtitleProperty")]
        public string PropertyNameExample;

        public string Subtitle = "Peace, Love & Ducks";
        public bool UseAlternativeSubtitle;
        public string SubtitleProperty => UseAlternativeSubtitle ? AlternativeSubtitle : Subtitle;

        string GetSubtitle() => UseAlternativeSubtitle ? AlternativeSubtitle : Subtitle;
    }
    // End
}
