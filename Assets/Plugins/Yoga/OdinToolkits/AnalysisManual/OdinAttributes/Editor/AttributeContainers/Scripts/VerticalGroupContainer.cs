using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class VerticalGroupContainer : AbsContainer
    {
        protected override string SetHeader() => "VerticalGroup";

        protected override string SetBrief() => "让 Property 按垂直布局分组，本身没有什么用，主要是和其他 Group 配合使用";

        protected override List<string> SetTip() => new List<string>()
            { };

        protected override List<ParamValue> SetParamValues() => new List<ParamValue>()
        {
            new ParamValue()
            {
                returnType = "string",
                paramName = "groupId",
                paramDescription = "分组 Id，代表名称，也代表路径"
            },
            new ParamValue()
            {
                returnType = "float",
                paramName = "order",
                paramDescription = "不同 Group 之间的排序，越大越靠后"
            },
            new ParamValue()
            {
                returnType = "float",
                paramName = "PaddingTop",
                paramDescription = "顶边距，默认为 0"
            },
            new ParamValue()
            {
                returnType = "float",
                paramName = "PaddingBottom",
                paramDescription = "底边距，默认为 0"
            }
        };

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(VerticalGroupExample));
    }
}