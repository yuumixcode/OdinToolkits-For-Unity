using Sirenix.OdinInspector;

namespace Community.Schwapo.Editor
{
    [ResolvedParameterExample]
    public class ShowIfGroupExamples_Condition
    {
        [ShowIfGroup("Show", Condition = "@ShowGroup")]
        [FoldoutGroup("Show/Attribute Expression Example")]
        public string AttributeExpressionExample;

        [ShowIfGroup("Show", Condition = "ShowGroup")]
        [FoldoutGroup("Show/Field Name Example")]
        public string FieldNameExample;

        [ShowIfGroup("Show", Condition = "GetShowState")]
        [FoldoutGroup("Show/Method Name Example")]
        public string MethodNameExample;

        [ShowIfGroup("Show", Condition = "ShowGroupProperty")]
        [FoldoutGroup("Show/Property Name Example")]
        public string PropertyNameExample;

        public bool ShowGroup = true;
        public bool ShowGroupProperty => ShowGroup;

        bool GetShowState() => ShowGroup;
    }
    // End
}
