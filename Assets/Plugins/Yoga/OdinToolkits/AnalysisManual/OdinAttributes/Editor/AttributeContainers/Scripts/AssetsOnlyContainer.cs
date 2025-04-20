using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class AssetsOnlyContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "AssetsOnly";
        }

        protected override string SetBrief()
        {
            return "确保被标记的字段或者属性引用项目中的资源，而不是场景中的物体";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "可以用于确保引用项目资源"
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>();
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(AssetsOnlyExample));
        }
    }
}
