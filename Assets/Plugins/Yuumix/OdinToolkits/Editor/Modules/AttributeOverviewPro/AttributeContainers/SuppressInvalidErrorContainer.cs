using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Editor
{
    public class SuppressInvalidErrorContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "SuppressInvalidError";

        protected override string GetIntroduction() => "用于抑制 Odin 的特性无效的错误信息，使其不会显示";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "某些特性不能标记特定字段，会报错，再标记上 [SuppressInvalidError] 将不会报错提醒，例如 Range 特性标记在 string 类型上"
            };

        protected override List<ParamValue> GetParamValues() => new List<ParamValue>();

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(SuppressInvalidErrorExample));
    }
}
