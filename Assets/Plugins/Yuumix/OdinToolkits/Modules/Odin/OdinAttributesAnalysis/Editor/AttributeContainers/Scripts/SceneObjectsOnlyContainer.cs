using System.Collections.Generic;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class SceneObjectsOnlyContainer : AbsContainer
    {
        protected override string SetHeader() => "SceneObjectsOnly";

        protected override string SetBrief() => "标记 Property 只能引用场景中的物体对象";

        protected override List<string> SetTip() => new List<string>();

        protected override List<ParamValue> SetParamValues() => new List<ParamValue>();

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(SceneObjectsOnlyExample));
    }
}
