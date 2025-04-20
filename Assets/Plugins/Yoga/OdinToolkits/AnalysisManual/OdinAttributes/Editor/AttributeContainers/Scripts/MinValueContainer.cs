using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class MinValueContainer : AbsContainer
    {
        protected override string SetHeader() => "MinValue";

        protected override string SetBrief() => "MaxValue 用于基本字段，使用它来定义字段的最小值";

        protected override List<string> SetTip() => new List<string>()
            { };

        protected override List<ParamValue> SetParamValues() => new List<ParamValue>()
        {
            new ParamValue()
            {
                returnType = "double",
                paramName = "minValue",
                paramDescription = "字段的最小值"
            },
            new ParamValue()
            {
                returnType = "string",
                paramName = "expression",
                paramDescription = DescriptionConfigs.SupportAllResolver
            }
        };

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(MinMaxValueExample));
    }
}