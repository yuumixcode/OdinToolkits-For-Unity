using Sirenix.OdinInspector;

namespace YOGA.Modules.OdinToolkits.Schwapo.Editor.Examples
{
    [ResolvedParameterExample]
    public class MinValueExamples_Expression
    {
        [FoldoutGroup("Attribute Expression Example")]
        [MinValue("@MinValue")]
        public float AttributeExpressionExample;

        [FoldoutGroup("Field Name Example")]
        [MinValue("MinValue")]
        public float FieldNameExample;

        [FoldoutGroup("Method Name Example")]
        [MinValue("GetMinValue")]
        public float MethodNameExample;

        public float MinValue = 10f;

        [FoldoutGroup("Property Name Example")]
        [MinValue("MinValueProperty")]
        public float PropertyNameExample;

        public float MinValueProperty => MinValue;

        float GetMinValue() => MinValue;
    }
    // End
}