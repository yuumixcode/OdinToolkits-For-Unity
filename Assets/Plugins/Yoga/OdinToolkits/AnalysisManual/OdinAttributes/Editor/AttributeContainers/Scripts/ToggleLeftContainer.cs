using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class ToggleLeftContainer : AbsContainer
    {
        protected override string SetHeader() => "ToggleLeft";

        protected override string SetBrief() => "将 bool 类型的 Property 的勾选框放置在左边";

        protected override List<string> SetTip() => new List<string>()
        {
            "和 [EnableIf] 等条件特性配合使用"
        };

        protected override List<ParamValue> SetParamValues() => new List<ParamValue>()
            { };

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(ToggleLeftExample));
    }
}