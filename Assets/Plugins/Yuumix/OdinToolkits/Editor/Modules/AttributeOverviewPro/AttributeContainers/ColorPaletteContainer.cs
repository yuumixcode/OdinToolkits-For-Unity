using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Editor
{
    public class ColorPaletteContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "ColorPalette";

        protected override string GetIntroduction() => "Odin 定制的一个 Color 的绘制样式";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParamValue> GetParamValues() =>
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

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(ColorPaletteExample));
    }
}
