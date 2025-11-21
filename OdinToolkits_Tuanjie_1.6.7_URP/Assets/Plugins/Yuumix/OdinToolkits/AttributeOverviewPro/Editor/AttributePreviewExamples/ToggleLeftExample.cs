using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [AttributeOverviewProExample]
    public class ToggleLeftExample : ExampleSO
    {
        #region Serialized Fields

        [InfoBox("绘制勾选框在左侧")]
        [ToggleLeft]
        public bool leftToggled;

        [EnableIf("leftToggled")]
        public int A;

        [EnableIf("leftToggled")]
        public bool B;

        [EnableIf("leftToggled")]
        public bool C;

        #endregion
    }
}
