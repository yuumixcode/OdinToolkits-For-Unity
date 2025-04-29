using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;
using System.Collections.Generic;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class HideReferenceObjectPickerContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "HideReferenceObjectPicker";
        }

        protected override string SetBrief()
        {
            return "Odin 序列化可以将原本 Unity 不支持序列化的多态类型绘制出引用选择器，[HideReferenceObjectPicker]可以将引用选择器隐藏，避免修改";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "多态类型的序列化需要使用 Odin 的序列化",
                "当选择器被隐藏时，可以右键单击字段设置为 null",
                "如果完全不想被修改，可以使用 [DisableContextMenu] 标记字段"
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>();
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(HideReferenceObjectPickerExample));
        }
    }
}