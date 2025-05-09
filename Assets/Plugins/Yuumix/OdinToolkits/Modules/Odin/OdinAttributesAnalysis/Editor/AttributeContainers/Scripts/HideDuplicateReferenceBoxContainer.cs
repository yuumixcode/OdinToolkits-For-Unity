using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;
using System.Collections.Generic;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class HideDuplicateReferenceBoxContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "HideDuplicateReferenceBox";
        }

        protected override string SetBrief()
        {
            return "如果多个 Property 的值都引用同一个，那么 Odin 将绘制一个 Box 告知用户，该值是同一个，" +
                   "[HideDuplicateReferenceBox] 可以关闭此绘制";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "如果是递归调用，将无法关闭 Box 的绘制",
                "一般在 Inspector 面板中，也不会相互引用，而且就算有，Odin 使用 Box 也可以更好提醒"
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>();
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(HideDuplicateReferenceBoxExample));
        }
    }
}