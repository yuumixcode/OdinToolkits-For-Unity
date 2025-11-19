using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Editor;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class FoldoutGroupContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "FoldoutGroup";

        protected override string GetIntroduction() => "将 Property 以 Foldout 的形式分组，可以折叠";

        protected override List<string> GetTips() =>
            new List<string>
                { "可以和其他 Group 特性连接，共享分组路径" };

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "groupName",
                    ParameterDescription = "分组名称"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "expanded",
                    ParameterDescription = "是否设置默认状态为展开"
                },
                new ParameterValue
                {
                    ReturnType = "int",
                    ParameterName = "order",
                    ParameterDescription =
                        "是不同 Group 在 Inspector 面板上的排序，从 PropertyGroupAttribute 基类继承获得的变量，比 PropertyOrder 优先级更高"
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(FoldoutGroupExample));
    }
}
