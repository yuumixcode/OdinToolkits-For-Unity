using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;
using System.Collections.Generic;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class WrapContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "Wrap";
        }

        protected override string SetBrief()
        {
            return "可以对大部分的基础变量使用，让它在达到某个值时，开始循环";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
                { "对角度值可以使用" };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>
            {
                new()
                {
                    returnType = "double",
                    paramName = "min",
                    paramDescription = "最小值" + DescriptionConfigs.SupportAllResolver
                },
                new()
                {
                    returnType = "double",
                    paramName = "max",
                    paramDescription = "最大值" + DescriptionConfigs.SupportAllResolver
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(WrapExample));
        }
    }
}