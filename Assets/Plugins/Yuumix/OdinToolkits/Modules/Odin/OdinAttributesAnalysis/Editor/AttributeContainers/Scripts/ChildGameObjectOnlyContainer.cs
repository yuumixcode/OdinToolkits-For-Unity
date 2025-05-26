using System.Collections.Generic;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class ChildGameObjectOnlyContainer : AbsContainer
    {
        protected override string SetHeader() => "ChildGameObjectOnly";

        protected override string SetBrief() => "作用于继承 Component 或者 GameObject 的字段上，在面板上绘制一个小按钮，用于选择当前物体的子物体";

        protected override List<string> SetTip() => new List<string>();

        protected override List<ParamValue> SetParamValues() =>
            new List<ParamValue>
            {
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "IncludeSelf",
                    paramDescription = "是否包含当前物体，默认为 true"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "IncludeInactive",
                    paramDescription = "是否包含非激活的物体，默认为 false"
                }
            };

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(ChildGameObjectOnlyExample));
    }
}
