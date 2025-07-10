using System.Collections.Generic;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class ShowInInlineEditorContainer : AbsContainer
    {
        protected override string SetHeader() => "ShowInInlineEditor";

        protected override string SetBrief() => "标记特定 Property，当所在类对象被标记了 [InlineEditor] 时，才能显示";

        protected override List<string> SetTip() => new List<string>();

        protected override List<ParamValue> SetParamValues() => new List<ParamValue>();

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(ShowInInlineEditorExample));
    }
}
