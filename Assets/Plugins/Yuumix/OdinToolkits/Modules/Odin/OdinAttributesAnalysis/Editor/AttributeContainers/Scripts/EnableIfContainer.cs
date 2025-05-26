using System.Collections.Generic;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class EnableIfContainer : AbsContainer
    {
        protected override string SetHeader() => "EnableIf";

        protected override string SetBrief() => "满足条件时，Property 才可获取焦点";

        protected override List<string> SetTip() => new List<string>();

        protected override List<ParamValue> SetParamValues() =>
            new List<ParamValue>
            {
                new ParamValue
                {
                    returnType = "string",
                    paramName = "condition",
                    paramDescription = "成员名，" + DescriptionConfigs.SupportAllResolver
                },
                new ParamValue
                {
                    returnType = "object",
                    paramName = "optionalValue",
                    paramDescription = "成员名的值，需要与 condition 参数配合使用，如果成员的值 == optionalValue，则满足条件"
                }
            };

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(EnableIfExample));
    }
}
