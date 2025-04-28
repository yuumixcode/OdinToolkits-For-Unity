using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class PropertyOrderContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "PropertyOrder";
        }

        protected override string SetBrief()
        {
            return "自定义控制所有 Property 的绘制顺序";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "PropertyOrder 用来自定义绘制顺序，默认序号为 0，数字越大越靠后绘制，可以为负数",
                "推荐把 [PropertyOrder] 特性放到第一位"
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>();
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(PropertyOrderExample));
        }
    }
}