using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class TableColumnWidthContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "TableColumnWidth";

        protected override string GetIntroduction() => "设置 TableList 特性标记的 List 的元素宽度";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "列表必须标记 [TableList]，列表元素封装为一个类对象，在元素内部的字段上标记 [TableColumnWidth]"
            };

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "int",
                    ParameterName = "width",
                    ParameterDescription = "宽度，单位为像素"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "resizable",
                    ParameterDescription = "是否允许调整宽度，默认为 true"
                }
            };

        protected override string GetOriginalCode() =>
            ReadCodeWithoutNamespace(typeof(TableColumnWidthExample));
    }
}
