using Sirenix.OdinInspector;
using UnityEngine;

namespace YOGA.Modules.OdinToolkits.Schwapo.Editor.Examples
{
    [ResolvedParameterExample]
    public class MinMaxSliderExamples_MinMaxValueGetter
    {
        [FoldoutGroup("Attribute Expression Example")]
        [MinMaxSlider("@ExpandRange ? MinMaxValuesExpanded : MinMaxValues")]
        public Vector2 AttributeExpressionExample = new Vector2(0f, 10f);

        public bool ExpandRange;

        [FoldoutGroup("Field Name Example")]
        [MinMaxSlider("MinMaxValues")]
        public Vector2 FieldNameExample = new Vector2(0f, 10f);

        [FoldoutGroup("Method Name Example")]
        [MinMaxSlider("GetMinMaxValues")]
        public Vector2 MethodNameExample = new Vector2(0f, 10f);

        public Vector2 MinMaxValues = new Vector2(0f, 10f);
        public Vector2 MinMaxValuesExpanded = new Vector2(0f, 20f);

        [FoldoutGroup("Property Name Example")]
        [MinMaxSlider("MinMaxValuesProperty")]
        public Vector2 PropertyNameExample = new Vector2(0f, 10f);

        public Vector2 MinMaxValuesProperty => ExpandRange ? MinMaxValuesExpanded : MinMaxValues;

        Vector2 GetMinMaxValues() => ExpandRange ? MinMaxValuesExpanded : MinMaxValues;
    }
    // End

    [ResolvedParameterExample]
    public class MinMaxSliderExamples_MinValueGetter
    {
        [FoldoutGroup("Attribute Expression Example")]
        [MinMaxSlider("@UseIncreasedMinValue ? IncreasedMinValue : MinValue", 10f)]
        public Vector2 AttributeExpressionExample = new Vector2(7.5f, 10f);

        [FoldoutGroup("Field Name Example")]
        [MinMaxSlider("MinValue", 10f)]
        public Vector2 FieldNameExample = new Vector2(7.5f, 10f);

        public float IncreasedMinValue = 5f;

        [FoldoutGroup("Method Name Example")]
        [MinMaxSlider("GetMinValue", 10f)]
        public Vector2 MethodNameExample = new Vector2(7.5f, 10f);

        public float MinValue = 0f;

        [FoldoutGroup("Property Name Example")]
        [MinMaxSlider("MinValueProperty", 10f)]
        public Vector2 PropertyNameExample = new Vector2(7.5f, 10f);

        public bool UseIncreasedMinValue;
        public float MinValueProperty => UseIncreasedMinValue ? IncreasedMinValue : MinValue;

        float GetMinValue() => UseIncreasedMinValue ? IncreasedMinValue : MinValue;
    }
    // End

    [ResolvedParameterExample]
    public class MinMaxSliderExamples_MaxValueGetter
    {
        [FoldoutGroup("Attribute Expression Example")]
        [MinMaxSlider(0f, "@UseIncreasedMaxValue ? IncreasedMaxValue : MaxValue")]
        public Vector2 AttributeExpressionExample = new Vector2(0f, 10f);

        [FoldoutGroup("Field Name Example")]
        [MinMaxSlider(0f, "MaxValue")]
        public Vector2 FieldNameExample = new Vector2(0f, 10f);

        public float IncreasedMaxValue = 20f;
        public float MaxValue = 10f;

        [FoldoutGroup("Method Name Example")]
        [MinMaxSlider(0f, "GetMaxValue")]
        public Vector2 MethodNameExample = new Vector2(0f, 10f);

        [FoldoutGroup("Property Name Example")]
        [MinMaxSlider(0f, "MaxValueProperty")]
        public Vector2 PropertyNameExample = new Vector2(0f, 10f);

        public bool UseIncreasedMaxValue;
        public float MaxValueProperty => UseIncreasedMaxValue ? IncreasedMaxValue : MaxValue;

        float GetMaxValue() => UseIncreasedMaxValue ? IncreasedMaxValue : MaxValue;
    }
    // End
}