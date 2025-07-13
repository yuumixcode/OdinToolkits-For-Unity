using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Editor
{
    public class DisableInEditorModeContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "DisableInEditorMode";

        protected override string GetIntroduction() => "让 Property 无法在编辑模式下选中";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "适合运行时需要调试的数据"
            };

        protected override List<ParamValue> GetParamValues() => new List<ParamValue>();

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(DisableInEditorModeExample));
    }
}
