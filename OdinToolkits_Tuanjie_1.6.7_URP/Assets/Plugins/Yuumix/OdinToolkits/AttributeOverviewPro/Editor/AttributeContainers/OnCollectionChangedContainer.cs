using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Editor;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class OnCollectionChangedContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "OnCollectionChanged";

        protected override string GetIntroduction() => "当一个集合的元素改变时，增加或者删除";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "方法签名模板为: public void FunctionName(CollectionChangeInfo info, object value)",
                "CollectionChangeInfo 是 Odin 提供的一个修改行为有关的结构体，value 表示的值是集合，而不是集合中的元素",
                "该示例使用了 Odin 序列化，序列化 HashSet 和 Dictionary"
            };

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "before",
                    ParameterDescription = "触发函数名，时机为修改前，方法参数为 (CollectionChangeInfo info, object value)，无返回值，" +
                                       DescriptionConfigs.SupportAllResolver
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "after",
                    ParameterDescription = "触发函数名，时机为修改前，方法参数为 (CollectionChangeInfo info, object value)，无返回值，" +
                                       DescriptionConfigs.SupportAllResolver
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(OnCollectionChangedExample));
    }
}
