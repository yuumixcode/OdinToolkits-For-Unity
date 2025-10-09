using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Modules.Editor
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

        protected override List<ParamValue> GetParamValues() => new List<ParamValue>();

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(EnableGUIExample));
    }
}
