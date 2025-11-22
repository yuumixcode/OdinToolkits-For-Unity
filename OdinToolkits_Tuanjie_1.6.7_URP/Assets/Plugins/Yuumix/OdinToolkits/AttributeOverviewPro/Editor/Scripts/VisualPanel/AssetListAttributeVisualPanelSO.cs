namespace Yuumix.OdinToolkits.AttributeOverviewPro.Editor
{
    [AttributeCategory(OdinAttributeCategory.TypeSpecific)]
    public class AssetListAttributeVisualPanelSO : AbstractAttributeVisualPanelSO
    {
        public override void Initialize()
        {
            SetModel(new AssetListAttributeModel());
        }
    }
}
