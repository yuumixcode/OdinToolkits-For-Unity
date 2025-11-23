using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class EnumToggleButtonsContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "EnumToggleButtons";

        protected override string GetIntroduction() => "让枚举变成一排按钮，同时支持可以支持多选，更直观";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParameterValue> GetParamValues() => new List<ParameterValue>();

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(EnumToggleButtonsExample));
    }
}
