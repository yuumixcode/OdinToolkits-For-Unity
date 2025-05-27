using System.Collections.Generic;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class IndentContainer : AbsContainer
    {
        protected override string SetHeader() => "Indent";

        protected override string SetBrief() => "主动设置 Property 的缩进，可以为负数";

        protected override List<string> SetTip() =>
            new List<string>
            {
                "主要用于样式不符合预期的修补，通常不需要手动控制缩进"
            };

        protected override List<ParamValue> SetParamValues() =>
            new List<ParamValue>
            {
                new ParamValue
                {
                    returnType = "int",
                    paramName = "indentLevel",
                    paramDescription = "缩进值，可以为负数，默认为 1"
                }
            };

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(IndentExample));
    }
}
