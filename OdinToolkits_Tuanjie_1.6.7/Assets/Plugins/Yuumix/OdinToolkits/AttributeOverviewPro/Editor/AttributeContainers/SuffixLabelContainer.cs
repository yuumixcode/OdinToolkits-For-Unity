using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class SuffixLabelContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "SuffixLabel";

        protected override string GetIntroduction() => "在一个 Property 后面加一个后缀字符串，用于补充说明";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParamValue> GetParamValues() =>
            new List<ParamValue>
            {
                new ParamValue
                {
                    returnType = "string",
                    paramName = "Label",
                    paramDescription = "后缀字符串，" + DescriptionConfigs.SupportAllResolver
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "Overlay",
                    paramDescription = "是否覆盖原属性，默认为 false，绘制在 Property 之后"
                },
                new ParamValue
                {
                    returnType = "SdfIconType",
                    paramName = "Icon",
                    paramDescription = "后缀图标，默认为 SdfIconType.None"
                },
                new ParamValue
                {
                    returnType = "Color",
                    paramName = "IconColor",
                    paramDescription = DescriptionConfigs.ColorDescription
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(SuffixLabelExample));
    }
}
