using Sirenix.OdinInspector;

namespace YOGA.Modules.OdinToolkits.Schwapo.Editor.Examples
{
    [ResolvedParameterExample]
    public class PropertyRangeExamples_MinGetter
    {
        [FoldoutGroup("Attribute Expression Example")]
        [PropertyRange("@UseIncreasedMin ? IncreasedMin : Min", 10f)]
        public float AttributeExpressionExample = 7.5f;

        [FoldoutGroup("Field Name Example")]
        [PropertyRange("Min", 10f)]
        public float FieldNameExample = 7.5f;

        public float IncreasedMin = 5f;

        [FoldoutGroup("Method Name Example")]
        [PropertyRange("GetMin", 10f)]
        public float MethodNameExample = 7.5f;

        public float Min = 0f;

        [FoldoutGroup("Property Name Example")]
        [PropertyRange("MinProperty", 10f)]
        public float PropertyNameExample = 7.5f;

        public bool UseIncreasedMin;
        public float MinProperty => UseIncreasedMin ? IncreasedMin : Min;

        float GetMin() => UseIncreasedMin ? IncreasedMin : Min;
    }
    // End

    [ResolvedParameterExample]
    public class PropertyRangeExamples_MaxGetter
    {
        [FoldoutGroup("Attribute Expression Example")]
        [PropertyRange(0f, "@UseIncreasedMax ? IncreasedMax : Max")]
        public float AttributeExpressionExample = 7.5f;

        [FoldoutGroup("Field Name Example")]
        [PropertyRange(0f, "Max")]
        public float FieldNameExample = 7.5f;

        public float IncreasedMax = 20f;
        public float Max = 10f;

        [FoldoutGroup("Method Name Example")]
        [PropertyRange(0f, "GetMax")]
        public float MethodNameExample = 7.5f;

        [FoldoutGroup("Property Name Example")]
        [PropertyRange(0f, "MaxProperty")]
        public float PropertyNameExample = 7.5f;

        public bool UseIncreasedMax;
        public float MaxProperty => UseIncreasedMax ? IncreasedMax : Max;

        float GetMax() => UseIncreasedMax ? IncreasedMax : Max;
    }
    // End
}