using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class ToggleLeftContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "ToggleLeft";
        }

        protected override string SetBrief()
        {
            return "将 bool 类型的 Property 的勾选框放置在左边";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "和 [EnableIf] 等条件特性配合使用"
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>();
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(ToggleLeftExample));
        }
    }
}