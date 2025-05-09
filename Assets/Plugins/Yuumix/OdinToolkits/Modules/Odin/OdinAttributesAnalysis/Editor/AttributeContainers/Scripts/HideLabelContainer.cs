using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;
using System.Collections.Generic;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class HideLabelContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "HideLabel";
        }

        protected override string SetBrief()
        {
            return "隐藏字符串的名称";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "可以和其他特性配合使用，自定义字段的显示"
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>();
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(HideLabelExample));
        }
    }
}