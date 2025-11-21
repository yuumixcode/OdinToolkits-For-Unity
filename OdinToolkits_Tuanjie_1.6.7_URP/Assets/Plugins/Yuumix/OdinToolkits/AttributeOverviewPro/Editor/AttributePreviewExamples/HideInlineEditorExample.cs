using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [AttributeOverviewProExample]
    public class HideInlineEditorExample : ExampleSO
    {
        #region Serialized Fields

        [InlineEditor]
        public CommonInlineObject commonInlineObject;

        #endregion
    }
}
