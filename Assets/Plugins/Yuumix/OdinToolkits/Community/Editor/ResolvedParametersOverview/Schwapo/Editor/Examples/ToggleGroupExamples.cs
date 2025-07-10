using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.ThirdParty.ResolvedParametersOverview.Schwapo.Editor.Examples
{
    [ResolvedParameterExample]
    public class ToggleGroupExamples_ToggleGroupTitle
    {
        public string AlternativeTitle = "Peace, Love & Fenrir";
        public string Title = "Peace, Love & Ducks";

        [FoldoutGroup("Field Name Example")]
        [ToggleGroup("Field Name Example/Toggle1", ToggleGroupTitle = "$Title")]
        public bool Toggle1;

        [FoldoutGroup("Property Name Example")]
        [ToggleGroup("Property Name Example/Toggle2", ToggleGroupTitle = "$TitleProperty")]
        public bool Toggle2;

        [FoldoutGroup("Attribute Expression Example")]
        [ToggleGroup("Attribute Expression Example/Toggle3",
            ToggleGroupTitle = "@UseAlternativeTitle ? AlternativeTitle : Title")]
        public bool Toggle3;

        [FoldoutGroup("Method Name Example")]
        [ToggleGroup("Method Name Example/Toggle4", ToggleGroupTitle = "$GetTitle")]
        public bool Toggle4;

        public bool UseAlternativeTitle;
        public string TitleProperty => UseAlternativeTitle ? AlternativeTitle : Title;

        string GetTitle() => UseAlternativeTitle ? AlternativeTitle : Title;
    }
    // End
}
