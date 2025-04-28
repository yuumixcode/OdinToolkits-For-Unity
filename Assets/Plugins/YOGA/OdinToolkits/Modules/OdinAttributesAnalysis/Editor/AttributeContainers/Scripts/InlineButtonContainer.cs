using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class InlineButtonContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "InlineButton";
        }

        protected override string SetBrief()
        {
            return "在一行的结尾绘制按钮";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "可以同时有多个 InlineButton"
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>
            {
                new()
                {
                    returnType = "string",
                    paramName = "action",
                    paramDescription = "按钮点击时调用的方法名，会自动生成按钮，" + DescriptionConfigs.SupportAllResolver
                },
                new()
                {
                    returnType = "SdfIconType",
                    paramName = "icon",
                    paramDescription = "按钮图标，Odin 提供的图标枚举"
                },
                new()
                {
                    returnType = "string",
                    paramName = "label",
                    paramDescription = "自定义按钮文本，默认为方法名"
                },
                new()
                {
                    returnType = "IconAlignment",
                    paramName = "iconAlignment",
                    paramDescription = "图标对齐方式"
                },
                new()
                {
                    returnType = "string",
                    paramName = "ShowIf",
                    paramDescription = "是否显示按钮，" + DescriptionConfigs.SupportAllResolver
                },
                new()
                {
                    returnType = "string",
                    paramName = "ButtonColor",
                    paramDescription = DescriptionConfigs.ColorDescription
                },
                new()
                {
                    returnType = "string",
                    paramName = "TextColor",
                    paramDescription = DescriptionConfigs.ColorDescription
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(InlineButtonExample));
        }
    }
}