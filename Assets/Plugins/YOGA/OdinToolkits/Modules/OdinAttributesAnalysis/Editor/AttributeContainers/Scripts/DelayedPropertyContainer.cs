using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class DelayedPropertyContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "DelayedProperty";
        }

        protected override string SetBrief()
        {
            return "延迟修改值，确认后才真正修改，和 Unity 内置的十分相似，但是它可以作用于属性，而不仅仅是字段";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "作用于属性时，需要 [ShowInInspector] 配合使用才能显示，但是实际没有序列化保存到文件中",
                "使用场景不多，通常使用 Delayed 即可"
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>();
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(DelayedPropertyExample));
        }
    }
}