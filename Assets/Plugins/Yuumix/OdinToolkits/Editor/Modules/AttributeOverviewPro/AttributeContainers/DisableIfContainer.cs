using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Editor
{
    public class DisableIfContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "DisableIf";

        protected override string GetIntroduction() => "满足条件后，关闭任何 Property 的焦点获取，无法选中";

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

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(DisableIfExample));
    }
}
