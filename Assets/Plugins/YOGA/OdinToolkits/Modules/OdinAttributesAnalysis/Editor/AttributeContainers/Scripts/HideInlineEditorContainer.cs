using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
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