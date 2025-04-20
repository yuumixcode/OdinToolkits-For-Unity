using Sirenix.OdinInspector;

namespace YOGA.Modules.OdinToolkits.Schwapo.Editor.Examples
{
    [ResolvedParameterExample]
    public class EnableIfExamples_Condition
    {
        [FoldoutGroup("Attribute Expression Example")]
        [EnableIf("@IsEnabled")]
        public string AttributeExpressionExample;

        [FoldoutGroup("Field Name Example")]
        [EnableIf("IsEnabled")]
        public string FieldNameExample;

        public bool IsEnabled;

        [FoldoutGroup("Method Name Example")]
        [EnableIf("GetEnabledState")]
        public string MethodNameExample;

        [FoldoutGroup("Property Name Example")]
        [EnableIf("IsEnabledProperty")]
        public string PropertyNameExample;

        public bool IsEnabledProperty => IsEnabled;

        bool GetEnabledState() => IsEnabled;
    }
    // End
}