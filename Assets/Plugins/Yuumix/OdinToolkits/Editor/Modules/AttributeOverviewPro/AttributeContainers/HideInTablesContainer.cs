using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Editor
{
    public class HideInTablesContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "HideInTables";

        protected override string GetIntroduction() => "在 TableList 中隐藏被 [HideInTables] 标记的字段";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParamValue> GetParamValues() => new List<ParamValue>();

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(HideInTablesExample));
    }
}
