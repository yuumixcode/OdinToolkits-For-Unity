using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class LabelTextContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "LabelText";
        }

        protected override string SetBrief()
        {
            return "自定义 Property 的 Label 名称字符串";
        }

        protected override List<string> SetTip()
        {
            return new List<string>();
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>
            {
                new()
                {
                    returnType = "string",
                    paramName = "text",
                    paramDescription = "自定义的名称字符串，" + DescriptionConfigs.SupportAllResolver
                },
                new()
                {
                    returnType = "bool",
                    paramName = "nicifyText",
                    paramDescription = "是否首字母大写优化，默认为 false，例如 m_someField 自动变成 Some Field"
                },
                new()
                {
                    returnType = "SdfIconType",
                    paramName = "icon",
                    paramDescription = "图标类型，默认为 SdfIconType.None"
                },
                new()
                {
                    returnType = "string",
                    paramName = "IconColor",
                    paramDescription = DescriptionConfigs.ColorDescription
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(LabelTextExample));
        }
    }
}