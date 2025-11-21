using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [AttributeOverviewProExample]
    public class DisableInInlineEditorExample : ExampleSO
    {
        [InlineEditor]
        public CommonInlineObject commonInlineObject;
    }
}
