using System.Collections.Generic;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class PropertySpaceContainer : AbsContainer
    {
        protected override string SetHeader() => "PropertySpace";

        protected override string SetBrief() => "控制 Property 中的值的前后间隔";

        protected override List<string> SetTip() =>
            new List<string>
            {
                "PropertySpace 的间隔距离直接作用于 Property 中的值的那一行，不会包括 Group，实际会扩大 Group 的范围"
            };

        protected override List<ParamValue> SetParamValues() =>
            new List<ParamValue>
            {
                new ParamValue
                {
                    returnType = "float",
                    paramName = "spaceBefore",
                    paramDescription = "和上一个 property 的距离，像素为单位"
                },
                new ParamValue
                {
                    returnType = "float",
                    paramName = "spaceAfter",
                    paramDescription = "和下一个 property 的距离，像素为单位"
                }
            };

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(PropertySpaceExample));
    }
}
