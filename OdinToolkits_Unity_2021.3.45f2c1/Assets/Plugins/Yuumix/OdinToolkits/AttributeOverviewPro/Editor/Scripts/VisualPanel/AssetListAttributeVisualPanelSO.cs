namespace Yuumix.OdinToolkits.AttributeOverviewPro.Editor
{
    [AttributeCategory(OdinAttributeCategory.TypeSpecifics)]
    public class AssetListAttributeVisualPanelSO : AbstractAttributeVisualPanelSO
    {
        public override void Initialize()
        {
            SetModel(new AssetListAttributeModel());
        }
    }
}
