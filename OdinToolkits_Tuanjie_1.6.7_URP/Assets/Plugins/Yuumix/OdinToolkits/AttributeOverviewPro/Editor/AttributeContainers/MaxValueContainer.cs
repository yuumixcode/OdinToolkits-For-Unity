using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Editor;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class MaxValueContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "MaxValue";

        protected override string GetIntroduction() => "MaxValue 用于基本字段，使用它来定义字段的最大值";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "double",
                    ParameterName = "maxValue",
                    ParameterDescription = "字段的最大值"
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "expression",
                    ParameterDescription = DescriptionConfigs.SupportAllResolver
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(MinMaxValueExample));
    }
}
