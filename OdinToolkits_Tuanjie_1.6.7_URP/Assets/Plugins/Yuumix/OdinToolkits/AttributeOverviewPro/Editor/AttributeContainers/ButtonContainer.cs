using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Editor;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class ButtonContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "Button";

        protected override string GetIntroduction() => "将方法绘制成一个按钮，包括有参数的方法";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "name",
                    ParameterDescription = "自定义按钮显示的名称，" + DescriptionConfigs.SupportAllResolver
                },
                new ParameterValue
                {
                    ReturnType = "ButtonSizes",
                    ParameterName = "buttonSize",
                    ParameterDescription = "按钮大小的枚举，有 Small，Medium，Large，Gigantic"
                },
                new ParameterValue
                {
                    ReturnType = "int",
                    ParameterName = "buttonSize",
                    ParameterDescription = "按钮的高度，会自动控制按钮的宽度"
                },
                new ParameterValue
                {
                    ReturnType = "ButtonStyle",
                    ParameterName = "parameterBtnStyle",
                    ParameterDescription = "按钮的样式枚举，有 CompactBox，FoldoutButton，Box"
                },
                new ParameterValue
                {
                    ReturnType = "SdfIconType",
                    ParameterName = "icon",
                    ParameterDescription = "Odin 提供的图标枚举，有非常多的图标可以使用"
                },
                new ParameterValue
                {
                    ReturnType = "IconAlignment",
                    ParameterName = "iconAlignment",
                    ParameterDescription = "图标的对齐方式，有 LeftOfText，RightOfText，LeftEdge，RightEdge"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "Stretch",
                    ParameterDescription = "是否拉伸宽度，默认为 true，否则将依据名称的长度绘制按钮"
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(ButtonExample));
    }
}
