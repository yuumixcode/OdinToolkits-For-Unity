using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class EnableGUIContainer : AbsContainer
    {
        protected override string SetHeader() => "EnableGUI";

        protected override string SetBrief() => "强制启用 property ，使其正常显示";

        protected override List<string> SetTip() => new List<string>()
        {
            "部分 property 是灰色显示无法修改，可以使用 [EnableGUI] 来恢复正常，仅优化显示样式"
        };

        protected override List<ParamValue> SetParamValues() => new List<ParamValue>();

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(EnableGUIExample));
    }
}
