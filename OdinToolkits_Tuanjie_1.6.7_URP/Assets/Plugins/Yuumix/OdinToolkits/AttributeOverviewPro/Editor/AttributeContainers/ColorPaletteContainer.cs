using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Editor;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class ColorPaletteContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "ColorPalette";

        protected override string GetIntroduction() => "Odin 定制的一个 Color 的绘制样式";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "ShowAlpha",
                    ParameterDescription = "是否显示 Alpha 通道，默认为 true"
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "PaletteName",
                    ParameterDescription = "调色板的名称"
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(ColorPaletteExample));
    }
}
