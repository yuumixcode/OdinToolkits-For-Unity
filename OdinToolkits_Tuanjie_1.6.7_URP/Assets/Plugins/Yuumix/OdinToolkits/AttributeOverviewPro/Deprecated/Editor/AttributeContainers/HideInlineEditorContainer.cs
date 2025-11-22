using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class HideInlineEditorContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "HideInlineEditor";

        protected override string GetIntroduction() =>
            "如果一个对象被标记 [InlineEditor]，那么被标记为 [HideInlineEditor] 的 Property 将隐藏";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParameterValue> GetParamValues() => new List<ParameterValue>();

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(HideInlineEditorExample));
    }
}
