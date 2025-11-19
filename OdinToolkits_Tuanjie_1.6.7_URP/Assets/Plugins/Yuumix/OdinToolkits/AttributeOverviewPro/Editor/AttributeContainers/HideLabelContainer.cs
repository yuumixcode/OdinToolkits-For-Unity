using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Editor;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

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

        protected override List<ParameterValue> GetParamValues() => new List<ParameterValue>();

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(HideLabelExample));
    }
}
