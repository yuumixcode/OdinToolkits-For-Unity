using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;
using System.Collections.Generic;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class IndentContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "Indent";
        }

        protected override string SetBrief()
        {
            return "主动设置 Property 的缩进，可以为负数";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "主要用于样式不符合预期的修补，通常不需要手动控制缩进"
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>
            {
                new()
                {
                    returnType = "int",
                    paramName = "indentLevel",
                    paramDescription = "缩进值，可以为负数，默认为 1"
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(IndentExample));
        }
    }
}