using Sirenix.OdinInspector;
using System;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [OdinToolkitsAttributeExample]
    public class TypeInfoBoxExample : ExampleSO
    {
        #region Serialized Fields

        [PropertyOrder(1)]
        public MyType myObject = new MyType();

        #endregion

        #region Nested type: ${0}

        [Serializable]
        [TypeInfoBox("将会直接绘制一个 InfoBox 在类的顶部，这个特性是放在 class 上")]
        public class MyType
        {
            #region Serialized Fields

            public int value;

            #endregion
        }

        #endregion
    }
}
