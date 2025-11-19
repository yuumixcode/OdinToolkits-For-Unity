using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Editor;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class PropertyOrderContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "PropertyOrder";

        protected override string GetIntroduction() => "自定义控制所有 Property 的绘制顺序";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "PropertyOrder 用来自定义绘制顺序，默认序号为 0，数字越大越靠后绘制，可以为负数",
                "推荐把 [PropertyOrder] 特性放到第一位"
            };

        protected override List<ParameterValue> GetParamValues() => new List<ParameterValue>();

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(PropertyOrderExample));
    }
}
