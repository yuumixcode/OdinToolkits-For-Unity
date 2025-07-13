using System;
using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.Editor
{
    [OdinToolkitsAttributeExample]
    public class TypeInfoBoxExample : ExampleSO
    {
        [PropertyOrder(1)]
        public MyType myObject = new MyType();

        [Serializable]
        [TypeInfoBox("将会直接绘制一个 InfoBox 在类的顶部，这个特性是放在 class 上")]
        public class MyType
        {
            public int value;
        }
    }
}
