using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Runtime.Scripts;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    [AttributeOverviewProExample]
    public class DisableInInlineEditorExample : ExampleSO
    {
        #region Serialized Fields

        [InlineEditor]
        public CommonInlineObject commonInlineObject;

        #endregion
    }
}
