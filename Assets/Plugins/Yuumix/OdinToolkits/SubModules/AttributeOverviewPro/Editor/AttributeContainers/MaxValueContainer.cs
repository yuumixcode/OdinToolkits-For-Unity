using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class MaxValueContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "MaxValue";

        protected override string GetIntroduction() => "MaxValue 用于基本字段，使用它来定义字段的最大值";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParamValue> GetParamValues() =>
            new List<ParamValue>
            {
                new ParamValue
                {
                    returnType = "double",
                    paramName = "maxValue",
                    paramDescription = "字段的最大值"
                },
                new ParamValue
                {
                    returnType = "string",
                    paramName = "expression",
                    paramDescription = DescriptionConfigs.SupportAllResolver
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(MinMaxValueExample));
    }
}
