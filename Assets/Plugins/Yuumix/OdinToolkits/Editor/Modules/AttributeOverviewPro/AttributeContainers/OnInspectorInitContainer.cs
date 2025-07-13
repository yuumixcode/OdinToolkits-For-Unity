using System.Collections.Generic;

namespace Yuumix.OdinToolkits.Editor
{
    public class OnInspectorInitContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "OnInspectorInit";

        protected override string GetIntroduction() => "当 Inspector 面板选择时，执行初始化操作";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "此方法至少执行一次，也有可能执行多次，当重新构建 PropertyTree 时，也会触发，比较常见的是多态类型修改值触发",
                "根据字段顺序进行触发初始化方法"
            };

        protected override List<ParamValue> GetParamValues() =>
            new List<ParamValue>
            {
                new ParamValue
                {
                    returnType = "string",
                    paramName = "action",
                    paramDescription = "触发函数名，方法可选 (InspectorProperty property, T value)，" +
                                       DescriptionConfigs.SupportAllResolver
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(OnInspectorInitExample));
    }
}
