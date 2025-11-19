using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Editor;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class OnInspectorDisposeContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "OnInspectorDispose";

        protected override string GetIntroduction() => "设置 Inspector 面板的 Dispose 方法，通常是用于结束显示时的处理方法";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "当更换 Inspector 面板选择或垃圾收集器收集 PropertyTree 时，至少触发一次，也有可能触发多次，最常见的是多态 Property 类型发生改变时，会触发 Dispose"
            };

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "action",
                    ParameterDescription = "触发函数名，方法可选 (InspectorProperty property, T value)，无返回值，" +
                                       DescriptionConfigs.SupportAllResolver
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(OnInspectorDisposeExample));
    }
}
