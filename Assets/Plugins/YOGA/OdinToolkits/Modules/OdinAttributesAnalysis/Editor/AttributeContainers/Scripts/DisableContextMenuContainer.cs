using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class DisableContextMenuContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "DisableContextMenu";
        }

        protected override string SetBrief()
        {
            return "关闭 ContextMenu 菜单";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "默认只关闭标记的 Property 的右键菜单"
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>
            {
                new()
                {
                    returnType = "bool",
                    paramName = "disableForMember",
                    paramDescription = "是否禁用成员菜单"
                },
                new()
                {
                    returnType = "bool",
                    paramName = "disableCollectionElements",
                    paramDescription = "是否禁用集合元素菜单"
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(DisableContextMenuExample));
        }
    }
}