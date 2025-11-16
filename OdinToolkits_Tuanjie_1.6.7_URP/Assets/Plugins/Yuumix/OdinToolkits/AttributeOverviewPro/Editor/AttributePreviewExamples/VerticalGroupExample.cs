using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [OdinToolkitsAttributeExample]
    public class VerticalGroupExample : ExampleSO
    {
        #region Serialized Fields

        [HorizontalGroup("Horizon", 0.5f, Title = "横向组")]
        [VerticalGroup("Horizon/Left", PaddingBottom = 100)]
        public int verticalGroup1;

        [HorizontalGroup("Horizon")]
        [VerticalGroup("Horizon/Left")]
        public int verticalGroup2;

        [HorizontalGroup("Horizon")]
        [VerticalGroup("Horizon/Right")]
        public int verticalGroup3;

        public int normalGroup;

        #endregion
    }
}
