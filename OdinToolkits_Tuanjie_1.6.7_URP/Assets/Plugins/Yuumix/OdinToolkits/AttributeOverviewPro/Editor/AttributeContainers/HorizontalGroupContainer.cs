using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Modules.Editor
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

        protected override List<ParamValue> GetParamValues() =>
            new List<ParamValue>
            {
                new ParamValue
                {
                    returnType = "string",
                    paramName = "group",
                    paramDescription = "分组路径，默认为 _DefaultHorizontalGroup"
                },
                new ParamValue
                {
                    returnType = "float",
                    paramName = "width",
                    paramDescription = "宽度，默认为自动控制，如果 0 ~ 1 则表示百分比，否则表示像素"
                },
                new ParamValue
                {
                    returnType = "int",
                    paramName = "marginLeft",
                    paramDescription = "左边空白边界，单位是像素"
                },
                new ParamValue
                {
                    returnType = "int",
                    paramName = "marginRight",
                    paramDescription = "右边空白边界，单位是像素"
                },
                new ParamValue
                {
                    returnType = "float",
                    paramName = "order",
                    paramDescription = "不同的 Group 在 Inspector 面板中的优先级"
                },
                new ParamValue
                {
                    returnType = "float",
                    paramName = "MarginLeft",
                    paramDescription = "左边空白边界，和 marginLeft 类似，大写开头的表示变量，0 ~ 1 则表示百分比，否则表示像素"
                },
                new ParamValue
                {
                    returnType = "float",
                    paramName = "MarginRight",
                    paramDescription = "右边空白边界，和 marginRight 类似，大写开头的表示变量，0 ~ 1 则表示百分比，否则表示像素"
                },

                new ParamValue
                {
                    returnType = "float",
                    paramName = "PaddingLeft",
                    paramDescription = "左侧内边距，0 ~ 1 则表示百分比，否则表示像素"
                },
                new ParamValue
                {
                    returnType = "float",
                    paramName = "PaddingRight",
                    paramDescription = "右侧内边距，0 ~ 1 则表示百分比，否则表示像素"
                },
                new ParamValue
                {
                    returnType = "float",
                    paramName = "MinWidth",
                    paramDescription = "最小宽度，0 ~ 1 则表示百分比，否则表示像素"
                },
                new ParamValue
                {
                    returnType = "float",
                    paramName = "MaxWidth",
                    paramDescription = "最大宽度，0 ~ 1 则表示百分比，否则表示像素"
                },
                new ParamValue
                {
                    returnType = "float",
                    paramName = "Gap",
                    paramDescription = "不同列之间的宽度，0 ~ 1 则表示百分比，否则表示像素"
                },
                new ParamValue
                {
                    returnType = "string",
                    paramName = "Title",
                    paramDescription = "设置一个标题"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "DisableAutomaticLabelWidth",
                    paramDescription = "关闭自动设置宽度，默认为 false"
                },
                new ParamValue
                {
                    returnType = "LabelWidth",
                    paramName = "LabelWidth",
                    paramDescription = "设置标签宽度"
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(HorizontalGroupExample));
    }
}
