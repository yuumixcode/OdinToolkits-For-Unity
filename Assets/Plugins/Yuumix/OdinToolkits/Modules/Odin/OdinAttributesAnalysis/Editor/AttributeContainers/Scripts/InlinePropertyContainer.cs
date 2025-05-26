using System.Collections.Generic;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class InlinePropertyContainer : AbsContainer
    {
        protected override string SetHeader() => "InlineProperty";

        protected override string SetBrief() => "将通常需要折叠的 Property 重新绘制在一行中";

        protected override List<string> SetTip() =>
            new List<string>
            {
                "参数设置的是自定义类或者结构体中的子 properties 的宽度"
            };

        protected override List<ParamValue> SetParamValues() =>
            new List<ParamValue>
            {
                new ParamValue
                {
                    returnType = "int",
                    paramName = "LabelWidth",
                    paramDescription = "所有子 properties 的宽度"
                }
            };

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(InlinePropertyExample));
    }
}
