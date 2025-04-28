using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class HideInPlayModeContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "HideInPlayMode";
        }

        protected override string SetBrief()
        {
            return "Play 状态下隐藏 Property";
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
            return ReadCodeWithoutNamespace(typeof(HideInPlayModeExample));
        }
    }
}