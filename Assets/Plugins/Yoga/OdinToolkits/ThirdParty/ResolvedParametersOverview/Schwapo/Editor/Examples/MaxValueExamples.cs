using Sirenix.OdinInspector;

namespace YOGA.Modules.OdinToolkits.Schwapo.Editor.Examples
{
    [ResolvedParameterExample]
    public class MaxValueExamples_Expression
    {
        [FoldoutGroup("Attribute Expression Example")] [MaxValue("@MaxValue")]
        public float AttributeExpressionExample;

        [FoldoutGroup("Field Name Example")] [MaxValue("MaxValue")]
        public float FieldNameExample;

        public float MaxValue = 10f;

        [FoldoutGroup("Method Name Example")] [MaxValue("GetMaxValue")]
        public float MethodNameExample;

        [FoldoutGroup("Property Name Example")] [MaxValue("MaxValueProperty")]
        public float PropertyNameExample;

        public float MaxValueProperty => MaxValue;

        private float GetMaxValue()
        {
            return MaxValue;
        }
    }
    // End
}