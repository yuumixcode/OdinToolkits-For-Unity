using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class DrawWithUnityContainer : AbsContainer
    {
        protected override string SetHeader() => "DrawWithUnity";

        protected override string SetBrief() => "对特定 Property 采用 Unity 原生绘制";

        protected override List<string> SetTip() => new List<string>()
        {
            "[DrawWithUnity] 可以应用于一个字段或属性，使 Odin 使用 Unity 的旧绘图系统绘制它，" +
            "注意，[DrawWithUnity] 并不意味着此 Property 会完全禁用 Odin ，只是内部选择对此 Property 的绘制调用了 Unity 原生绘制，" +
            "Odin 依旧控制 Property 绘制，并且如果存在还存在其他 Odin 特性，不能保证完全使用 Unity 原生样式",
            "如果需要完全禁用 Odin，可以设置从 Odin 系统中剔除这一个类的绘制"
        };

        protected override List<ParamValue> SetParamValues() => new List<ParamValue>()
            { };

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(DrawWithUnityExample));
    }
}