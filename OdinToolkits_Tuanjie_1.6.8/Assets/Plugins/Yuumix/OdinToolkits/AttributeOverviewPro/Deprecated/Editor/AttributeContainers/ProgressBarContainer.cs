using System.Collections.Generic;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class ProgressBarContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "ProgressBar";

        protected override string GetIntroduction() => "将值以进度条的方式显示";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "可以根据值进行颜色的变换"
            };

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "double",
                    ParameterName = "min",
                    ParameterDescription = "最小值，还有一个类似的参数 minGetter，" + DescriptionConfigs.SupportAllResolver
                },
                new ParameterValue
                {
                    ReturnType = "double",
                    ParameterName = "max",
                    ParameterDescription = "最大值，还有一个类似的参数 maxGetter，" + DescriptionConfigs.SupportAllResolver
                },
                new ParameterValue
                {
                    ReturnType = "float",
                    ParameterName = "r",
                    ParameterDescription = "RGB 的红色通道"
                },
                new ParameterValue
                {
                    ReturnType = "float",
                    ParameterName = "g",
                    ParameterDescription = "RGB 的绿色通道"
                },
                new ParameterValue
                {
                    ReturnType = "float",
                    ParameterName = "b",
                    ParameterDescription = "RGB 的蓝色通道"
                },
                new ParameterValue
                {
                    ReturnType = "int",
                    ParameterName = "Height",
                    ParameterDescription = "进度条高度"
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "ColorGetter",
                    ParameterDescription = "颜色获取器，返回值类型为 Color，" + DescriptionConfigs.ColorDescription
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "BackgroundColorGetter",
                    ParameterDescription = "背景颜色获取器，返回值类型为 Color，" + DescriptionConfigs.ColorDescription
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "Segmented",
                    ParameterDescription = "是否像瓦片一样分块显示"
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "CustomValueStringGetter",
                    ParameterDescription =
                        "自定义 ValueLabel，返回值类型为 string，" + DescriptionConfigs.SupportAllResolver
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "DrawValueLabel",
                    ParameterDescription = "是否绘制 ValueLabel"
                },
                new ParameterValue
                {
                    ReturnType = nameof(TextAlignment),
                    ParameterName = "ValueLabelAlignment",
                    ParameterDescription = "对齐样式"
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(ProgressBarExample));
    }
}
