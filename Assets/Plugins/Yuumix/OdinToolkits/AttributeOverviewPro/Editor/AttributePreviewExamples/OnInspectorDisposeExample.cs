using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [OdinToolkitsAttributeExample]
    public class OnInspectorDisposeExample : ExampleSO
    {
        [OnInspectorDispose("@UnityEngine.Debug.Log(\"Dispose event invoked!\")")]
        [ShowInInspector]
        [InfoBox("当修改这个多态字段时会执行 Dispose，或者放弃选择此 Example 时也会触发")]
        [DisplayAsString]
        public BaseClass PolymorphicField;

        #region Nested type: ${0}

        public abstract class BaseClass
        {
            public override string ToString() => GetType().Name;
        }

        public class A : BaseClass { }

        public class B : BaseClass { }

        public class C : BaseClass { }

        #endregion
    }
}
