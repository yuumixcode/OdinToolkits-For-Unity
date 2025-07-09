using System.Collections.Generic;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class PropertyOrderContainer : AbsContainer
    {
        protected override string SetHeader() => "PropertyOrder";

        protected override string SetBrief() => "自定义控制所有 Property 的绘制顺序";

        protected override List<string> SetTip() =>
            new List<string>
            {
                "PropertyOrder 用来自定义绘制顺序，默认序号为 0，数字越大越靠后绘制，可以为负数",
                "推荐把 [PropertyOrder] 特性放到第一位"
            };

        protected override List<ParamValue> SetParamValues() => new List<ParamValue>();

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(PropertyOrderExample));
    }
}
