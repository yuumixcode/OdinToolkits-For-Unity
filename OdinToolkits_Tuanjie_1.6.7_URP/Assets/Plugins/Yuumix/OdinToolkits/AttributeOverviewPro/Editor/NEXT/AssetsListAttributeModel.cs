using System.Collections.Generic;
using YuumixEditor;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Editor
{
    internal class AssetsListAttributeModel : IAbstractAttributeModel
    {
        public BilingualHeaderWidget HeaderWidget { get; set; }
        public List<string> UsageTips { get; set; }

        public void Initialize()
        {
            HeaderWidget = new BilingualHeaderWidget("AssetsList", "AssetsList", "选择多个资产", "Select multiple assets");
        }
    }
}
