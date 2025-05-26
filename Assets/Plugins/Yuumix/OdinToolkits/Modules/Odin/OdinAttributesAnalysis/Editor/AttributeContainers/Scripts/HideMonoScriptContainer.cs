using System.Collections.Generic;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class HideMonoScriptContainer : AbsContainer
    {
        protected override string SetHeader() => "HideMonoScript";

        protected override string SetBrief() => "隐藏灰色显示的脚本文件的 Property";

        protected override List<string> SetTip() =>
            new List<string>
            {
                "可以在 Odin 的全局设置中关闭"
            };

        protected override List<ParamValue> SetParamValues() => new List<ParamValue>();

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(HideMonoScriptExample));
    }
}
