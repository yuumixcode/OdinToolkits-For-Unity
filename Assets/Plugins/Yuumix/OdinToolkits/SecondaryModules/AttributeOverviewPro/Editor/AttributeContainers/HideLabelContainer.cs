using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class HideLabelContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "HideLabel";

        protected override string GetIntroduction() => "隐藏字符串的名称";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "可以和其他特性配合使用，自定义字段的显示"
            };

        protected override List<ParamValue> GetParamValues() => new List<ParamValue>();

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(HideLabelExample));
    }
}
