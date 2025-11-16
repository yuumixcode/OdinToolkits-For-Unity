using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class ButtonContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "Button";

        protected override string GetIntroduction() => "将方法绘制成一个按钮，包括有参数的方法";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParamValue> GetParamValues() =>
            new List<ParamValue>
            {
                new ParamValue
                {
                    returnType = "string",
                    paramName = "name",
                    paramDescription = "自定义按钮显示的名称，" + DescriptionConfigs.SupportAllResolver
                },
                new ParamValue
                {
                    returnType = "ButtonSizes",
                    paramName = "buttonSize",
                    paramDescription = "按钮大小的枚举，有 Small，Medium，Large，Gigantic"
                },
                new ParamValue
                {
                    returnType = "int",
                    paramName = "buttonSize",
                    paramDescription = "按钮的高度，会自动控制按钮的宽度"
                },
                new ParamValue
                {
                    returnType = "ButtonStyle",
                    paramName = "parameterBtnStyle",
                    paramDescription = "按钮的样式枚举，有 CompactBox，FoldoutButton，Box"
                },
                new ParamValue
                {
                    returnType = "SdfIconType",
                    paramName = "icon",
                    paramDescription = "Odin 提供的图标枚举，有非常多的图标可以使用"
                },
                new ParamValue
                {
                    returnType = "IconAlignment",
                    paramName = "iconAlignment",
                    paramDescription = "图标的对齐方式，有 LeftOfText，RightOfText，LeftEdge，RightEdge"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "Stretch",
                    paramDescription = "是否拉伸宽度，默认为 true，否则将依据名称的长度绘制按钮"
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(ButtonExample));
    }
}
