using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class SuffixLabelContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "SuffixLabel";

        protected override string GetIntroduction() => "在一个 Property 后面加一个后缀字符串，用于补充说明";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "Label",
                    ParameterDescription = "后缀字符串，" + DescriptionConfigs.SupportAllResolver
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "Overlay",
                    ParameterDescription = "是否覆盖原属性，默认为 false，绘制在 Property 之后"
                },
                new ParameterValue
                {
                    ReturnType = "SdfIconType",
                    ParameterName = "Icon",
                    ParameterDescription = "后缀图标，默认为 SdfIconType.None"
                },
                new ParameterValue
                {
                    ReturnType = "Color",
                    ParameterName = "IconColor",
                    ParameterDescription = DescriptionConfigs.ColorDescription
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(SuffixLabelExample));
    }
}
