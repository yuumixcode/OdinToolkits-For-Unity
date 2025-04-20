using Sirenix.OdinInspector;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
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