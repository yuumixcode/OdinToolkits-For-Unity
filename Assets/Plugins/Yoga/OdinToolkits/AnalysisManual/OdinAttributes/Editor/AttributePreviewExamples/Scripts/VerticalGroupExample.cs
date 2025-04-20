using Sirenix.OdinInspector;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    [IsChineseAttributeExample]
    public class VerticalGroupExample : ExampleScriptableObject
    {
        [HorizontalGroup("Horizon", width: 0.5f, Title = "横向组")]
        [VerticalGroup("Horizon/Left", PaddingBottom = 100)]
        public int verticalGroup1;

        [HorizontalGroup("Horizon")]
        [VerticalGroup("Horizon/Left")]
        public int verticalGroup2;

        [HorizontalGroup("Horizon")]
        [VerticalGroup("Horizon/Right")]
        public int verticalGroup3;

        public int normalGroup;
    }
}