using Sirenix.OdinInspector;

namespace Yuumix.Community.Schwapo.Editor
{
    [ResolvedParameterExample]
    public class MaxValueExamples_Expression
    {
        [FoldoutGroup("Attribute Expression Example")]
        [MaxValue("@MaxValue")]
        public float AttributeExpressionExample;

        [FoldoutGroup("Field Name Example")]
        [MaxValue("MaxValue")]
        public float FieldNameExample;

        public float MaxValue = 10f;

        [FoldoutGroup("Method Name Example")]
        [MaxValue("GetMaxValue")]
        public float MethodNameExample;

        [FoldoutGroup("Property Name Example")]
        [MaxValue("MaxValueProperty")]
        public float PropertyNameExample;

        public float MaxValueProperty => MaxValue;

        float GetMaxValue() => MaxValue;
    }
    // End
}
