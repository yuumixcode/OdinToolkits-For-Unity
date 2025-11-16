using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class ShowIfContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "ShowIf";

        protected override string GetIntroduction() => "满足特定条件时，显示对应的 Property";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParamValue> GetParamValues() =>
            new List<ParamValue>
            {
                new ParamValue
                {
                    returnType = "string",
                    paramName = "condition",
                    paramDescription = "成员名，" + DescriptionConfigs.SupportAllResolver
                },
                new ParamValue
                {
                    returnType = "object",
                    paramName = "optionalValue",
                    paramDescription = "成员名的值，需要与 condition 参数配合使用，如果成员的值 == optionalValue，则满足条件"
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(ShowIfExample));
    }
}
