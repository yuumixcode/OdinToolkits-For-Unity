using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class LabelTextContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "LabelText";

        protected override string GetIntroduction() => "自定义 Property 的 Label 名称字符串";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParamValue> GetParamValues() =>
            new List<ParamValue>
            {
                new ParamValue
                {
                    returnType = "string",
                    paramName = "text",
                    paramDescription = "自定义的名称字符串，" + DescriptionConfigs.SupportAllResolver
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "nicifyText",
                    paramDescription = "是否首字母大写优化，默认为 false，例如 m_someField 自动变成 Some Field"
                },
                new ParamValue
                {
                    returnType = "SdfIconType",
                    paramName = "icon",
                    paramDescription = "图标类型，默认为 SdfIconType.None"
                },
                new ParamValue
                {
                    returnType = "string",
                    paramName = "IconColor",
                    paramDescription = DescriptionConfigs.ColorDescription
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(LabelTextExample));
    }
}
