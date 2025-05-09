using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Sirenix.OdinInspector;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class ToggleLeftExample : ExampleScriptableObject
    {
        [InfoBox("绘制勾选框在左侧")] [ToggleLeft] public bool leftToggled;

        [EnableIf("leftToggled")] public int A;

        [EnableIf("leftToggled")] public bool B;

        [EnableIf("leftToggled")] public bool C;
    }
}