using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Editor
{
    public class ShowDrawerChainContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "ShowDrawerChain";

        protected override string GetIntroduction() => "用于调试查看 Property 的绘制链，查看具体绘制行为";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParamValue> GetParamValues() => new List<ParamValue>();

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(ShowDrawerChainExample));
    }
}
