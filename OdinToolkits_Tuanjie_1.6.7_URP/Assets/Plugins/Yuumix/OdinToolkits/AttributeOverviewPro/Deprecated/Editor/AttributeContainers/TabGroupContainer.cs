using Sirenix.OdinInspector;
using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class TabGroupContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "TabGroup";

        protected override string GetIntroduction() => "将任意 Property 以 Tab 的样式分组";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "和其他 Group 连接时，需要同时设置分组路径和 tab 参数，否则构造函数将使用默认的分组路径，不会将第一个参数识别为分组路径"
            };

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "group",
                    ParameterDescription = "分组名以及路径，可以和其他 Group 连接"
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "tab",
                    ParameterDescription = "分组标题"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "useFixedHeight",
                    ParameterDescription = "是否使用固定高度，默认为 false"
                },
                new ParameterValue
                {
                    ReturnType = "SdfIconType",
                    ParameterName = "icon",
                    ParameterDescription = "图标样式"
                },
                new ParameterValue
                {
                    ReturnType = "float",
                    ParameterName = "order",
                    ParameterDescription = "和其他 Group 的排序，默认为 0"
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "TabName",
                    ParameterDescription = "TabGroup 的名称"
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "TabId",
                    ParameterDescription = "默认由构造函数中的参数 tab 赋值"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "Paddingless",
                    ParameterDescription = "如果为 true，则每页内容都不会被包含在一个 Box 中，属于样式优化参数"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "HideTabGroupIfTabGroupOnlyHasOneTab",
                    ParameterDescription = "如果为 true，当只有一个 Tab 时将隐藏 Tab 样式"
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "TextColor",
                    ParameterDescription = DescriptionConfigs.ColorDescription
                },
                new ParameterValue
                {
                    ReturnType = nameof(TabLayouting),
                    ParameterName = "TabLayouting",
                    ParameterDescription = "Tab 的布局方式，包含 MultiRow 和 Shrink"
                },
                new ParameterValue
                {
                    ReturnType = "List<TabGroupAttribute>",
                    ParameterName = "Tabs",
                    ParameterDescription = "这个组中所有的 Tabs "
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(TabGroupExample));
    }
}
