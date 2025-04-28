using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class SceneObjectsOnlyContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "SceneObjectsOnly";
        }

        protected override string SetBrief()
        {
            return "标记 Property 只能引用场景中的物体对象";
        }

        protected override List<string> SetTip()
        {
            return new List<string>();
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>();
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(SceneObjectsOnlyExample));
        }
    }
}