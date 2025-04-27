using Sirenix.OdinInspector;
using UnityEngine;

namespace YOGA.Modules.OdinToolkits.Schwapo.Editor.Examples
{
    [ResolvedParameterExample]
    public class ProgressBarExamples_MinGetter
    {
        [FoldoutGroup("Attribute Expression Example")] [ProgressBar("@UseIncreasedMin ? IncreasedMin : Min", 10f)]
        public float AttributeExpressionExample = 7.5f;

        [FoldoutGroup("Field Name Example")] [ProgressBar("Min", 10f)]
        public float FieldNameExample = 7.5f;

        public float IncreasedMin = 5f;

        [FoldoutGroup("Method Name Example")] [ProgressBar("GetMin", 10f)]
        public float MethodNameExample = 7.5f;

        public float Min = 0f;

        [FoldoutGroup("Property Name Example")] [ProgressBar("MinProperty", 10f)]
        public float PropertyNameExample = 7.5f;

        public bool UseIncreasedMin;
        public float MinProperty => UseIncreasedMin ? IncreasedMin : Min;

        private float GetMin()
        {
            return UseIncreasedMin ? IncreasedMin : Min;
        }
    }
    // End

    [ResolvedParameterExample]
    public class ProgressBarExamples_MaxGetter
    {
        [FoldoutGroup("Attribute Expression Example")] [ProgressBar(0f, "@UseIncreasedMax ? IncreasedMax : Max")]
        public float AttributeExpressionExample = 7.5f;

        [FoldoutGroup("Field Name Example")] [ProgressBar(0f, "Max")]
        public float FieldNameExample = 7.5f;

        public float IncreasedMax = 20f;
        public float Max = 10f;

        [FoldoutGroup("Method Name Example")] [ProgressBar(0f, "GetMax")]
        public float MethodNameExample = 7.5f;

        [FoldoutGroup("Property Name Example")] [ProgressBar(0f, "MaxProperty")]
        public float PropertyNameExample = 7.5f;

        public bool UseIncreasedMax;
        public float MaxProperty => UseIncreasedMax ? IncreasedMax : Max;

        private float GetMax()
        {
            return UseIncreasedMax ? IncreasedMax : Max;
        }
    }
    // End

    [ResolvedParameterExample]
    public class ProgressBarExamples_ColorGetter
    {
        public Color AlternativeColor = new(1f, 0.79f, 0.14f, 1f);

        [FoldoutGroup("Attribute Expression Example")]
        [ProgressBar(0f, 10f, ColorGetter = "@UseAlternativeColor ? AlternativeColor : Color")]
        public float AttributeExpressionExample = 7.5f;

        public Color Color = new(0.11f, 0.77f, 0.5f, 1f);

        [FoldoutGroup("Field Name Example")] [ProgressBar(0f, 10f, ColorGetter = "Color")]
        public float FieldNameExample = 7.5f;

        [FoldoutGroup("Method Name Example")] [ProgressBar(0f, 10f, ColorGetter = "GetColor")]
        public float MethodNameExample = 7.5f;

        [FoldoutGroup("Property Name Example")] [ProgressBar(0f, 10f, ColorGetter = "ColorProperty")]
        public float PropertyNameExample = 7.5f;

        public bool UseAlternativeColor;
        public Color ColorProperty => UseAlternativeColor ? AlternativeColor : Color;

        private Color GetColor()
        {
            return UseAlternativeColor ? AlternativeColor : Color;
        }
    }
    // End

    [ResolvedParameterExample]
    public class ProgressBarExamples_BackgroundColorGetter
    {
        public Color AlternativeColor = new(1f, 0.79f, 0.14f, 1f);

        [FoldoutGroup("Attribute Expression Example")]
        [ProgressBar(0f, 10f, BackgroundColorGetter = "@UseAlternativeColor ? AlternativeColor : Color")]
        public float AttributeExpressionExample = 7.5f;

        public Color Color = new(0.11f, 0.77f, 0.5f, 1f);

        [FoldoutGroup("Field Name Example")] [ProgressBar(0f, 10f, BackgroundColorGetter = "Color")]
        public float FieldNameExample = 7.5f;

        [FoldoutGroup("Method Name Example")] [ProgressBar(0f, 10f, BackgroundColorGetter = "GetColor")]
        public float MethodNameExample = 7.5f;

        [FoldoutGroup("Property Name Example")] [ProgressBar(0f, 10f, BackgroundColorGetter = "ColorProperty")]
        public float PropertyNameExample = 7.5f;

        public bool UseAlternativeColor;
        public Color ColorProperty => UseAlternativeColor ? AlternativeColor : Color;

        private Color GetColor()
        {
            return UseAlternativeColor ? AlternativeColor : Color;
        }
    }
    // End

    [ResolvedParameterExample]
    public class ProgressBarExamples_CustomValueStringGetter
    {
        public string AlternativeString = "Alternative Custom String";

        [FoldoutGroup("Attribute Expression Example")]
        [ProgressBar(0f, 10f, CustomValueStringGetter = "@\"The current value is:\" + $value")]
        public float AttributeExpressionExample = 7.5f;

        [FoldoutGroup("Field Name Example")] [ProgressBar(0f, 10f, CustomValueStringGetter = "$ValueString")]
        public float FieldNameExample = 7.5f;

        [FoldoutGroup("Method Name Example")] [ProgressBar(0f, 10f, CustomValueStringGetter = "$GetValueString")]
        public float MethodNameExample = 7.5f;

        [FoldoutGroup("Property Name Example")] [ProgressBar(0f, 10f, CustomValueStringGetter = "$ValueStringProperty")]
        public float PropertyNameExample = 7.5f;

        public bool UseAlternativeString;
        public string ValueString = "Custom Value String";
        public string ValueStringProperty => UseAlternativeString ? AlternativeString : ValueString;

        private string GetValueString(float value)
        {
            return "The current value is: " + value;
        }
    }
    // End
}