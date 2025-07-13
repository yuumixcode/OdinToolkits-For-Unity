using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Editor
{
    public class DisplayAsStringContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "DisplayAsString";

        protected override string GetIntroduction() => "将任意 Property 以 Label 的样式绘制在 Inspector 面板上";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParamValue> GetParamValues() =>
            new List<ParamValue>
            {
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "fontSize",
                    paramDescription = "字体大小"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "overflow",
                    paramDescription = "是否溢出"
                },
                new ParamValue
                {
                    returnType = "TextAlignment",
                    paramName = "alignment",
                    paramDescription = "对齐方式"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "enableRichText",
                    paramDescription = "是否开启富文本"
                },
                new ParamValue
                {
                    returnType = "string",
                    paramName = "Format",
                    paramDescription = "对特殊类的值进行格式化。Type 必须实现 IFormatable 接口。"
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(DisplayAsStringExample));
    }
}
