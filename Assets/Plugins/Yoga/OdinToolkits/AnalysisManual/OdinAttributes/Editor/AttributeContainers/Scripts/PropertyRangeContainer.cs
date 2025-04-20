using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class PropertyRangeContainer : AbsContainer
    {
        protected override string SetHeader() => "PropertyRange";

        protected override string SetBrief() => "和 Unity 的 Range 类似，但是 PropertyRange 可以应用到属性";

        protected override List<string> SetTip() => new List<string>()
            { };

        protected override List<ParamValue> SetParamValues() => new List<ParamValue>()
        {
            new ParamValue()
            {
                returnType = "double",
                paramName = "min",
                paramDescription = "最小值，还有类似参数 minGetter，" + DescriptionConfigs.SupportAllResolver
            },
            new ParamValue()
            {
                returnType = "double",
                paramName = "max",
                paramDescription = "最大值，还有类似参数 maxGetter，" + DescriptionConfigs.SupportAllResolver
            }
        };

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(PropertyRangeExample));
    }
}