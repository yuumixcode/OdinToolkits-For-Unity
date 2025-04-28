using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class HideInEditorModeContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "HideInEditorMode";
        }

        protected override string SetBrief()
        {
            return "标记的 Property 在编辑器状态下隐藏";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
                { "适合运行时需要调试的数据" };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>();
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(HideInEditorModeExample));
        }
    }
}