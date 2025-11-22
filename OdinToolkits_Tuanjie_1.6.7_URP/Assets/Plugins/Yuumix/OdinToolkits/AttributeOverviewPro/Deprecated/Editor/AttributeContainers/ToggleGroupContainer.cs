using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class ToggleGroupContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "ToggleGroup";

        protected override string GetIntroduction() =>
            "将任意 Property 以类似 FoldoutGroup 可折叠的形式分组绘制，同时有一个 Toggle 值可以控制 Enable 或者 Disable";

        protected override List<string> GetTips() =>
            new List<string>
                { "bool 类型控制变量要包含在 ToggleGroup 内部" };

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "toggleMemberName",
                    ParameterDescription = "bool 类型的成员名，用于控制开启焦点获取和关闭，" + DescriptionConfigs.SupportAllResolver
                },
                new ParameterValue
                {
                    ReturnType = "float",
                    ParameterName = "order",
                    ParameterDescription = "不同 Group 之间排序，值越大越靠后，默认为 0"
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "groupTitle",
                    ParameterDescription = "分组标题以及路径"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "CollapseOthersOnExpand",
                    ParameterDescription = "是否展开时关闭其他分组，默认为 true"
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(ToggleGroupExample));
    }
}
