using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class TableColumnWidthContainer : AbsContainer
    {
        protected override string SetHeader() => "TableColumnWidth";

        protected override string SetBrief() => "设置 TableList 特性标记的 List 的元素宽度";

        protected override List<string> SetTip() => new List<string>()
        {
            "列表必须标记 [TableList]，列表元素封装为一个类对象，在元素内部的字段上标记 [TableColumnWidth]"
        };

        protected override List<ParamValue> SetParamValues() => new List<ParamValue>()
        {
            new ParamValue()
            {
                returnType = "int",
                paramName = "width",
                paramDescription = "宽度，单位为像素"
            },
            new ParamValue()
            {
                returnType = "bool",
                paramName = "resizable",
                paramDescription = "是否允许调整宽度，默认为 true"
            }
        };

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(TableColumnWidthExample));
    }
}