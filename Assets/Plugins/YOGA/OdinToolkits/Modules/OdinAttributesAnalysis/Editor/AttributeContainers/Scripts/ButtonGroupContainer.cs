using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;
using System.Collections.Generic;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class ButtonGroupContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "ButtonGroup";
        }

        protected override string SetBrief()
        {
            return "将实例(非静态)方法绘制成按钮，并横向分组";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "ButtonGroup 可以和 Button 结合使用，Button 对于按钮的设置将覆盖 ButtonGroup"
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
                    paramDescription = "分组名，默认为 _DefaultGroup，" + DescriptionConfigs.SupportMemberResolverLite
                },
                new()
                {
                    returnType = "float",
                    paramName = "order",
                    paramDescription = "排序，越小越靠左，默认为 0"
                },
                new()
                {
                    returnType = "int",
                    paramName = "ButtonHeight",
                    paramDescription = "按钮高度"
                },
                new()
                {
                    returnType = "IconAlignment",
                    paramName = "IconAlignment",
                    paramDescription = "图标对齐方式"
                },
                new()
                {
                    returnType = "int",
                    paramName = "ButtonAlignment",
                    paramDescription = "0 为向左对齐，1 为向右对齐"
                },
                new()
                {
                    returnType = "bool",
                    paramName = "Stretch",
                    paramDescription = "是否拉伸宽度，默认为 true"
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(ButtonGroupExample));
        }
    }
}