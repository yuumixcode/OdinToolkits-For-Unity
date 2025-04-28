using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class EnumToggleButtonsContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "EnumToggleButtons";
        }

        protected override string SetBrief()
        {
            return "让枚举变成一排按钮，同时支持可以支持多选，更直观";
        }

        protected override List<string> SetTip()
        {
            return new List<string>();
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>();
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(EnumToggleButtonsExample));
        }
    }
}