using Sirenix.OdinInspector;

namespace YOGA.Modules.OdinToolkits.Schwapo.Editor.Examples
{
    [ResolvedParameterExample]
    public class HideIfExamples_Condition
    {
        [FoldoutGroup("Attribute Expression Example")]
        [HideIf("@IsHidden")]
        public string AttributeExpressionExample;

        [FoldoutGroup("Field Name Example")]
        [HideIf("IsHidden")]
        public string FieldNameExample;

        public bool IsHidden;

        [FoldoutGroup("Method Name Example")]
        [HideIf("GetHiddenState")]
        public string MethodNameExample;

        [FoldoutGroup("Property Name Example")]
        [HideIf("IsHiddenProperty")]
        public string PropertyNameExample;

        public bool IsHiddenProperty => IsHidden;

        bool GetHiddenState() => IsHidden;
    }
    // End
}