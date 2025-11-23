using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class HideInTablesContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "HideInTables";

        protected override string GetIntroduction() => "在 TableList 中隐藏被 [HideInTables] 标记的字段";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParameterValue> GetParamValues() => new List<ParameterValue>();

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(HideInTablesExample));
    }
}
