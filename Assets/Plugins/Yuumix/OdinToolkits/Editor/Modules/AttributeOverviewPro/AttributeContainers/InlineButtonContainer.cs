using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Editor
{
    public class InlineButtonContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "InlineButton";

        protected override string GetIntroduction() => "在一行的结尾绘制按钮";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "可以同时有多个 InlineButton"
            };

        protected override List<ParamValue> GetParamValues() =>
            new List<ParamValue>
            {
                new ParamValue
                {
                    returnType = "string",
                    paramName = "action",
                    paramDescription = "按钮点击时调用的方法名，会自动生成按钮，" + DescriptionConfigs.SupportAllResolver
                },
                new ParamValue
                {
                    returnType = "SdfIconType",
                    paramName = "icon",
                    paramDescription = "按钮图标，Odin 提供的图标枚举"
                },
                new ParamValue
                {
                    returnType = "string",
                    paramName = "label",
                    paramDescription = "自定义按钮文本，默认为方法名"
                },
                new ParamValue
                {
                    returnType = "IconAlignment",
                    paramName = "iconAlignment",
                    paramDescription = "图标对齐方式"
                },
                new ParamValue
                {
                    returnType = "string",
                    paramName = "ShowIf",
                    paramDescription = "是否显示按钮，" + DescriptionConfigs.SupportAllResolver
                },
                new ParamValue
                {
                    returnType = "string",
                    paramName = "ButtonColor",
                    paramDescription = DescriptionConfigs.ColorDescription
                },
                new ParamValue
                {
                    returnType = "string",
                    paramName = "TextColor",
                    paramDescription = DescriptionConfigs.ColorDescription
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(InlineButtonExample));
    }
}
