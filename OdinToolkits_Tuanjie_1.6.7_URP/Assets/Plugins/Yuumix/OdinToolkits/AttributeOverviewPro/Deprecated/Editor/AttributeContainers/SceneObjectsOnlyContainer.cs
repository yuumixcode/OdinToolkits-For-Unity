using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class SceneObjectsOnlyContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "SceneObjectsOnly";

        protected override string GetIntroduction() => "标记 Property 只能引用场景中的物体对象";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParameterValue> GetParamValues() => new List<ParameterValue>();

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(SceneObjectsOnlyExample));
    }
}
