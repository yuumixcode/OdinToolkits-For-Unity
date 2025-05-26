using System.Collections.Generic;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class HideInEditorModeContainer : AbsContainer
    {
        protected override string SetHeader() => "HideInEditorMode";

        protected override string SetBrief() => "标记的 Property 在编辑器状态下隐藏";

        protected override List<string> SetTip() =>
            new List<string>
                { "适合运行时需要调试的数据" };

        protected override List<ParamValue> SetParamValues() => new List<ParamValue>();

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(HideInEditorModeExample));
    }
}
