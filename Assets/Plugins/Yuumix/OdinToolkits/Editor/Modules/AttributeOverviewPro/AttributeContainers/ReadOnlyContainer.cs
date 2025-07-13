using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Editor
{
    public class ReadOnlyContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "ReadOnly";

        protected override string GetIntroduction() => "让 Property 在 Inspector 上只读";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "仅在 Inspector 上只读，代码中依旧可以修改"
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(ReadOnlyExample));
    }
}
