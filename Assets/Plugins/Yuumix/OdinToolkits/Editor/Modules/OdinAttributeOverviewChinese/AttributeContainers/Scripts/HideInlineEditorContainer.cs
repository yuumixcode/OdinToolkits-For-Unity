using System.Collections.Generic;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class HideInlineEditorContainer : AbsContainer
    {
        protected override string SetHeader() => "HideInlineEditor";

        protected override string SetBrief() => "如果一个对象被标记 [InlineEditor]，那么被标记为 [HideInlineEditor] 的 Property 将隐藏";

        protected override List<string> SetTip() => new List<string>();

        protected override List<ParamValue> SetParamValues() => new List<ParamValue>();

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(HideInlineEditorExample));
    }
}
