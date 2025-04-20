using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class EnumToggleButtonsContainer : AbsContainer
    {
        protected override string SetHeader() => "EnumToggleButtons";

        protected override string SetBrief() => "让枚举变成一排按钮，同时支持可以支持多选，更直观";

        protected override List<string> SetTip() => new List<string>()
        {
            
        };

        protected override List<ParamValue> SetParamValues() => new List<ParamValue>()
        {
            
        };

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(EnumToggleButtonsExample));
    }
}