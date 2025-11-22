using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class DisableInPlayModeContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "DisableInPlayMode";

        protected override string GetIntroduction() => "简介";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParameterValue> GetParamValues() => new List<ParameterValue>();

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(DisableInPlayModeExample));
    }
}
