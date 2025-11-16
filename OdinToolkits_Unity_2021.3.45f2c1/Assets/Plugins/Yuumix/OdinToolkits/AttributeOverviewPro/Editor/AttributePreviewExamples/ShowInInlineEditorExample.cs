using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [OdinToolkitsAttributeExample]
    public class ShowInInlineEditorExample : ExampleSO
    {
        [InlineEditor]
        public CommonInlineObject commonInlineObject;
    }
}
