using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class ShowDrawerChainContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "ShowDrawerChain";
        }

        protected override string SetBrief()
        {
            return "用于调试查看 Property 的绘制链，查看具体绘制行为";
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
            return ReadCodeWithoutNamespace(typeof(ShowDrawerChainExample));
        }
    }
}