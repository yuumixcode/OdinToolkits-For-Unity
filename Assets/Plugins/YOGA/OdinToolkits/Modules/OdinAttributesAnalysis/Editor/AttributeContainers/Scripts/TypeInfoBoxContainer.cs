using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class TypeInfoBoxContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "TypeInfoBox";
        }

        protected override string SetBrief()
        {
            return "在类的内部的最上方绘制一个 InfoBox";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "快速绘制一个顶部的 InfoBox，不需要使用 PropertyOrder 和 OnInspectorGUI 特性"
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>
            {
                new()
                {
                    returnType = "string",
                    paramName = "message",
                    paramDescription = "顶部 InfoBox 的消息内容"
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(TypeInfoBoxExample));
        }
    }
}