using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class OnValueChangedContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "OnValueChanged";

        protected override string GetIntroduction() => "当 Property 在 Inspector 面板上修改时触发方法";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "代码修改不会触发这个方法"
            };

        protected override List<ParameterValue> GetParamValues() => new List<ParameterValue>();

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(OnValueChangedExample));
    }
}
