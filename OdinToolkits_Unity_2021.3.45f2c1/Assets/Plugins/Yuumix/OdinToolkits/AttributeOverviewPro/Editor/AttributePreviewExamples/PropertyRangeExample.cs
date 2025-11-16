using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [OdinToolkitsAttributeExample]
    public class PropertyRangeExample : ExampleSO
    {
        [PropertyRange(0, 10)]
        public int propertyRange1;
    }
}
