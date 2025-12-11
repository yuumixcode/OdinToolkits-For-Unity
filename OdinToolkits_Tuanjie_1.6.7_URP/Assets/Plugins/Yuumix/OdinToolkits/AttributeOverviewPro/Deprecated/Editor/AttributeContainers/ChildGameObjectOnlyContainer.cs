using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class ChildGameObjectOnlyContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "ChildGameObjectOnly";

        protected override string GetIntroduction() =>
            "作用于继承 Component 或者 GameObject 的字段上，在面板上绘制一个小按钮，用于选择当前物体的子物体";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "IncludeSelf",
                    ParameterDescription = "是否包含当前物体，默认为 true"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "IncludeInactive",
                    ParameterDescription = "是否包含非激活的物体，默认为 false"
                }
            };

        protected override string GetOriginalCode() =>
            ReadCodeWithoutNamespace(typeof(ChildGameObjectOnlyExample));
    }
}
