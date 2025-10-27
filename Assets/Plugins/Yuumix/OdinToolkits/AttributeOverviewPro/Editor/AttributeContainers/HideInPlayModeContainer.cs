using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class HideInPlayModeContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "HideInPlayMode";

        protected override string GetIntroduction() => "Play 状态下隐藏 Property";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParamValue> GetParamValues() => new List<ParamValue>();

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(HideInPlayModeExample));
    }
}
