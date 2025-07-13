using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Editor
{
    public class EnumPagingContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "EnumPaging";

        protected override string GetIntroduction() => "作用于枚举类型，绘制一个可循环的枚举按钮";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "可以和其他结合使用，比如可以改变 Unity 编辑器当前选择的工具"
            };

        protected override List<ParamValue> GetParamValues() => new List<ParamValue>();

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(EnumPagingExample));
    }
}
