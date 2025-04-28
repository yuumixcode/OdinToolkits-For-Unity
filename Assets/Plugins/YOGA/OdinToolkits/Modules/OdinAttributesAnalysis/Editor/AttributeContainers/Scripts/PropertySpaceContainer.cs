using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class PropertySpaceContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "PropertySpace";
        }

        protected override string SetBrief()
        {
            return "控制 Property 中的值的前后间隔";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "PropertySpace 的间隔距离直接作用于 Property 中的值的那一行，不会包括 Group，实际会扩大 Group 的范围"
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>
            {
                new()
                {
                    returnType = "float",
                    paramName = "spaceBefore",
                    paramDescription = "和上一个 property 的距离，像素为单位"
                },
                new()
                {
                    returnType = "float",
                    paramName = "spaceAfter",
                    paramDescription = "和下一个 property 的距离，像素为单位"
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(PropertySpaceExample));
        }
    }
}