using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Editor;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
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

        protected override List<ParameterValue> GetParamValues() => new List<ParameterValue>();

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(EnumPagingExample));
    }
}
