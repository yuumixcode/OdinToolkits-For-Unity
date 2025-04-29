using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;
using System.Collections.Generic;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class MinMaxSliderContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "MinMaxSlider";
        }

        protected override string SetBrief()
        {
            return "把 Vector2 以滑动条的样式绘制";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "左边的值代表 Vector2 的 X，右边的值代表 Vector2 的 Y，可以设置一个范围，但是限制了 X 必须小于等于 Y",
                "实际运用中，把 MinMaxSlider 对应的 Vector2 变量作为一个范围界定值，而不是直接参与运算的值"
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>
            {
                new()
                {
                    returnType = "float",
                    paramName = "minValue",
                    paramDescription = "最小值，" + DescriptionConfigs.SupportAllResolver
                },
                new()
                {
                    returnType = "float",
                    paramName = "maxValue",
                    paramDescription = "最大值，" + DescriptionConfigs.SupportAllResolver
                },
                new()
                {
                    returnType = "bool",
                    paramName = "showFields",
                    paramDescription = "如果为 true，将会绘制值的范围数值"
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(MinMaxSliderExample));
        }
    }
}