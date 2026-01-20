using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class TitleContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "Title";

        protected override string GetIntroduction() => "为任意 Property 添加标题，类似于 Unity 的 Header 特性";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "Title 和 Header 类似，属于直接绘制，Odin 还提供了一个 TitleGroup 用于分组"
            };

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "title",
                    ParameterDescription = "标题字符串，" + DescriptionConfigs.SupportAllResolver
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "subtitle",
                    ParameterDescription = "子标题字符串，" + DescriptionConfigs.SupportAllResolver
                },
                new ParameterValue
                {
                    ReturnType = "TitleAlignments",
                    ParameterName = "titleAlignment",
                    ParameterDescription =
                        "标题对齐方式，默认为左对齐，共有四个枚举，TitleAlignments.Left，TitleAlignments.Centered，" +
                        "TitleAlignments.Right，以及 TitleAlignments.Split，Split 表示左边为标题，右边为子标题"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "horizontalLine",
                    ParameterDescription = "是否显示分割线，默认为 true"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "bold",
                    ParameterDescription = "是否加粗，默认为 true"
                }
            };

        public override List<ResolvedParam> GetResolvedParams() =>
            new List<ResolvedParam>
            {
                new ResolvedParam
                {
                    ParamName = "Title",
                    ReturnType = "string",
                    ResolverType = ResolverType.ValueResolver,
                    ParamValues = new List<ParameterValue>
                    {
                        new ParameterValue
                        {
                            ReturnType = typeof(InspectorProperty).FullName,
                            ParameterName = "$property",
                            ParameterDescription = DescriptionConfigs.InspectorPropertyDesc
                        },
                        new ParameterValue
                        {
                            ReturnType = "T 泛型",
                            ParameterName = "$value",
                            ParameterDescription = DescriptionConfigs.ValueDesc
                        }
                    }
                },
                new ResolvedParam
                {
                    ParamName = "Subtitle",
                    ReturnType = "string",
                    ResolverType = ResolverType.ValueResolver,
                    ParamValues = new List<ParameterValue>
                    {
                        new ParameterValue
                        {
                            ReturnType = typeof(InspectorProperty).FullName,
                            ParameterName = "$property",
                            ParameterDescription = DescriptionConfigs.InspectorPropertyDesc
                        },
                        new ParameterValue
                        {
                            ReturnType = "T泛型",
                            ParameterName = "$value",
                            ParameterDescription = DescriptionConfigs.ValueDesc
                        }
                    }
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(TitleExample));
    }
}
