using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class HideDuplicateReferenceBoxContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "HideDuplicateReferenceBox";

        protected override string GetIntroduction() =>
            "如果多个 Property 的值都引用同一个，那么 Odin 将绘制一个 Box 告知用户，该值是同一个，" +
            "[HideDuplicateReferenceBox] 可以关闭此绘制";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "如果是递归调用，将无法关闭 Box 的绘制",
                "一般在 Inspector 面板中，也不会相互引用，而且就算有，Odin 使用 Box 也可以更好提醒"
            };

        protected override List<ParameterValue> GetParamValues() => new List<ParameterValue>();

        protected override string GetOriginalCode() =>
            ReadCodeWithoutNamespace(typeof(HideDuplicateReferenceBoxExample));
    }
}
