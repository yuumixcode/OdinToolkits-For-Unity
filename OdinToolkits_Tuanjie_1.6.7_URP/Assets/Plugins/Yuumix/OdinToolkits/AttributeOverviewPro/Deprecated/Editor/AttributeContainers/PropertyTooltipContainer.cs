using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class PropertyTooltipContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "PropertyTooltip";

        protected override string GetIntroduction() => "给任意 Property 添加 Tooltip";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "任意 Property 包括字段，显示的属性，绘制的方法"
            };

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "Tooltip",
                    ParameterDescription = "Tooltip 文本，" + DescriptionConfigs.SupportAllResolver
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(PropertyTooltipExample));
    }
}
