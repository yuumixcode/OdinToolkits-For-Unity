using System.Collections.Generic;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class ColorPaletteContainer : AbsContainer
    {
        protected override string SetHeader() => "ColorPalette";

        protected override string SetBrief() => "Odin 定制的一个 Color 的绘制样式";

        protected override List<string> SetTip() => new List<string>();

        protected override List<ParamValue> SetParamValues() =>
            new List<ParamValue>
            {
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "ShowAlpha",
                    paramDescription = "是否显示 Alpha 通道，默认为 true"
                },
                new ParamValue
                {
                    returnType = "string",
                    paramName = "PaletteName",
                    paramDescription = "调色板的名称"
                }
            };

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(ColorPaletteExample));
    }
}
