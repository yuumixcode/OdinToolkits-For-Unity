using Sirenix.OdinInspector;

namespace Yuumix.Community.Schwapo.Editor
{
    [ResolvedParameterExample]
    public class HideIfGroupExamples_Condition
    {
        [HideIfGroup("Hidden", Condition = "@HideGroup")]
        [FoldoutGroup("Hidden/Attribute Expression Example")]
        public string AttributeExpressionExample;

        [HideIfGroup("Hidden", Condition = "HideGroup")]
        [FoldoutGroup("Hidden/Field Name Example")]
        public string FieldNameExample;

        public bool HideGroup;

        [HideIfGroup("Hidden", Condition = "GetHiddenState")]
        [FoldoutGroup("Hidden/Method Name Example")]
        public string MethodNameExample;

        [HideIfGroup("Hidden", Condition = "HideGroupProperty")]
        [FoldoutGroup("Hidden/Property Name Example")]
        public string PropertyNameExample;

        public bool HideGroupProperty => HideGroup;

        bool GetHiddenState() => HideGroup;
    }
    // End
}
