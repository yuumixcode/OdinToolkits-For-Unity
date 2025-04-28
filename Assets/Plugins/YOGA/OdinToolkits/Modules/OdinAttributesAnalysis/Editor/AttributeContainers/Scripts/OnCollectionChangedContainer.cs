using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class OnCollectionChangedContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "OnCollectionChanged";
        }

        protected override string SetBrief()
        {
            return "当一个集合的元素改变时，增加或者删除";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "方法签名模板为: public void FunctionName(CollectionChangeInfo info, object value)",
                "CollectionChangeInfo 是 Odin 提供的一个修改行为有关的结构体，value 表示的值是集合，而不是集合中的元素",
                "该示例使用了 Odin 序列化，序列化 HashSet 和 Dictionary"
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>
            {
                new()
                {
                    returnType = "string",
                    paramName = "before",
                    paramDescription = "触发函数名，时机为修改前，方法参数为 (CollectionChangeInfo info, object value)，无返回值，" +
                                       DescriptionConfigs.SupportAllResolver
                },
                new()
                {
                    returnType = "string",
                    paramName = "after",
                    paramDescription = "触发函数名，时机为修改前，方法参数为 (CollectionChangeInfo info, object value)，无返回值，" +
                                       DescriptionConfigs.SupportAllResolver
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(OnCollectionChangedExample));
        }
    }
}