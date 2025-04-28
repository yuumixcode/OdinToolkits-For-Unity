using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class ColorPaletteContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "ColorPalette";
        }

        protected override string SetBrief()
        {
            return "Odin 定制的一个 Color 的绘制样式";
        }

        protected override List<string> SetTip()
        {
            return new List<string>();
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>
            {
                new()
                {
                    returnType = "bool",
                    paramName = "ShowAlpha",
                    paramDescription = "是否显示 Alpha 通道，默认为 true"
                },
                new()
                {
                    returnType = "string",
                    paramName = "PaletteName",
                    paramDescription = "调色板的名称"
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(ColorPaletteExample));
        }
    }
}