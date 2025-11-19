using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Editor;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class VerticalGroupContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "VerticalGroup";

        protected override string GetIntroduction() => "让 Property 按垂直布局分组，本身没有什么用，主要是和其他 Group 配合使用";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "groupId",
                    ParameterDescription = "分组 Id，代表名称，也代表路径"
                },
                new ParameterValue
                {
                    ReturnType = "float",
                    ParameterName = "order",
                    ParameterDescription = "不同 Group 之间的排序，越大越靠后"
                },
                new ParameterValue
                {
                    ReturnType = "float",
                    ParameterName = "PaddingTop",
                    ParameterDescription = "顶边距，默认为 0"
                },
                new ParameterValue
                {
                    ReturnType = "float",
                    ParameterName = "PaddingBottom",
                    ParameterDescription = "底边距，默认为 0"
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(VerticalGroupExample));
    }
}
