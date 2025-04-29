using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Sirenix.OdinInspector;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class HideReferenceObjectPickerExample : ExampleOdinScriptableObject
    {
        [Title("Hidden Object Pickers")] [HideReferenceObjectPicker] [Indent]
        public MyCustomReferenceType OdinSerializedProperty1 = new();

        [HideReferenceObjectPicker] [Indent] public MyCustomReferenceType OdinSerializedProperty2 = new();

        [Title("Shown Object Pickers")] [Indent]
        public MyCustomReferenceType OdinSerializedProperty3 = new();

        [Indent] public MyCustomReferenceType OdinSerializedProperty4 = new();

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