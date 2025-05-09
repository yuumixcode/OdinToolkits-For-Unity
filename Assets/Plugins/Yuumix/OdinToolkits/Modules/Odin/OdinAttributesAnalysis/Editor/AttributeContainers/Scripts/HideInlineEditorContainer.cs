using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;
using System.Collections.Generic;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class HideInlineEditorContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "HideInlineEditor";
        }

        protected override string SetBrief()
        {
            return "如果一个对象被标记 [InlineEditor]，那么被标记为 [HideInlineEditor] 的 Property 将隐藏";
        }

        protected override List<string> SetTip()
        {
            return new List<string>();
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>();
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(HideInlineEditorExample));
        }
    }
}