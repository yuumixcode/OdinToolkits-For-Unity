using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Editor
{
    public class AssetListAttributeVisualPanelSO : AbstractAttributeVisualPanelSO
    {
        [PropertyOrder(-1000)]
        [OnInspectorInit]
        void Initialize()
        {
            SetModel(new AssetListAttributeModel());
        }
    }
}
