using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class ToggleLeftContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "ToggleLeft";

        protected override string GetIntroduction() => "将 bool 类型的 Property 的勾选框放置在左边";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "和 [EnableIf] 等条件特性配合使用"
            };

        protected override List<ParameterValue> GetParamValues() => new List<ParameterValue>();

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(ToggleLeftExample));
    }
}
