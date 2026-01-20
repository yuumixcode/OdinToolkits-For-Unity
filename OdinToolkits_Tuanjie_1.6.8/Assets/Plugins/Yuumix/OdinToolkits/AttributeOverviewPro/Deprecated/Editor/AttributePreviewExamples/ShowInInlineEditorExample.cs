using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Runtime.Scripts;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    [AttributeOverviewProExample]
    public class ShowInInlineEditorExample : ExampleSO
    {
        #region Serialized Fields

        [InlineEditor]
        public CommonInlineObject commonInlineObject;

        #endregion
    }
}
