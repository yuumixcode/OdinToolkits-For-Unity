using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class ButtonGroupContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "ButtonGroup";

        protected override string GetIntroduction() => "将实例(非静态)方法绘制成按钮，并横向分组";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "ButtonGroup 可以和 Button 结合使用，Button 对于按钮的设置将覆盖 ButtonGroup"
            };

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "group",
                    ParameterDescription =
                        "分组名，默认为 _DefaultGroup，" + DescriptionConfigs.SupportMemberResolverLite
                },
                new ParameterValue
                {
                    ReturnType = "float",
                    ParameterName = "order",
                    ParameterDescription = "排序，越小越靠左，默认为 0"
                },
                new ParameterValue
                {
                    ReturnType = "int",
                    ParameterName = "ButtonHeight",
                    ParameterDescription = "按钮高度"
                },
                new ParameterValue
                {
                    ReturnType = "IconAlignment",
                    ParameterName = "IconAlignment",
                    ParameterDescription = "图标对齐方式"
                },
                new ParameterValue
                {
                    ReturnType = "int",
                    ParameterName = "ButtonAlignment",
                    ParameterDescription = "0 为向左对齐，1 为向右对齐"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "Stretch",
                    ParameterDescription = "是否拉伸宽度，默认为 true"
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(ButtonGroupExample));
    }
}
