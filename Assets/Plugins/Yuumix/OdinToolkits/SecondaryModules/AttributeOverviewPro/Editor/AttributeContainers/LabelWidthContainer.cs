using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class LabelWidthContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "LabelWidth";

        protected override string GetIntroduction() => "Property 名称字符串的宽度";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParamValue> GetParamValues() =>
            new List<ParamValue>
            {
                new ParamValue
                {
                    returnType = "float",
                    paramName = "width",
                    paramDescription = "宽度值，单位为像素"
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(LabelWidthExample));
    }
}
