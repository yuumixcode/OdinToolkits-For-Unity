using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Editor
{
    public class MultiLinePropertyContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "MultiLineProperty";

        protected override string GetIntroduction() => "简介";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParamValue> GetParamValues() => new List<ParamValue>();

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(MultiLinePropertyExample));
    }
}
