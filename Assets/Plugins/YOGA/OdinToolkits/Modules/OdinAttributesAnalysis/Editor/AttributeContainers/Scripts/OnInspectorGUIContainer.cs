using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;
using System.Collections.Generic;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class OnInspectorGUIContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "OnInspectorGUI";
        }

        protected override string SetBrief()
        {
            return "可以标记在任何 Property 上，用于额外绘制 GUI";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "通常直接作用于方法上，可以作用于字段或者属性上"
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
                },
                new()
                {
                    returnType = "bool",
                    paramName = "append",
                    paramDescription = "是否绘制在后方，如果为 true, 则绘制在后方，否则绘制在前方"
                },
                new()
                {
                    returnType = "string",
                    paramName = "prepend",
                    paramDescription = "绘制在前方的方法"
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(OnInspectorGUIExample));
        }
    }
}