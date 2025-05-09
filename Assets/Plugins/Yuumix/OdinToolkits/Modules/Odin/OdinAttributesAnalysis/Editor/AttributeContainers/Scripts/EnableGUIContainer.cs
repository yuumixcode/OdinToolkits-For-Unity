using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;
using System.Collections.Generic;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class EnableGUIContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "EnableGUI";
        }

        protected override string SetBrief()
        {
            return "强制启用 property ，使其正常显示";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "部分 property 是灰色显示无法修改，可以使用 [EnableGUI] 来恢复正常，仅优化显示样式"
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>();
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(EnableGUIExample));
        }
    }
}