using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class LabelWidthContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "LabelWidth";
        }

        protected override string SetBrief()
        {
            return "Property 名称字符串的宽度";
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
                    returnType = "float",
                    paramName = "width",
                    paramDescription = "宽度值，单位为像素"
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(LabelWidthExample));
        }
    }
}