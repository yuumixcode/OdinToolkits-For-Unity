using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class TypeInfoBoxContainer : AbsContainer
    {
        protected override string SetHeader() => "TypeInfoBox";

        protected override string SetBrief() => "在类的内部的最上方绘制一个 InfoBox";

        protected override List<string> SetTip() => new List<string>()
        {
            "快速绘制一个顶部的 InfoBox，不需要使用 PropertyOrder 和 OnInspectorGUI 特性"
        };

        protected override List<ParamValue> SetParamValues() => new List<ParamValue>()
        {
            new ParamValue()
            {
                returnType = "string",
                paramName = "message",
                paramDescription = "顶部 InfoBox 的消息内容"
            }
        };

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(TypeInfoBoxExample));
    }
}