using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;
using System.Collections.Generic;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class BoxGroupContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "BoxGroup";
        }

        protected override string SetBrief()
        {
            return "将 Property 使用 Box 样式进行分组";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "可以和其他 Group 特性连接，共享分组路径"
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>
            {
                new()
                {
                    returnType = "string",
                    paramName = "group",
                    paramDescription = "分组路径，可以和其他 Group 连接使用"
                },
                new()
                {
                    returnType = "bool",
                    paramName = "showLabel",
                    paramDescription = "是否显示分组标题"
                },
                new()
                {
                    returnType = "string",
                    paramName = "LabelText",
                    paramDescription = "自定义分组标题"
                },
                new()
                {
                    returnType = "bool",
                    paramName = "centerLabel",
                    paramDescription = "是否居中显示标题"
                },
                new()
                {
                    returnType = "int",
                    paramName = "order",
                    paramDescription =
                        "是不同 Group 在 Inspector 面板上的排序，从 PropertyGroupAttribute 基类继承获得的变量，比 PropertyOrder 优先级更高"
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(BoxGroupExample));
        }
    }
}