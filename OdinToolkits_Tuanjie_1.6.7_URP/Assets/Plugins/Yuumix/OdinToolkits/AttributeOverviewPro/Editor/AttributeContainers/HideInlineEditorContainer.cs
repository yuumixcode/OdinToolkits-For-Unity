using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class HideInlineEditorContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "HideInlineEditor";

        protected override string GetIntroduction() =>
            "如果一个对象被标记 [InlineEditor]，那么被标记为 [HideInlineEditor] 的 Property 将隐藏";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParamValue> GetParamValues() => new List<ParamValue>();

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(HideInlineEditorExample));
    }
}
