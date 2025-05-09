using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;
using Sirenix.OdinInspector;
using System.Collections.Generic;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class TitleGroupContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "TitleGroup";
        }

        protected override string SetBrief()
        {
            return "将任意 Property 以标题为核心进行分组绘制";
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
                    paramName = "title",
                    paramDescription = "标题名，同时也是 Group 路径"
                },
                new()
                {
                    returnType = "string",
                    paramName = "subtitle",
                    paramDescription = "子标题"
                },
                new()
                {
                    returnType = nameof(TitleAlignments),
                    paramName = "alignment",
                    paramDescription = "对齐方式，和 Title 一样"
                },
                new()
                {
                    returnType = "bool",
                    paramName = "horizontalLine",
                    paramDescription = "是否绘制水平线"
                },
                new()
                {
                    returnType = "bool",
                    paramName = "boldTitle",
                    paramDescription = "是否粗体绘制"
                },
                new()
                {
                    returnType = "bool",
                    paramName = "indent",
                    paramDescription = "是否缩进所有组成员，默认为 false"
                },
                new()
                {
                    returnType = "float",
                    paramName = "order",
                    paramDescription = "和其他 Group 的排序，默认为 0"
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(TitleGroupExample));
        }
    }
}