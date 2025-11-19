using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Editor;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class BoxGroupContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "BoxGroup";

        protected override string GetIntroduction() => "将 Property 使用 Box 样式进行分组";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "可以和其他 Group 特性连接，共享分组路径"
            };

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "group",
                    ParameterDescription = "分组路径，可以和其他 Group 连接使用"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "showLabel",
                    ParameterDescription = "是否显示分组标题"
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "LabelText",
                    ParameterDescription = "自定义分组标题"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "centerLabel",
                    ParameterDescription = "是否居中显示标题"
                },
                new ParameterValue
                {
                    ReturnType = "int",
                    ParameterName = "order",
                    ParameterDescription =
                        "是不同 Group 在 Inspector 面板上的排序，从 PropertyGroupAttribute 基类继承获得的变量，比 PropertyOrder 优先级更高"
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(BoxGroupExample));
    }
}
