using System.Collections.Generic;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class AssetsOnlyContainer : AbsContainer
    {
        protected override string SetHeader() => "AssetsOnly";

        protected override string SetBrief() => "确保被标记的字段或者属性引用项目中的资源，而不是场景中的物体";

        protected override List<string> SetTip() =>
            new List<string>
            {
                "可以用于确保引用项目资源"
            };

        protected override List<ParamValue> SetParamValues() => new List<ParamValue>();

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(AssetsOnlyExample));
    }
}
