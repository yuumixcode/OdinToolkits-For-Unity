using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Editor;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class ToggleContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "Toggle";

        protected override string GetIntroduction() => "将一个自定义类的 Property 通过一个 bool 类型的值控制是否可以获取焦点";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "通常是一个自定义类内部有一个 bool 类型的字段，然后引用该字段",
                "也可以直接作用与一个类上，让该类默认变为 Toggle 样式"
            };

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "ToggleMemberName",
                    ParameterDescription = "成员名引用"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "CollapseOthersOnExpand",
                    ParameterDescription = "展开时是否折叠其他 Toggle，默认为 true"
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(ToggleExample));
    }
}
