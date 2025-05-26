using System.Collections.Generic;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class OnInspectorInitContainer : AbsContainer
    {
        protected override string SetHeader() => "OnInspectorInit";

        protected override string SetBrief() => "当 Inspector 面板选择时，执行初始化操作";

        protected override List<string> SetTip() =>
            new List<string>
            {
                "此方法至少执行一次，也有可能执行多次，当重新构建 PropertyTree 时，也会触发，比较常见的是多态类型修改值触发",
                "根据字段顺序进行触发初始化方法"
            };

        protected override List<ParamValue> SetParamValues() =>
            new List<ParamValue>
            {
                new ParamValue
                {
                    returnType = "string",
                    paramName = "action",
                    paramDescription = "触发函数名，方法可选 (InspectorProperty property, T value)，" +
                                       DescriptionConfigs.SupportAllResolver
                }
            };

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(OnInspectorInitExample));
    }
}
