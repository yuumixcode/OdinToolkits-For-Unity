using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Editor
{
    public class EnumToggleButtonsContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "EnumToggleButtons";

        protected override string GetIntroduction() => "让枚举变成一排按钮，同时支持可以支持多选，更直观";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParamValue> GetParamValues() => new List<ParamValue>();

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(EnumToggleButtonsExample));
    }
}
