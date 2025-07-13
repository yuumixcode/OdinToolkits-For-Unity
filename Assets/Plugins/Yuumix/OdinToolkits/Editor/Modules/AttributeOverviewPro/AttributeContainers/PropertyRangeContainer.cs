using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Editor
{
    public class PropertyRangeContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "PropertyRange";

        protected override string GetIntroduction() => "和 Unity 的 Range 类似，但是 PropertyRange 可以应用到属性";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParamValue> GetParamValues() =>
            new List<ParamValue>
            {
                new ParamValue
                {
                    returnType = "double",
                    paramName = "min",
                    paramDescription = "最小值，还有类似参数 minGetter，" + DescriptionConfigs.SupportAllResolver
                },
                new ParamValue
                {
                    returnType = "double",
                    paramName = "max",
                    paramDescription = "最大值，还有类似参数 maxGetter，" + DescriptionConfigs.SupportAllResolver
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(PropertyRangeExample));
    }
}
