using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Editor;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class CustomContextMenuContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "CustomContextMenu";

        protected override string GetIntroduction() => "自定义 Property 的右键菜单";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "Odin Inspector 会默认覆盖 Unity 的 Context 菜单，如果使用 Unity 的 [ContextMenuItem] 特性，需要标记 [DrawWithUnity]"
            };

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "menuItem",
                    ParameterDescription = "右键菜单名称，也表示菜单路径"
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "action",
                    ParameterDescription = "右键菜单点击事件，" + DescriptionConfigs.SupportMemberResolverLite
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(CustomContextMenuExample));
    }
}
