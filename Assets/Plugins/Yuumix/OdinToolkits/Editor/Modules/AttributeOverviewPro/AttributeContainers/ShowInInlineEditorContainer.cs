using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Editor
{
    public class ShowInInlineEditorContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "ShowInInlineEditor";

        protected override string GetIntroduction() => "标记特定 Property，当所在类对象被标记了 [InlineEditor] 时，才能显示";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParamValue> GetParamValues() => new List<ParamValue>();

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(ShowInInlineEditorExample));
    }
}
