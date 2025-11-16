using System.Collections.Generic;

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

        protected override List<ParamValue> GetParamValues() =>
            new List<ParamValue>
            {
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "disableForMember",
                    paramDescription = "是否禁用成员菜单"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "disableCollectionElements",
                    paramDescription = "是否禁用集合元素菜单"
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(DisableContextMenuExample));
    }
}
