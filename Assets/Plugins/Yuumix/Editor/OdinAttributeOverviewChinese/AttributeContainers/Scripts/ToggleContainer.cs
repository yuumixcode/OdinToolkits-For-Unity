using System.Collections.Generic;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class ToggleContainer : AbsContainer
    {
        protected override string SetHeader() => "Toggle";

        protected override string SetBrief() => "将一个自定义类的 Property 通过一个 bool 类型的值控制是否可以获取焦点";

        protected override List<string> SetTip() =>
            new List<string>
            {
                "通常是一个自定义类内部有一个 bool 类型的字段，然后引用该字段",
                "也可以直接作用与一个类上，让该类默认变为 Toggle 样式"
            };

        protected override List<ParamValue> SetParamValues() =>
            new List<ParamValue>
            {
                new ParamValue
                {
                    returnType = "string",
                    paramName = "ToggleMemberName",
                    paramDescription = "成员名引用"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "CollapseOthersOnExpand",
                    paramDescription = "展开时是否折叠其他 Toggle，默认为 true"
                }
            };

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(ToggleExample));
    }
}
