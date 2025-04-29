using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Sirenix.OdinInspector;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class LabelWidthExample : ExampleScriptableObject
    {
        public int defaultWidth;

        [LabelWidth(50)] public int thin;

        [LabelWidth(250)] public int wide;
    }
}