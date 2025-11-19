using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Editor;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class MinValueContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "MinValue";

        protected override string GetIntroduction() => "MaxValue 用于基本字段，使用它来定义字段的最小值";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "double",
                    ParameterName = "minValue",
                    ParameterDescription = "字段的最小值"
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
