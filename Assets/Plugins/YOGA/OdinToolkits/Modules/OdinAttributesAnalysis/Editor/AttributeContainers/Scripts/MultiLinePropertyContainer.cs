using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class MultiLinePropertyContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "MultiLineProperty";
        }

        protected override string SetBrief()
        {
            return "简介";
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
            return ReadCodeWithoutNamespace(typeof(MultiLinePropertyExample));
        }
    }
}