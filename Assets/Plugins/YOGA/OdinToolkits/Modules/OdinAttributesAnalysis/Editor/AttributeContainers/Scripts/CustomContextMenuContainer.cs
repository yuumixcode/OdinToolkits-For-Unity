using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;
using System.Collections.Generic;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class CustomContextMenuContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "CustomContextMenu";
        }

        protected override string SetBrief()
        {
            return "自定义 Property 的右键菜单";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "Odin Inspector 会默认覆盖 Unity 的 Context 菜单，如果使用 Unity 的 [ContextMenuItem] 特性，需要标记 [DrawWithUnity]"
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>
            {
                new()
                {
                    returnType = "string",
                    paramName = "menuItem",
                    paramDescription = "右键菜单名称，也表示菜单路径"
                },
                new()
                {
                    returnType = "string",
                    paramName = "action",
                    paramDescription = "右键菜单点击事件，" + DescriptionConfigs.SupportMemberResolverLite
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(CustomContextMenuExample));
        }
    }
}