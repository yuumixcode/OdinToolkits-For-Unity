using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Editor;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class IndentContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "Indent";

        protected override string GetIntroduction() => "主动设置 Property 的缩进，可以为负数";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "主要用于样式不符合预期的修补，通常不需要手动控制缩进"
            };

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "int",
                    ParameterName = "indentLevel",
                    ParameterDescription = "缩进值，可以为负数，默认为 1"
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(IndentExample));
    }
}
