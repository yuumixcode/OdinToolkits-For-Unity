using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Editor
{
    public class InlinePropertyContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "InlineProperty";

        protected override string GetIntroduction() => "将通常需要折叠的 Property 重新绘制在一行中";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "参数设置的是自定义类或者结构体中的子 properties 的宽度"
            };

        protected override List<ParamValue> GetParamValues() =>
            new List<ParamValue>
            {
                new ParamValue
                {
                    returnType = "int",
                    paramName = "LabelWidth",
                    paramDescription = "所有子 properties 的宽度"
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(InlinePropertyExample));
    }
}
