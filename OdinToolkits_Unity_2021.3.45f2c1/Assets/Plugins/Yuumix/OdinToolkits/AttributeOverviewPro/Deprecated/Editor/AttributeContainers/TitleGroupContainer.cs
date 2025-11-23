using Sirenix.OdinInspector;
using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class TitleGroupContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "TitleGroup";

        protected override string GetIntroduction() => "将任意 Property 以标题为核心进行分组绘制";

        protected override List<string> GetTips() => new List<string>();

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "title",
                    ParameterDescription = "标题名，同时也是 Group 路径"
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "subtitle",
                    ParameterDescription = "子标题"
                },
                new ParameterValue
                {
                    ReturnType = nameof(TitleAlignments),
                    ParameterName = "alignment",
                    ParameterDescription = "对齐方式，和 Title 一样"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "horizontalLine",
                    ParameterDescription = "是否绘制水平线"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "boldTitle",
                    ParameterDescription = "是否粗体绘制"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "indent",
                    ParameterDescription = "是否缩进所有组成员，默认为 false"
                },
                new ParameterValue
                {
                    ReturnType = "float",
                    ParameterName = "order",
                    ParameterDescription = "和其他 Group 的排序，默认为 0"
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(TitleGroupExample));
    }
}
