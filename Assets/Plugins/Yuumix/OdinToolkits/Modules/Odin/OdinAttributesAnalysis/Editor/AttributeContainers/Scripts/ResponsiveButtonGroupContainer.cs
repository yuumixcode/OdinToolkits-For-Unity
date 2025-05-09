using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;
using System.Collections.Generic;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class ResponsiveButtonGroupContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "ResponsiveButtonGroup";
        }

        protected override string SetBrief()
        {
            return "可以自动布局的 Button";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "可以和 Button 同时使用，Button 会覆盖 ResponsiveButtonGroup 的设置"
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>
            {
                new()
                {
                    returnType = "string",
                    paramName = "group",
                    paramDescription = "Group 的名称，同时也代表路径，" + DescriptionConfigs.SupportMemberResolverLite
                },
                new()
                {
                    returnType = "ButtonSizes",
                    paramName = "DefaultButtonSize",
                    paramDescription = "默认的按钮大小，默认为 Medium"
                },
                new()
                {
                    returnType = "bool",
                    paramName = "UniformLayout",
                    paramDescription = "如果为真，那么一行按钮的宽度将是相同的"
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(ResponsiveButtonGroupExample));
        }
    }
}