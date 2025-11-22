namespace Yuumix.OdinToolkits.AttributeOverviewPro.Editor
{
    [AttributeCategory(OdinAttributeCategory.Essential)]
    public class AssetsOnlyAttributeVisualPanelSO : AbstractAttributeVisualPanelSO
    {
        public override void Initialize()
        {
            SetModel(new AssetsOnlyAttributeModel());
        }
    }
}
