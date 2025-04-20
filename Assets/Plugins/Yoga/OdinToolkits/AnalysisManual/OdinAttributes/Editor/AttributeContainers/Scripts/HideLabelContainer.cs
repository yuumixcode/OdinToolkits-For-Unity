using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class HideLabelContainer : AbsContainer
    {
        protected override string SetHeader() => "HideLabel";

        protected override string SetBrief() => "隐藏字符串的名称";

        protected override List<string> SetTip() => new List<string>()
        {
            "可以和其他特性配合使用，自定义字段的显示"
        };

        protected override List<ParamValue> SetParamValues() => new List<ParamValue>();

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(HideLabelExample));
    }
}