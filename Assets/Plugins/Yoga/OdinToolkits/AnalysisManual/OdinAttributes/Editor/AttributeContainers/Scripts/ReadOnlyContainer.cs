using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class ReadOnlyContainer : AbsContainer
    {
        protected override string SetHeader() => "ReadOnly";

        protected override string SetBrief() => "让 Property 在 Inspector 上只读";

        protected override List<string> SetTip() => new List<string>()
        {
            "仅在 Inspector 上只读，代码中依旧可以修改"
        };

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(ReadOnlyExample));
    }
}
