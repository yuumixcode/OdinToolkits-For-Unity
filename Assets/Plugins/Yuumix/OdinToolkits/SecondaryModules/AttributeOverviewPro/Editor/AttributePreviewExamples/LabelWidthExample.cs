using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [OdinToolkitsAttributeExample]
    public class LabelWidthExample : ExampleSO
    {
        #region Serialized Fields

        public int defaultWidth;

        [LabelWidth(50)]
        public int thin;

        [LabelWidth(250)]
        public int wide;

        #endregion
    }
}
