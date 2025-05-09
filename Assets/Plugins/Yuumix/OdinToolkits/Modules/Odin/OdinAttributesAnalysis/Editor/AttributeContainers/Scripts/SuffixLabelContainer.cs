using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;
using System.Collections.Generic;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class SuffixLabelContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "SuffixLabel";
        }

        protected override string SetBrief()
        {
            return "在一个 Property 后面加一个后缀字符串，用于补充说明";
        }

        protected override List<string> SetTip()
        {
            return new List<string>();
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>
            {
                new()
                {
                    returnType = "string",
                    paramName = "Label",
                    paramDescription = "后缀字符串，" + DescriptionConfigs.SupportAllResolver
                },
                new()
                {
                    returnType = "bool",
                    paramName = "Overlay",
                    paramDescription = "是否覆盖原属性，默认为 false，绘制在 Property 之后"
                },
                new()
                {
                    returnType = "SdfIconType",
                    paramName = "Icon",
                    paramDescription = "后缀图标，默认为 SdfIconType.None"
                },
                new()
                {
                    returnType = "Color",
                    paramName = "IconColor",
                    paramDescription = DescriptionConfigs.ColorDescription
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(SuffixLabelExample));
        }
    }
}