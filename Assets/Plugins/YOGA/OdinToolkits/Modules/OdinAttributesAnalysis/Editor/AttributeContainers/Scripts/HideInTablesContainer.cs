using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class HideInTablesContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "HideInTables";
        }

        protected override string SetBrief()
        {
            return "在 TableList 中隐藏被 [HideInTables] 标记的字段";
        }

        protected override List<string> SetTip()
        {
            return new List<string>();
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>();
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(HideInTablesExample));
        }
    }
}