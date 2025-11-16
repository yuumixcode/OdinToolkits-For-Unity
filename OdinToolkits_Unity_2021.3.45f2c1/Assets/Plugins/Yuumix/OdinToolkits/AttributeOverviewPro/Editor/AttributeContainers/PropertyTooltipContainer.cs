using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Modules.Editor
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

        protected override List<ParamValue> GetParamValues() =>
            new List<ParamValue>
            {
                new ParamValue
                {
                    returnType = "string",
                    paramName = "Tooltip",
                    paramDescription = "Tooltip 文本，" + DescriptionConfigs.SupportAllResolver
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(PropertyTooltipExample));
    }
}
