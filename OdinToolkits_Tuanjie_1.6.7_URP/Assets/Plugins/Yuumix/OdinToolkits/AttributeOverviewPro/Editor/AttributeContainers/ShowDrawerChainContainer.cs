using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Editor;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class ShowDrawerChainContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "ShowDrawerChain";

        protected override string GetIntroduction() => "用于调试查看 Property 的绘制链，查看具体绘制行为";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParameterValue> GetParamValues() => new List<ParameterValue>();

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(ShowDrawerChainExample));
    }
}
