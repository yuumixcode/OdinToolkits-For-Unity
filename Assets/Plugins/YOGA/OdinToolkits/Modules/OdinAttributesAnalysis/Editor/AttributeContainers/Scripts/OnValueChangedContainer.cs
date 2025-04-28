using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class OnValueChangedContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "OnValueChanged";
        }

        protected override string SetBrief()
        {
            return "当 Property 在 Inspector 面板上修改时触发方法";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "代码修改不会触发这个方法"
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>();
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(OnValueChangedExample));
        }
    }
}