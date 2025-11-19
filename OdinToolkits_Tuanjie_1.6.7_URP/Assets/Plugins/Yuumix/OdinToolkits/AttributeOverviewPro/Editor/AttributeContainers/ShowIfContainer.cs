using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Editor;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class ShowIfContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "ShowIf";

        protected override string GetIntroduction() => "满足特定条件时，显示对应的 Property";

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

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(ShowIfExample));
    }
}
