using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;
using System.Collections.Generic;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class DisableInEditorModeContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "DisableInEditorMode";
        }

        protected override string SetBrief()
        {
            return "让 Property 无法在编辑模式下选中";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "适合运行时需要调试的数据"
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>();
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(DisableInEditorModeExample));
        }
    }
}