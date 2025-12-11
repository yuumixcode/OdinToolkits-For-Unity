using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class HorizontalGroupContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "HorizontalGroup";

        protected override string GetIntroduction() => "控制横向的布局组，可以和其他 Group 连接使用";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "Padding 和 Margin 在视觉效果上很相似，任意选择即可",
                "float 类型的变量均可支持两种表示方式，如果 0 ~ 1 则表示百分比，否则表示像素"
            };

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "group",
                    ParameterDescription = "分组路径，默认为 _DefaultHorizontalGroup"
                },
                new ParameterValue
                {
                    ReturnType = "float",
                    ParameterName = "width",
                    ParameterDescription = "宽度，默认为自动控制，如果 0 ~ 1 则表示百分比，否则表示像素"
                },
                new ParameterValue
                {
                    ReturnType = "int",
                    ParameterName = "marginLeft",
                    ParameterDescription = "左边空白边界，单位是像素"
                },
                new ParameterValue
                {
                    ReturnType = "int",
                    ParameterName = "marginRight",
                    ParameterDescription = "右边空白边界，单位是像素"
                },
                new ParameterValue
                {
                    ReturnType = "float",
                    ParameterName = "order",
                    ParameterDescription = "不同的 Group 在 Inspector 面板中的优先级"
                },
                new ParameterValue
                {
                    ReturnType = "float",
                    ParameterName = "MarginLeft",
                    ParameterDescription = "左边空白边界，和 marginLeft 类似，大写开头的表示变量，0 ~ 1 则表示百分比，否则表示像素"
                },
                new ParameterValue
                {
                    ReturnType = "float",
                    ParameterName = "MarginRight",
                    ParameterDescription = "右边空白边界，和 marginRight 类似，大写开头的表示变量，0 ~ 1 则表示百分比，否则表示像素"
                },

                new ParameterValue
                {
                    ReturnType = "float",
                    ParameterName = "PaddingLeft",
                    ParameterDescription = "左侧内边距，0 ~ 1 则表示百分比，否则表示像素"
                },
                new ParameterValue
                {
                    ReturnType = "float",
                    ParameterName = "PaddingRight",
                    ParameterDescription = "右侧内边距，0 ~ 1 则表示百分比，否则表示像素"
                },
                new ParameterValue
                {
                    ReturnType = "float",
                    ParameterName = "MinWidth",
                    ParameterDescription = "最小宽度，0 ~ 1 则表示百分比，否则表示像素"
                },
                new ParameterValue
                {
                    ReturnType = "float",
                    ParameterName = "MaxWidth",
                    ParameterDescription = "最大宽度，0 ~ 1 则表示百分比，否则表示像素"
                },
                new ParameterValue
                {
                    ReturnType = "float",
                    ParameterName = "Gap",
                    ParameterDescription = "不同列之间的宽度，0 ~ 1 则表示百分比，否则表示像素"
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "Title",
                    ParameterDescription = "设置一个标题"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "DisableAutomaticLabelWidth",
                    ParameterDescription = "关闭自动设置宽度，默认为 false"
                },
                new ParameterValue
                {
                    ReturnType = "LabelWidth",
                    ParameterName = "LabelWidth",
                    ParameterDescription = "设置标签宽度"
                }
            };

        protected override string GetOriginalCode() =>
            ReadCodeWithoutNamespace(typeof(HorizontalGroupExample));
    }
}
