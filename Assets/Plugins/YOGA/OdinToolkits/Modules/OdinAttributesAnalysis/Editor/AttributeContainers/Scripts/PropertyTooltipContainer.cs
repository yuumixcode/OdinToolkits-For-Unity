using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class PropertyTooltipContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "PropertyTooltip";
        }

        protected override string SetBrief()
        {
            return "给任意 Property 添加 Tooltip";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "任意 Property 包括字段，显示的属性，绘制的方法"
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>
            {
                new()
                {
                    returnType = "string",
                    paramName = "Tooltip",
                    paramDescription = "Tooltip 文本，" + DescriptionConfigs.SupportAllResolver
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(PropertyTooltipExample));
        }
    }
}