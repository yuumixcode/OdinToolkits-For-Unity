using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class HideInPlayModeContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "HideInPlayMode";

        protected override string GetIntroduction() => "Play 状态下隐藏 Property";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParameterValue> GetParamValues() => new List<ParameterValue>();

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(HideInPlayModeExample));
    }
}
