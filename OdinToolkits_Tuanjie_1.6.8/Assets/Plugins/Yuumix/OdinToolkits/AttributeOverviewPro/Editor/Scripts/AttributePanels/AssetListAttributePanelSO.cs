namespace Yuumix.OdinToolkits.AttributeOverviewPro.Editor
{
    [AttributeCategory(OdinAttributeCategory.TypeSpecifics)]
    public class AssetListAttributePanelSO : AbstractAttributePanelSO
    {
        public override void Initialize()
        {
            SetData(new AssetListAttributeData());
        }
    }
}
