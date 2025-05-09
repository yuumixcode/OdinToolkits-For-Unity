using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;
using System.Collections.Generic;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class ShowInInlineEditorContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "ShowInInlineEditor";
        }

        protected override string SetBrief()
        {
            return "标记特定 Property，当所在类对象被标记了 [InlineEditor] 时，才能显示";
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
            return ReadCodeWithoutNamespace(typeof(ShowInInlineEditorExample));
        }
    }
}