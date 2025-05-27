using System.Collections.Generic;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class ButtonGroupContainer : AbsContainer
    {
        protected override string SetHeader() => "ButtonGroup";

        protected override string SetBrief() => "将实例(非静态)方法绘制成按钮，并横向分组";

        protected override List<string> SetTip() =>
            new List<string>
            {
                "ButtonGroup 可以和 Button 结合使用，Button 对于按钮的设置将覆盖 ButtonGroup"
            };

        protected override List<ParamValue> SetParamValues() =>
            new List<ParamValue>
            {
                new ParamValue
                {
                    returnType = "string",
                    paramName = "group",
                    paramDescription = "分组名，默认为 _DefaultGroup，" + DescriptionConfigs.SupportMemberResolverLite
                },
                new ParamValue
                {
                    returnType = "float",
                    paramName = "order",
                    paramDescription = "排序，越小越靠左，默认为 0"
                },
                new ParamValue
                {
                    returnType = "int",
                    paramName = "ButtonHeight",
                    paramDescription = "按钮高度"
                },
                new ParamValue
                {
                    returnType = "IconAlignment",
                    paramName = "IconAlignment",
                    paramDescription = "图标对齐方式"
                },
                new ParamValue
                {
                    returnType = "int",
                    paramName = "ButtonAlignment",
                    paramDescription = "0 为向左对齐，1 为向右对齐"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "Stretch",
                    paramDescription = "是否拉伸宽度，默认为 true"
                }
            };

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(ButtonGroupExample));
    }
}
