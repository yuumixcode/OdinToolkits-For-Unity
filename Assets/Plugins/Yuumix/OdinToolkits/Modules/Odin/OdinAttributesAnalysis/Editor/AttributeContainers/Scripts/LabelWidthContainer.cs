using System.Collections.Generic;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class LabelWidthContainer : AbsContainer
    {
        protected override string SetHeader() => "LabelWidth";

        protected override string SetBrief() => "Property 名称字符串的宽度";

        protected override List<string> SetTip() => new List<string>();

        protected override List<ParamValue> SetParamValues() =>
            new List<ParamValue>
            {
                new ParamValue
                {
                    returnType = "float",
                    paramName = "width",
                    paramDescription = "宽度值，单位为像素"
                }
            };

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(LabelWidthExample));
    }
}
