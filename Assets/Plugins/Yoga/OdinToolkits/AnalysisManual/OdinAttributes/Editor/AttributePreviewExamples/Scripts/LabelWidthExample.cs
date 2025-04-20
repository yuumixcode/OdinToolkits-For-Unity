using Sirenix.OdinInspector;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    [IsChineseAttributeExample]
    public class LabelWidthExample : ExampleScriptableObject
    {
        public int defaultWidth;

        [LabelWidth(50)]
        public int thin;

        [LabelWidth(250)]
        public int wide;
    }
}