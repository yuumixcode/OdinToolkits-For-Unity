using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class EnableGUIContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "EnableGUI";

        protected override string GetIntroduction() => "强制启用 property ，使其正常显示";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "部分 property 是灰色显示无法修改，可以使用 [EnableGUI] 来恢复正常，仅优化显示样式"
            };

        protected override List<ParameterValue> GetParamValues() => new List<ParameterValue>();

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(EnableGUIExample));
    }
}
