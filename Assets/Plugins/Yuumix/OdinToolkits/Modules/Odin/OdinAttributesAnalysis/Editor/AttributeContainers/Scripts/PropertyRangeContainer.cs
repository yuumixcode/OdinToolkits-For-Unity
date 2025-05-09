using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;
using System.Collections.Generic;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class PropertyRangeContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "PropertyRange";
        }

        protected override string SetBrief()
        {
            return "和 Unity 的 Range 类似，但是 PropertyRange 可以应用到属性";
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
                    paramName = "min",
                    paramDescription = "最小值，还有类似参数 minGetter，" + DescriptionConfigs.SupportAllResolver
                },
                new()
                {
                    returnType = "double",
                    paramName = "max",
                    paramDescription = "最大值，还有类似参数 maxGetter，" + DescriptionConfigs.SupportAllResolver
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(PropertyRangeExample));
        }
    }
}