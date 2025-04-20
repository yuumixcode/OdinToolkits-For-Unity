using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class HideInTablesContainer : AbsContainer
    {
        protected override string SetHeader() => "HideInTables";

        protected override string SetBrief() => "在 TableList 中隐藏被 [HideInTables] 标记的字段";

        protected override List<string> SetTip() => new List<string>()
            { };

        protected override List<ParamValue> SetParamValues() => new List<ParamValue>()
            { };

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(HideInTablesExample));
    }
}