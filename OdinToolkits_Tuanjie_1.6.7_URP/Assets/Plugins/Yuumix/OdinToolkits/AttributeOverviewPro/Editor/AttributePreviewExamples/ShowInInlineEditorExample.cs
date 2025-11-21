using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [AttributeOverviewProExample]
    public class ShowInInlineEditorExample : ExampleSO
    {
        [InlineEditor]
        public CommonInlineObject commonInlineObject;
    }
}
