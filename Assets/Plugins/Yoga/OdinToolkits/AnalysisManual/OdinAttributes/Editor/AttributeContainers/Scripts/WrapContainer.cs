using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class WrapContainer : AbsContainer
    {
        protected override string SetHeader() => "Wrap";

        protected override string SetBrief() => "可以对大部分的基础变量使用，让它在达到某个值时，开始循环";

        protected override List<string> SetTip() => new List<string>()
            { "对角度值可以使用" };

        protected override List<ParamValue> SetParamValues() => new List<ParamValue>()
        {
            new ParamValue()
            {
                returnType = "double",
                paramName = "min",
                paramDescription = "最小值" + DescriptionConfigs.SupportAllResolver
            },
            new ParamValue()
            {
                returnType = "double",
                paramName = "max",
                paramDescription = "最大值" + DescriptionConfigs.SupportAllResolver
            }
        };

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(WrapExample));
    }
}