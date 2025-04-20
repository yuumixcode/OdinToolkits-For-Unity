using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class HideInPlayModeContainer : AbsContainer
    {
        protected override string SetHeader() => "HideInPlayMode";

        protected override string SetBrief() => "Play 状态下隐藏 Property";

        protected override List<string> SetTip() => new List<string>()
            { };

        protected override List<ParamValue> SetParamValues() => new List<ParamValue>()
            { };

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(HideInPlayModeExample));
    }
}