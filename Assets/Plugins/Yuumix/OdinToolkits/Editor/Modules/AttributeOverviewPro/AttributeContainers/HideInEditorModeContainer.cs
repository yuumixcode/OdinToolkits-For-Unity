using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Editor
{
    public class HideInEditorModeContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "HideInEditorMode";

        protected override string GetIntroduction() => "标记的 Property 在编辑器状态下隐藏";

        protected override List<string> GetTips() =>
            new List<string>
                { "适合运行时需要调试的数据" };

        protected override List<ParamValue> GetParamValues() => new List<ParamValue>();

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(HideInEditorModeExample));
    }
}
