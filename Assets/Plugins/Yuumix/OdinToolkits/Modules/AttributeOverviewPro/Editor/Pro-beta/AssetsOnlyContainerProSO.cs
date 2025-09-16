using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules.AttributeOverviewPro.Editor
{
    public class AssetsOnlyContainerProSO : AttributeContainerProSO
    {
        protected override BilingualHeaderWidget GetHeaderWidget() => new BilingualHeaderWidget(
            "AssetsOnly",
            "AssetsOnly",
            "AssetsOnly 用于 Object 对象，确保被标记的字段或者属性引用项目中的资源，而不是场景中的物体。");

        public override void OdinToolkitsReset()
        {
            BaseReset();
        }
    }
}
