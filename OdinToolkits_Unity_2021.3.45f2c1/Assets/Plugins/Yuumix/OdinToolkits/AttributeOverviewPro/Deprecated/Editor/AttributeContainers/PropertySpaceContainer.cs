using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class PropertySpaceContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "PropertySpace";

        protected override string GetIntroduction() => "控制 Property 中的值的前后间隔";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "PropertySpace 的间隔距离直接作用于 Property 中的值的那一行，不会包括 Group，实际会扩大 Group 的范围"
            };

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "float",
                    ParameterName = "spaceBefore",
                    ParameterDescription = "和上一个 property 的距离，像素为单位"
                },
                new ParameterValue
                {
                    ReturnType = "float",
                    ParameterName = "spaceAfter",
                    ParameterDescription = "和下一个 property 的距离，像素为单位"
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(PropertySpaceExample));
    }
}
