using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;
using System.Collections.Generic;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class SuppressInvalidErrorContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "SuppressInvalidError";
        }

        protected override string SetBrief()
        {
            return "用于抑制 Odin 的特性无效的错误信息，使其不会显示";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "某些特性不能标记特定字段，会报错，再标记上 [SuppressInvalidError] 将不会报错提醒，例如 Range 特性标记在 string 类型上"
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>();
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(SuppressInvalidErrorExample));
        }
    }
}