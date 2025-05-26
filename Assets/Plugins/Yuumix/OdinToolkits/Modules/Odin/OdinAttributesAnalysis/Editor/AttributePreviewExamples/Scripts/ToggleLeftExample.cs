using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class ToggleLeftExample : ExampleScriptableObject
    {
        [InfoBox("绘制勾选框在左侧")]
        [ToggleLeft]
        public bool leftToggled;

        [EnableIf("leftToggled")]
        public int A;

        [EnableIf("leftToggled")]
        public bool B;

        [EnableIf("leftToggled")]
        public bool C;
    }
}
