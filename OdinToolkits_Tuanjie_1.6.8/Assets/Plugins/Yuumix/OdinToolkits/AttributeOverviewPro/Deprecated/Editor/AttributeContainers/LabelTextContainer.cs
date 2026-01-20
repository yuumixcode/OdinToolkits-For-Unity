using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class LabelTextContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "LabelText";

        protected override string GetIntroduction() => "自定义 Property 的 Label 名称字符串";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "text",
                    ParameterDescription = "自定义的名称字符串，" + DescriptionConfigs.SupportAllResolver
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "nicifyText",
                    ParameterDescription = "是否首字母大写优化，默认为 false，例如 m_someField 自动变成 Some Field"
                },
                new ParameterValue
                {
                    ReturnType = "SdfIconType",
                    ParameterName = "icon",
                    ParameterDescription = "图标类型，默认为 SdfIconType.None"
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "IconColor",
                    ParameterDescription = DescriptionConfigs.ColorDescription
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(LabelTextExample));
    }
}
