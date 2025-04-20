using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class DisableInPlayModeContainer : AbsContainer
    {
        protected override string SetHeader() => "DisableInPlayMode";

        protected override string SetBrief() => "简介";

        protected override List<string> SetTip() => new List<string>()
        {
            
        };

        protected override List<ParamValue> SetParamValues() => new List<ParamValue>()
        {
            
        };

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(DisableInPlayModeExample));
    }
}