using System.Collections.Generic;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.Editor
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

        protected override List<ParamValue> GetParamValues() =>
            new List<ParamValue>
            {
                new ParamValue
                {
                    returnType = "double",
                    paramName = "min",
                    paramDescription = "最小值，还有一个类似的参数 minGetter，" + DescriptionConfigs.SupportAllResolver
                },
                new ParamValue
                {
                    returnType = "double",
                    paramName = "max",
                    paramDescription = "最大值，还有一个类似的参数 maxGetter，" + DescriptionConfigs.SupportAllResolver
                },
                new ParamValue
                {
                    returnType = "float",
                    paramName = "r",
                    paramDescription = "RGB 的红色通道"
                },
                new ParamValue
                {
                    returnType = "float",
                    paramName = "g",
                    paramDescription = "RGB 的绿色通道"
                },
                new ParamValue
                {
                    returnType = "float",
                    paramName = "b",
                    paramDescription = "RGB 的蓝色通道"
                },
                new ParamValue
                {
                    returnType = "int",
                    paramName = "Height",
                    paramDescription = "进度条高度"
                },
                new ParamValue
                {
                    returnType = "string",
                    paramName = "ColorGetter",
                    paramDescription = "颜色获取器，返回值类型为 Color，" + DescriptionConfigs.ColorDescription
                },
                new ParamValue
                {
                    returnType = "string",
                    paramName = "BackgroundColorGetter",
                    paramDescription = "背景颜色获取器，返回值类型为 Color，" + DescriptionConfigs.ColorDescription
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "Segmented",
                    paramDescription = "是否像瓦片一样分块显示"
                },
                new ParamValue
                {
                    returnType = "string",
                    paramName = "CustomValueStringGetter",
                    paramDescription = "自定义 ValueLabel，返回值类型为 string，" + DescriptionConfigs.SupportAllResolver
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "DrawValueLabel",
                    paramDescription = "是否绘制 ValueLabel"
                },
                new ParamValue
                {
                    returnType = nameof(TextAlignment),
                    paramName = "ValueLabelAlignment",
                    paramDescription = "对齐样式"
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(ProgressBarExample));
    }
}
