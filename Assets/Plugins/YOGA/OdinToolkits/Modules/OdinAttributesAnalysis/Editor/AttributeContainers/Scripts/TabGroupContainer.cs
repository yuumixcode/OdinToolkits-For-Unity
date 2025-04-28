using System.Collections.Generic;
using Sirenix.OdinInspector;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class TabGroupContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "TabGroup";
        }

        protected override string SetBrief()
        {
            return "将任意 Property 以 Tab 的样式分组";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "和其他 Group 连接时，需要同时设置分组路径和 tab 参数，否则构造函数将使用默认的分组路径，不会将第一个参数识别为分组路径"
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
                    paramDescription = "分组名以及路径，可以和其他 Group 连接"
                },
                new()
                {
                    returnType = "string",
                    paramName = "tab",
                    paramDescription = "分组标题"
                },
                new()
                {
                    returnType = "bool",
                    paramName = "useFixedHeight",
                    paramDescription = "是否使用固定高度，默认为 false"
                },
                new()
                {
                    returnType = "SdfIconType",
                    paramName = "icon",
                    paramDescription = "图标样式"
                },
                new()
                {
                    returnType = "float",
                    paramName = "order",
                    paramDescription = "和其他 Group 的排序，默认为 0"
                },
                new()
                {
                    returnType = "string",
                    paramName = "TabName",
                    paramDescription = "TabGroup 的名称"
                },
                new()
                {
                    returnType = "string",
                    paramName = "TabId",
                    paramDescription = "默认由构造函数中的参数 tab 赋值"
                },
                new()
                {
                    returnType = "bool",
                    paramName = "Paddingless",
                    paramDescription = "如果为 true，则每页内容都不会被包含在一个 Box 中，属于样式优化参数"
                },
                new()
                {
                    returnType = "bool",
                    paramName = "HideTabGroupIfTabGroupOnlyHasOneTab",
                    paramDescription = "如果为 true，当只有一个 Tab 时将隐藏 Tab 样式"
                },
                new()
                {
                    returnType = "string",
                    paramName = "TextColor",
                    paramDescription = DescriptionConfigs.ColorDescription
                },
                new()
                {
                    returnType = nameof(TabLayouting),
                    paramName = "TabLayouting",
                    paramDescription = "Tab 的布局方式，包含 MultiRow 和 Shrink"
                },
                new()
                {
                    returnType = "List<TabGroupAttribute>",
                    paramName = "Tabs",
                    paramDescription = "这个组中所有的 Tabs "
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(TabGroupExample));
        }
    }
}