using System.Collections.Generic;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class LabelTextContainer : AbsContainer
    {
        protected override string SetHeader() => "LabelText";

        protected override string SetBrief() => "自定义 Property 的 Label 名称字符串";

        protected override List<string> SetTip() => new List<string>();

        protected override List<ParamValue> SetParamValues() =>
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

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(LabelTextExample));
    }
}
