using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class ResponsiveButtonGroupContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "ResponsiveButtonGroup";

        protected override string GetIntroduction() => "可以自动布局的 Button";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "可以和 Button 同时使用，Button 会覆盖 ResponsiveButtonGroup 的设置"
            };

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "group",
                    ParameterDescription = "Group 的名称，同时也代表路径，" + DescriptionConfigs.SupportMemberResolverLite
                },
                new ParameterValue
                {
                    ReturnType = "ButtonSizes",
                    ParameterName = "DefaultButtonSize",
                    ParameterDescription = "默认的按钮大小，默认为 Medium"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "UniformLayout",
                    ParameterDescription = "如果为真，那么一行按钮的宽度将是相同的"
                }
            };

        protected override string GetOriginalCode() =>
            ReadCodeWithoutNamespace(typeof(ResponsiveButtonGroupExample));
    }
}
