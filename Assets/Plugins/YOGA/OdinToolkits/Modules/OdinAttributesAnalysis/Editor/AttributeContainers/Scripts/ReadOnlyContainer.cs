using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class ReadOnlyContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "ReadOnly";
        }

        protected override string SetBrief()
        {
            return "让 Property 在 Inspector 上只读";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "仅在 Inspector 上只读，代码中依旧可以修改"
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(ReadOnlyExample));
        }
    }
}