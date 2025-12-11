using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class DisplayAsStringContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "DisplayAsString";

        protected override string GetIntroduction() => "将任意 Property 以 Label 的样式绘制在 Inspector 面板上";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "fontSize",
                    ParameterDescription = "字体大小"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "overflow",
                    ParameterDescription = "是否溢出"
                },
                new ParameterValue
                {
                    ReturnType = "TextAlignment",
                    ParameterName = "alignment",
                    ParameterDescription = "对齐方式"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "enableRichText",
                    ParameterDescription = "是否开启富文本"
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "Format",
                    ParameterDescription = "对特殊类的值进行格式化。Type 必须实现 IFormatable 接口。"
                }
            };

        protected override string GetOriginalCode() =>
            ReadCodeWithoutNamespace(typeof(DisplayAsStringExample));
    }
}
