using Sirenix.OdinInspector;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    [IsChineseAttributeExample]
    public class PropertyRangeExample : ExampleScriptableObject
    {
        [PropertyRange(0, 10)] public int propertyRange1;
    }
}