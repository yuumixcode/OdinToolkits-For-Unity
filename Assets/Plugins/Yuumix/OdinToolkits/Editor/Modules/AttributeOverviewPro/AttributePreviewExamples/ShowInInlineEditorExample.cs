using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.Editor
{
    [OdinToolkitsAttributeExample]
    public class ShowInInlineEditorExample : ExampleSO
    {
        [InlineEditor]
        public CommonInlineObject commonInlineObject;
    }
}
