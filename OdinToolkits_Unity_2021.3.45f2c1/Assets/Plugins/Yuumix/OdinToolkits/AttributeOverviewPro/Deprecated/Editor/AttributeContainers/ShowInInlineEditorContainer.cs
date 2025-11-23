using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class ShowInInlineEditorContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "ShowInInlineEditor";

        protected override string GetIntroduction() => "标记特定 Property，当所在类对象被标记了 [InlineEditor] 时，才能显示";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParameterValue> GetParamValues() => new List<ParameterValue>();

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(ShowInInlineEditorExample));
    }
}
