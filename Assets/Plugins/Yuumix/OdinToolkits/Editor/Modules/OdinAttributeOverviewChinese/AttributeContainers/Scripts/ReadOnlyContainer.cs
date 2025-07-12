using System.Collections.Generic;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class ReadOnlyContainer : AbsContainer
    {
        protected override string SetHeader() => "ReadOnly";

        protected override string SetBrief() => "让 Property 在 Inspector 上只读";

        protected override List<string> SetTip() =>
            new List<string>
            {
                "仅在 Inspector 上只读，代码中依旧可以修改"
            };

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(ReadOnlyExample));
    }
}
