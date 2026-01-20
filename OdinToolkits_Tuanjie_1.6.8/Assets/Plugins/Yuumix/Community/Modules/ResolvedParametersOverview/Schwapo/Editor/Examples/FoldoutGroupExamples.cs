using Sirenix.OdinInspector;

namespace Yuumix.Community.Schwapo.Editor
{
    [ResolvedParameterExample]
    public class FoldoutGroupExamples_GroupName
    {
        public string AlternativeGroupName = "Peace, Love & Fenrir";

        [FoldoutGroup("Attribute Expression Example")]
        [FoldoutGroup("Attribute Expression Example/Foldout",
            GroupName = "@UseAlternativeGroupName ? AlternativeGroupName : GroupName")]
        public string AttributeExpressionExample;

        [FoldoutGroup("Field Name Example")]
        [FoldoutGroup("Field Name Example/Foldout", GroupName = "$GroupName")]
        public string FieldNameExample;

        public string GroupName = "Peace, Love & Ducks";

        [FoldoutGroup("Method Name Example")]
        [FoldoutGroup("Method Name Example/Foldout", GroupName = "$GetGroupName")]
        public string MethodNameExample;

        [FoldoutGroup("Property Name Example")]
        [FoldoutGroup("Property Name Example/Foldout", GroupName = "$GroupNameProperty")]
        public string PropertyNameExample;

        public bool UseAlternativeGroupName;
        public string GroupNameProperty => UseAlternativeGroupName ? AlternativeGroupName : GroupName;

        string GetGroupName() => UseAlternativeGroupName ? AlternativeGroupName : GroupName;
    }
    // End
}
