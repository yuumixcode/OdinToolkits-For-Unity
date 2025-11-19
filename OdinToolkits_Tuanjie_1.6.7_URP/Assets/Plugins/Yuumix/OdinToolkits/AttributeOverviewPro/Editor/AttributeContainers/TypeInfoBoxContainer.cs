using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Editor;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
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

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "message",
                    ParameterDescription = "顶部 InfoBox 的消息内容"
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(TypeInfoBoxExample));
    }
}
