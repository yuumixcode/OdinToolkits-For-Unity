using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Editor;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class DisableInInlineEditorContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "DisableInInlineEditor";

        protected override string GetIntroduction() => "标记特定 Property，当所在类对象被标记了 [InlineEditor] 时，无法选中";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParameterValue> GetParamValues() => new List<ParameterValue>();

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(DisableInInlineEditorExample));
    }
}
