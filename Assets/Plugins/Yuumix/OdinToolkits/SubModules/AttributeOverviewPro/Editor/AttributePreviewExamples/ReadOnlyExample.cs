using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [OdinToolkitsAttributeExample]
    public class ReadOnlyExample : ExampleSO
    {
        [ReadOnly]
        public string readOnly1 = "这个字段是只读的，不可以在面板上修改，但是可以通过代码修改";

        [PropertyOrder(1)]
        [InfoBox("可以用在集合类型上 | Array | List，让集合也变成只读")]
        [ReadOnly]
        public List<int> readOnlyList = new List<int> { 1, 2, 3 };

        [ShowInInspector] [ReadOnly] public string ReadOnly2 => "这个属性是只读的";
    }
}
