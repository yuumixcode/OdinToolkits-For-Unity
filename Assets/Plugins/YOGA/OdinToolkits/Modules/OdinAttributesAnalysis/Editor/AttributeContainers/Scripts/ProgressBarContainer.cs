using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;
using System.Collections.Generic;
using UnityEngine;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class ProgressBarContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "ProgressBar";
        }

        protected override string SetBrief()
        {
            return "将值以进度条的方式显示";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "可以根据值进行颜色的变换"
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>
            {
                new()
                {
                    returnType = "double",
                    paramName = "min",
                    paramDescription = "最小值，还有一个类似的参数 minGetter，" + DescriptionConfigs.SupportAllResolver
                },
                new()
                {
                    returnType = "double",
                    paramName = "max",
                    paramDescription = "最大值，还有一个类似的参数 maxGetter，" + DescriptionConfigs.SupportAllResolver
                },
                new()
                {
                    returnType = "float",
                    paramName = "r",
                    paramDescription = "RGB 的红色通道"
                },
                new()
                {
                    returnType = "float",
                    paramName = "g",
                    paramDescription = "RGB 的绿色通道"
                },
                new()
                {
                    returnType = "float",
                    paramName = "b",
                    paramDescription = "RGB 的蓝色通道"
                },
                new()
                {
                    returnType = "int",
                    paramName = "Height",
                    paramDescription = "进度条高度"
                },
                new()
                {
                    returnType = "string",
                    paramName = "ColorGetter",
                    paramDescription = "颜色获取器，返回值类型为 Color，" + DescriptionConfigs.ColorDescription
                },
                new()
                {
                    returnType = "string",
                    paramName = "BackgroundColorGetter",
                    paramDescription = "背景颜色获取器，返回值类型为 Color，" + DescriptionConfigs.ColorDescription
                },
                new()
                {
                    returnType = "bool",
                    paramName = "Segmented",
                    paramDescription = "是否像瓦片一样分块显示"
                },
                new()
                {
                    returnType = "string",
                    paramName = "CustomValueStringGetter",
                    paramDescription = "自定义 ValueLabel，返回值类型为 string，" + DescriptionConfigs.SupportAllResolver
                },
                new()
                {
                    returnType = "bool",
                    paramName = "DrawValueLabel",
                    paramDescription = "是否绘制 ValueLabel"
                },
                new()
                {
                    returnType = nameof(TextAlignment),
                    paramName = "ValueLabelAlignment",
                    paramDescription = "对齐样式"
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(ProgressBarExample));
        }
    }
}