using Sirenix.OdinInspector;

namespace YOGA.Modules.OdinToolkits.Schwapo.Editor.Examples
{
    [ResolvedParameterExample]
    public class DisableIfExamples_Condition
    {
        [FoldoutGroup("Attribute Expression Example")] [DisableIf("@IsDisabled")]
        public string AttributeExpressionExample;

        [FoldoutGroup("Field Name Example")] [DisableIf("IsDisabled")]
        public string FieldNameExample;

        public bool IsDisabled;

        [FoldoutGroup("Method Name Example")] [DisableIf("GetDisabledState")]
        public string MethodNameExample;

        [FoldoutGroup("Property Name Example")] [DisableIf("IsDisabledProperty")]
        public string PropertyNameExample;

        public bool IsDisabledProperty => IsDisabled;

        private bool GetDisabledState()
        {
            return IsDisabled;
        }
    }
    // End
}