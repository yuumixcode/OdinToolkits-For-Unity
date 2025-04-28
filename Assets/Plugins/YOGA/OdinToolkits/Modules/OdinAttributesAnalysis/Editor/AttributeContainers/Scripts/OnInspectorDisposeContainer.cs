using System.Collections.Generic;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class OnInspectorDisposeContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "OnInspectorDispose";
        }

        protected override string SetBrief()
        {
            return "设置 Inspector 面板的 Dispose 方法，通常是用于结束显示时的处理方法";
        }


        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "当更换 Inspector 面板选择或垃圾收集器收集 PropertyTree 时，至少触发一次，也有可能触发多次，最常见的是多态 Property 类型发生改变时，会触发 Dispose"
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>
            {
                new()
                {
                    returnType = "string",
                    paramName = "action",
                    paramDescription = "触发函数名，方法可选 (InspectorProperty property, T value)，无返回值，" +
                                       DescriptionConfigs.SupportAllResolver
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(OnInspectorDisposeExample));
        }
    }
}