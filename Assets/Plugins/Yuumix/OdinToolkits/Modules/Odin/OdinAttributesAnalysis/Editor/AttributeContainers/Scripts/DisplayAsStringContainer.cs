using System.Collections.Generic;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class DisplayAsStringContainer : AbsContainer
    {
        protected override string SetHeader() => "DisplayAsString";

        protected override string SetBrief() => "将任意 Property 以 Label 的样式绘制在 Inspector 面板上";

        protected override List<string> SetTip() => new List<string>();

        protected override List<ParamValue> SetParamValues() =>
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

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(DisplayAsStringExample));
    }
}
