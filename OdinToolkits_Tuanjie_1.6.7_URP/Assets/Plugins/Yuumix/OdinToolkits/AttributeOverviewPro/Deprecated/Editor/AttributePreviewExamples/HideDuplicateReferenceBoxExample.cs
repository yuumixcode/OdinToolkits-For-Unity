using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    [AttributeOverviewProExample]
    public class HideDuplicateReferenceBoxExample : ExampleOdinSO
    {
        static readonly List<MyClass> StaticList = new List<MyClass>
        {
            new MyClass { a = 1, b = 2, c = 3 },
            new MyClass { a = 3, b = 2, c = 1 }
        };

        [TitleGroup("循环引用", "没有标记 [Serializable]，由 Odin 序列化")]
        public ReferenceTypeClass FirstObject;

        [HideDuplicateReferenceBox]
        public ReferenceTypeClass SecondObject;

        [PropertyOrder(-1)]
        [Title("原始 List")]
        [OnInspectorGUI]
        void OnGUI1() { }

        [Button("设置嵌套", ButtonSizes.Large)]
        void SetReference()
        {
            FirstObject = new ReferenceTypeClass();
            FirstObject.RecursiveRef = FirstObject;
            SecondObject = FirstObject;
        }

        #region Serialized Fields

        public List<MyClass> list0 = StaticList;

        [TitleGroup("List1")]
        public List<MyClass> list1 = StaticList;

        [TitleGroup("List2 标记 [HideDuplicateReferenceBox]")]
        [HideDuplicateReferenceBox]
        public List<MyClass> list2 = StaticList;

        #endregion

        #region Nested type: ${0}

        [Serializable]
        public class MyClass
        {
            #region Serialized Fields

            public int a;
            public int b;
            public int c;

            #endregion
        }

        public class ReferenceTypeClass
        {
            public int A;
            public ReferenceTypeClass RecursiveRef;
        }

        #endregion
    }
}
