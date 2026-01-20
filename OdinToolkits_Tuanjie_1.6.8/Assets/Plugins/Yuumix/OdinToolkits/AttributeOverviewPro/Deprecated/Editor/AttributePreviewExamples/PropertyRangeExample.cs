using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    [AttributeOverviewProExample]
    public class PropertyRangeExample : ExampleSO
    {
        #region Serialized Fields

        [PropertyRange(0, 10)]
        public int propertyRange1;

        #endregion
    }
}
