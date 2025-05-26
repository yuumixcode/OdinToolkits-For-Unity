using System.Collections.Generic;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class OnValueChangedContainer : AbsContainer
    {
        protected override string SetHeader() => "OnValueChanged";

        protected override string SetBrief() => "当 Property 在 Inspector 面板上修改时触发方法";

        protected override List<string> SetTip() =>
            new List<string>
            {
                "代码修改不会触发这个方法"
            };

        protected override List<ParamValue> SetParamValues() => new List<ParamValue>();

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(OnValueChangedExample));
    }
}
