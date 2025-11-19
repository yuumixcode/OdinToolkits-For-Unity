using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Editor;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class DisableContextMenuContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "DisableContextMenu";

        protected override string GetIntroduction() => "关闭 ContextMenu 菜单";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "默认只关闭标记的 Property 的右键菜单"
            };

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "disableForMember",
                    ParameterDescription = "是否禁用成员菜单"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "disableCollectionElements",
                    ParameterDescription = "是否禁用集合元素菜单"
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(DisableContextMenuExample));
    }
}
