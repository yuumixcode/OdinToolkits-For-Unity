using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Editor
{
    public class TypeInfoBoxContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "TypeInfoBox";

        protected override string GetIntroduction() => "在类的内部的最上方绘制一个 InfoBox";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "快速绘制一个顶部的 InfoBox，不需要使用 PropertyOrder 和 OnInspectorGUI 特性"
            };

        protected override List<ParamValue> GetParamValues() =>
            new List<ParamValue>
            {
                new ParamValue
                {
                    returnType = "string",
                    paramName = "message",
                    paramDescription = "顶部 InfoBox 的消息内容"
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(TypeInfoBoxExample));
    }
}
