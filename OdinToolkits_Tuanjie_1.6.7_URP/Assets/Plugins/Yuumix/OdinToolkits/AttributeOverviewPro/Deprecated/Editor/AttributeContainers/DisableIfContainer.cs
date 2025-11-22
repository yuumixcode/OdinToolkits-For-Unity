using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class DisableIfContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "DisableIf";

        protected override string GetIntroduction() => "满足条件后，关闭任何 Property 的焦点获取，无法选中";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "condition",
                    ParameterDescription = "成员名，" + DescriptionConfigs.SupportAllResolver
                },
                new ParameterValue
                {
                    ReturnType = "object",
                    ParameterName = "optionalValue",
                    ParameterDescription = "成员名的值，需要与 condition 参数配合使用，如果成员的值 == optionalValue，则满足条件"
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(DisableIfExample));
    }
}
