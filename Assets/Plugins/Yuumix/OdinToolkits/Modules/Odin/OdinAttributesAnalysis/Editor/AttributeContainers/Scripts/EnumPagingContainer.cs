using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;
using System.Collections.Generic;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class EnumPagingContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "EnumPaging";
        }

        protected override string SetBrief()
        {
            return "作用于枚举类型，绘制一个可循环的枚举按钮";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "可以和其他结合使用，比如可以改变 Unity 编辑器当前选择的工具"
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>();
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(EnumPagingExample));
        }
    }
}