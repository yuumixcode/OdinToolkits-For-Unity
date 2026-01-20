namespace Yuumix.OdinToolkits.AttributeOverviewPro.Editor
{
    [AttributeCategory(OdinAttributeCategory.Essentials)]
    public class AssetsOnlyAttributePanelSO : AbstractAttributePanelSO
    {
        public override void Initialize()
        {
            SetData(new AssetsOnlyAttributeData());
        }
    }
}
