using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.ThirdParty.ResolvedParametersOverview.Schwapo.Editor.Examples
{
    [ResolvedParameterExample]
    public class ShowIfExamples_Condition
    {
        [FoldoutGroup("Attribute Expression Example")]
        [ShowIf("@Show")]
        public string AttributeExpressionExample;

        [FoldoutGroup("Field Name Example")]
        [ShowIf("Show")]
        public string FieldNameExample;

        [FoldoutGroup("Method Name Example")]
        [ShowIf("GetShowState")]
        public string MethodNameExample;

        [FoldoutGroup("Property Name Example")]
        [ShowIf("ShowProperty")]
        public string PropertyNameExample;

        public bool Show = true;
        public bool ShowProperty => Show;

        bool GetShowState() => Show;
    }
    // End
}
