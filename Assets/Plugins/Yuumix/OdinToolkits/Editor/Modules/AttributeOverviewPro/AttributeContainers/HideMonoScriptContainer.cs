using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Editor
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

        protected override List<ParamValue> GetParamValues() => new List<ParamValue>();

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(HideMonoScriptExample));
    }
}
