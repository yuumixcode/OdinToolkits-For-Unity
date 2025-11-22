using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class ShowPropertyResolverContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "ShowPropertyResolver";

        protected override string GetIntroduction() => "显示负责将成员引入属性树的属性解析器";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "一个通常不会在检查器中显示的特定成员突然出现时，可以使用此特性调试",
                "此示例基于 Odin 序列化"
            };

        protected override List<ParameterValue> GetParamValues() => new List<ParameterValue>();

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(ShowPropertyResolverExample));
    }
}
