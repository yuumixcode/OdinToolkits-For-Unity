using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;
using System.Collections.Generic;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class MaxValueContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "MaxValue";
        }

        protected override string SetBrief()
        {
            return "MaxValue 用于基本字段，使用它来定义字段的最大值";
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
                    returnType = "double",
                    paramName = "maxValue",
                    paramDescription = "字段的最大值"
                },
                new()
                {
                    returnType = "string",
                    paramName = "expression",
                    paramDescription = DescriptionConfigs.SupportAllResolver
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(MinMaxValueExample));
        }
    }
}