using YuumixEditor;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Editor
{
    internal class AssetsOnlyAttributeModel : AbstractAttributeModel
    {
        public override void Initialize()
        {
            HeaderWidget = new BilingualHeaderWidget("AssetsOnly", "AssetsOnly", "选择一个资产", "Select an asset");
        }
    }
}
