using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;
using System.Collections.Generic;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class OnInspectorInitContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "OnInspectorInit";
        }

        protected override string SetBrief()
        {
            return "当 Inspector 面板选择时，执行初始化操作";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "此方法至少执行一次，也有可能执行多次，当重新构建 PropertyTree 时，也会触发，比较常见的是多态类型修改值触发",
                "根据字段顺序进行触发初始化方法"
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
                    paramDescription = "触发函数名，方法可选 (InspectorProperty property, T value)，" +
                                       DescriptionConfigs.SupportAllResolver
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(OnInspectorInitExample));
        }
    }
}