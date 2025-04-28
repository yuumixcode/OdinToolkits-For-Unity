using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class ChildGameObjectOnlyContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "ChildGameObjectOnly";
        }

        protected override string SetBrief()
        {
            return "作用于继承 Component 或者 GameObject 的字段上，在面板上绘制一个小按钮，用于选择当前物体的子物体";
        }

        protected override List<string> SetTip()
        {
            return new List<string>();
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>
            {
                new()
                {
                    returnType = "bool",
                    paramName = "IncludeSelf",
                    paramDescription = "是否包含当前物体，默认为 true"
                },
                new()
                {
                    returnType = "bool",
                    paramName = "IncludeInactive",
                    paramDescription = "是否包含非激活的物体，默认为 false"
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(ChildGameObjectOnlyExample));
        }
    }
}