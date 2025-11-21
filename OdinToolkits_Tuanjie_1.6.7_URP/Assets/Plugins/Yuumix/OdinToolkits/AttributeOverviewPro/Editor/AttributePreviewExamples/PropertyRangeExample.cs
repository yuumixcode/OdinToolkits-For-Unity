using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [AttributeOverviewProExample]
    public class PropertyRangeExample : ExampleSO
    {
        [PropertyRange(0, 10)]
        public int propertyRange1;
    }
}
