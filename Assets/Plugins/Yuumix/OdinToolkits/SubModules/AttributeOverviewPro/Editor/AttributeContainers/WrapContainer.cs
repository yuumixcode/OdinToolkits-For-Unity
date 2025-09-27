using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class WrapContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "Wrap";

        protected override string GetIntroduction() => "可以对大部分的基础变量使用，让它在达到某个值时，开始循环";

        protected override List<string> GetTips() =>
            new List<string>
                { "对角度值可以使用" };

        protected override List<ParamValue> GetParamValues() =>
            new List<ParamValue>
            {
                new ParamValue
                {
                    returnType = "double",
                    paramName = "min",
                    paramDescription = "最小值" + DescriptionConfigs.SupportAllResolver
                },
                new ParamValue
                {
                    returnType = "double",
                    paramName = "max",
                    paramDescription = "最大值" + DescriptionConfigs.SupportAllResolver
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(WrapExample));
    }
}
