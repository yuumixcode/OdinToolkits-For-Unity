using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class DisallowModificationsInContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "DisallowModificationsIn";

        protected override string GetIntroduction() => "禁用 Property，防止进行修改并启用验证，如果进行了修改，则提供错误消息";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "代码可以修改，但是会出现错误信息，[DisableIn] 仅仅禁用成员，代码修改不会报错"
            };

        protected override List<ParamValue> GetParamValues() => new List<ParamValue>();

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(DisallowModificationsInExample));
    }
}
