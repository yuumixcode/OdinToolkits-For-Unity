using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Editor
{
    public class AssetsOnlyAttributeVisualPanelSO : AbstractAttributeVisualPanelSO
    {
        [PropertyOrder(-1000)]
        [OnInspectorInit]
        public void Initialize()
        {
            SetModel(new AssetsOnlyAttributeModel());
        }
    }
}
