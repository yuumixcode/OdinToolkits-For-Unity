using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class ShowIfGroupContainer : AbsContainer
    {
        protected override string SetHeader() => "ShowIfGroup";

        protected override string SetBrief() => "让 Property 以组的形式同时隐藏或显示";

        protected override List<string> SetTip() => new List<string>()
        {
            "如果 ShowIfGroup 没有设置 Condition，那么路径既发挥组名作用，也是条件判断的值，引用成员名，如果设置 Condition，则路径将只表示组名",
            "ShowIfGroup 可以和其他组连接使用",
            "注: ShowIfGroup 的组路径目前无法在 Rider 中识别，但是正常生效。- 3.3.1.11"
        };

        protected override List<ParamValue> SetParamValues() => new List<ParamValue>()
        {
            new ParamValue()
            {
                returnType = "string",
                paramName = "path",
                paramDescription = "路径，可以只代表组名，也可以引用成员名，" + DescriptionConfigs.SupportMemberResolverLite + "特例: 不支持方法名"
            },
            new ParamValue()
            {
                returnType = "string",
                paramName = "Condition",
                paramDescription = "条件，可以是任意字符串，但不是空字符串，" + DescriptionConfigs.SupportMemberResolverLite
            },
            new ParamValue()
            {
                returnType = "object",
                paramName = "Value",
                paramDescription = "值，可以是任意类型，但不是 null"
            },
            new ParamValue()
            {
                returnType = "bool",
                paramName = "animate",
                paramDescription = "是否显示折叠动画，默认为 true"
            }
        };

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(ShowIfGroupExample));
    }
}