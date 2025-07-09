using Sirenix.OdinInspector;
using System.Collections.Generic;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class TitleGroupContainer : AbsContainer
    {
        protected override string SetHeader() => "TitleGroup";

        protected override string SetBrief() => "将任意 Property 以标题为核心进行分组绘制";

        protected override List<string> SetTip() => new List<string>();

        protected override List<ParamValue> SetParamValues() =>
            new List<ParamValue>
            {
                new ParamValue
                {
                    returnType = "string",
                    paramName = "title",
                    paramDescription = "标题名，同时也是 Group 路径"
                },
                new ParamValue
                {
                    returnType = "string",
                    paramName = "subtitle",
                    paramDescription = "子标题"
                },
                new ParamValue
                {
                    returnType = nameof(TitleAlignments),
                    paramName = "alignment",
                    paramDescription = "对齐方式，和 Title 一样"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "horizontalLine",
                    paramDescription = "是否绘制水平线"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "boldTitle",
                    paramDescription = "是否粗体绘制"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "indent",
                    paramDescription = "是否缩进所有组成员，默认为 false"
                },
                new ParamValue
                {
                    returnType = "float",
                    paramName = "order",
                    paramDescription = "和其他 Group 的排序，默认为 0"
                }
            };

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(TitleGroupExample));
    }
}
