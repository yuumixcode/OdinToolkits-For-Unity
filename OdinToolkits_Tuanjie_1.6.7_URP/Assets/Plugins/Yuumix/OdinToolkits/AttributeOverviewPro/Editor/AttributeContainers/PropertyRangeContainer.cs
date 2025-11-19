using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Editor;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class PropertyRangeContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "PropertyRange";

        protected override string GetIntroduction() => "和 Unity 的 Range 类似，但是 PropertyRange 可以应用到属性";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "double",
                    ParameterName = "min",
                    ParameterDescription = "最小值，还有类似参数 minGetter，" + DescriptionConfigs.SupportAllResolver
                },
                new ParameterValue
                {
                    ReturnType = "double",
                    ParameterName = "max",
                    ParameterDescription = "最大值，还有类似参数 maxGetter，" + DescriptionConfigs.SupportAllResolver
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(PropertyRangeExample));
    }
}
