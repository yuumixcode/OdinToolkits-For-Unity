using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class DisableInPlayModeContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "DisableInPlayMode";

        protected override string GetIntroduction() => "简介";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParamValue> GetParamValues() => new List<ParamValue>();

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(DisableInPlayModeExample));
    }
}
