using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
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
