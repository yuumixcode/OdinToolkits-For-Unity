using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class SceneObjectsOnlyContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "SceneObjectsOnly";

        protected override string GetIntroduction() => "标记 Property 只能引用场景中的物体对象";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParamValue> GetParamValues() => new List<ParamValue>();

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(SceneObjectsOnlyExample));
    }
}
