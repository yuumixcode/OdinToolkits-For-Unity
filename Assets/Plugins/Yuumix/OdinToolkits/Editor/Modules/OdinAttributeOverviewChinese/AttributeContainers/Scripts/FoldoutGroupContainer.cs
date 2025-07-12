using System.Collections.Generic;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class FoldoutGroupContainer : AbsContainer
    {
        protected override string SetHeader() => "FoldoutGroup";

        protected override string SetBrief() => "将 Property 以 Foldout 的形式分组，可以折叠";

        protected override List<string> SetTip() =>
            new List<string>
                { "可以和其他 Group 特性连接，共享分组路径" };

        protected override List<ParamValue> SetParamValues() =>
            new List<ParamValue>
            {
                new ParamValue
                {
                    returnType = "string",
                    paramName = "groupName",
                    paramDescription = "分组名称"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "expanded",
                    paramDescription = "是否设置默认状态为展开"
                },
                new ParamValue
                {
                    returnType = "int",
                    paramName = "order",
                    paramDescription =
                        "是不同 Group 在 Inspector 面板上的排序，从 PropertyGroupAttribute 基类继承获得的变量，比 PropertyOrder 优先级更高"
                }
            };

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(FoldoutGroupExample));
    }
}
