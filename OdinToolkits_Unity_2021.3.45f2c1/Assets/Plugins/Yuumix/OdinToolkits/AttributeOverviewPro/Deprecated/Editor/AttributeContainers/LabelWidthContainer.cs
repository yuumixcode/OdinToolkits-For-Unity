using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class LabelWidthContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "LabelWidth";

        protected override string GetIntroduction() => "Property 名称字符串的宽度";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "float",
                    ParameterName = "width",
                    ParameterDescription = "宽度值，单位为像素"
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(LabelWidthExample));
    }
}
