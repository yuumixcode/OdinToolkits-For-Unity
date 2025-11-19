using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Editor;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
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

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "action",
                    ParameterDescription = "按钮点击时调用的方法名，会自动生成按钮，" + DescriptionConfigs.SupportAllResolver
                },
                new ParameterValue
                {
                    ReturnType = "SdfIconType",
                    ParameterName = "icon",
                    ParameterDescription = "按钮图标，Odin 提供的图标枚举"
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "label",
                    ParameterDescription = "自定义按钮文本，默认为方法名"
                },
                new ParameterValue
                {
                    ReturnType = "IconAlignment",
                    ParameterName = "iconAlignment",
                    ParameterDescription = "图标对齐方式"
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "ShowIf",
                    ParameterDescription = "是否显示按钮，" + DescriptionConfigs.SupportAllResolver
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "ButtonColor",
                    ParameterDescription = DescriptionConfigs.ColorDescription
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "TextColor",
                    ParameterDescription = DescriptionConfigs.ColorDescription
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(InlineButtonExample));
    }
}
