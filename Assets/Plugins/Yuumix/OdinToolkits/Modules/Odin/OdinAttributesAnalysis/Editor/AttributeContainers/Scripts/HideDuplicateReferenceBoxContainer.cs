using System.Collections.Generic;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class HideDuplicateReferenceBoxContainer : AbsContainer
    {
        protected override string SetHeader() => "HideDuplicateReferenceBox";

        protected override string SetBrief() =>
            "如果多个 Property 的值都引用同一个，那么 Odin 将绘制一个 Box 告知用户，该值是同一个，" +
            "[HideDuplicateReferenceBox] 可以关闭此绘制";

        protected override List<string> SetTip() =>
            new List<string>
            {
                "如果是递归调用，将无法关闭 Box 的绘制",
                "一般在 Inspector 面板中，也不会相互引用，而且就算有，Odin 使用 Box 也可以更好提醒"
            };

        protected override List<ParamValue> SetParamValues() => new List<ParamValue>();

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(HideDuplicateReferenceBoxExample));
    }
}
