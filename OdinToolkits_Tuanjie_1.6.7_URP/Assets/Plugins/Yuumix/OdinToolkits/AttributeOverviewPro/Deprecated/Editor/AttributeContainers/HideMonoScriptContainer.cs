using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class HideMonoScriptContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "HideMonoScript";

        protected override string GetIntroduction() => "隐藏灰色显示的脚本文件的 Property";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "可以在 Odin 的全局设置中关闭"
            };

        protected override List<ParameterValue> GetParamValues() => new List<ParameterValue>();

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(HideMonoScriptExample));
    }
}
