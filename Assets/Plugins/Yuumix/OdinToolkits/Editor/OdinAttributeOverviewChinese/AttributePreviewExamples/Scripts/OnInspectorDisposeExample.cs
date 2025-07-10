using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class OnInspectorDisposeExample : ExampleScriptableObject
    {
        [OnInspectorDispose("@UnityEngine.Debug.Log(\"Dispose event invoked!\")")]
        [ShowInInspector]
        [InfoBox("当修改这个多态字段时会执行 Dispose，或者放弃选择此 Example 时也会触发")]
        [DisplayAsString]
        public BaseClass PolymorphicField;

        public abstract class BaseClass
        {
            public override string ToString() => GetType().Name;
        }

        public class A : BaseClass { }

        public class B : BaseClass { }

        public class C : BaseClass { }
    }
}
