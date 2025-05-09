using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;
using System.Collections.Generic;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class ButtonContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "Button";
        }

        protected override string SetBrief()
        {
            return "将方法绘制成一个按钮，包括有参数的方法";
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
                    paramName = "name",
                    paramDescription = "自定义按钮显示的名称，" + DescriptionConfigs.SupportAllResolver
                },
                new()
                {
                    returnType = "ButtonSizes",
                    paramName = "buttonSize",
                    paramDescription = "按钮大小的枚举，有 Small，Medium，Large，Gigantic"
                },
                new()
                {
                    returnType = "int",
                    paramName = "buttonSize",
                    paramDescription = "按钮的高度，会自动控制按钮的宽度"
                },
                new()
                {
                    returnType = "ButtonStyle",
                    paramName = "parameterBtnStyle",
                    paramDescription = "按钮的样式枚举，有 CompactBox，FoldoutButton，Box"
                },
                new()
                {
                    returnType = "SdfIconType",
                    paramName = "icon",
                    paramDescription = "Odin 提供的图标枚举，有非常多的图标可以使用"
                },
                new()
                {
                    returnType = "IconAlignment",
                    paramName = "iconAlignment",
                    paramDescription = "图标的对齐方式，有 LeftOfText，RightOfText，LeftEdge，RightEdge"
                },
                new()
                {
                    returnType = "bool",
                    paramName = "Stretch",
                    paramDescription = "是否拉伸宽度，默认为 true，否则将依据名称的长度绘制按钮"
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(ButtonExample));
        }
    }
}