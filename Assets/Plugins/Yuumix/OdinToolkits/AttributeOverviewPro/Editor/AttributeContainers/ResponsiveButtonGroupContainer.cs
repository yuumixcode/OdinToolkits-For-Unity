using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Modules.Editor
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

        protected override List<ParamValue> GetParamValues() =>
            new List<ParamValue>
            {
                new ParamValue
                {
                    returnType = "string",
                    paramName = "group",
                    paramDescription = "Group 的名称，同时也代表路径，" + DescriptionConfigs.SupportMemberResolverLite
                },
                new ParamValue
                {
                    returnType = "ButtonSizes",
                    paramName = "DefaultButtonSize",
                    paramDescription = "默认的按钮大小，默认为 Medium"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "UniformLayout",
                    paramDescription = "如果为真，那么一行按钮的宽度将是相同的"
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(ResponsiveButtonGroupExample));
    }
}
