using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Sirenix.OdinInspector;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class VerticalGroupExample : ExampleScriptableObject
    {
        [HorizontalGroup("Horizon", 0.5f, Title = "横向组")] [VerticalGroup("Horizon/Left", PaddingBottom = 100)]
        public int verticalGroup1;

        [HorizontalGroup("Horizon")] [VerticalGroup("Horizon/Left")]
        public int verticalGroup2;

        [HorizontalGroup("Horizon")] [VerticalGroup("Horizon/Right")]
        public int verticalGroup3;

        public int normalGroup;
    }
}