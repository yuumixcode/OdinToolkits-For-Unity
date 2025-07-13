using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.Editor
{
    [OdinToolkitsAttributeExample]
    public class HideReferenceObjectPickerExample : ExampleOdinSO
    {
        [Title("Hidden Object Pickers")]
        [HideReferenceObjectPicker]
        [Indent]
        public MyCustomReferenceType OdinSerializedProperty1 = new MyCustomReferenceType();

        [HideReferenceObjectPicker]
        [Indent]
        public MyCustomReferenceType OdinSerializedProperty2 = new MyCustomReferenceType();

        [Title("Shown Object Pickers")]
        [Indent]
        public MyCustomReferenceType OdinSerializedProperty3 = new MyCustomReferenceType();

        [Indent]
        public MyCustomReferenceType OdinSerializedProperty4 = new MyCustomReferenceType();

        // 可以直接在类上使用
        // [HideReferenceObjectPicker]
        public class MyCustomReferenceType
        {
            public int A;
            public int B;
            public int C;
        }
    }
}
