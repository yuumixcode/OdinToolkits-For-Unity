using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class TitleContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "Title";
        }

        protected override string SetBrief()
        {
            return "为任意 Property 添加标题，类似于 Unity 的 Header 特性";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "Title 和 Header 类似，属于直接绘制，Odin 还提供了一个 TitleGroup 用于分组"
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>
            {
                new()
                {
                    returnType = "string",
                    paramName = "title",
                    paramDescription = "标题字符串，" + DescriptionConfigs.SupportAllResolver
                },
                new()
                {
                    returnType = "string",
                    paramName = "subtitle",
                    paramDescription = "子标题字符串，" + DescriptionConfigs.SupportAllResolver
                },
                new()
                {
                    returnType = "TitleAlignments",
                    paramName = "titleAlignment",
                    paramDescription = "标题对齐方式，默认为左对齐，共有四个枚举，TitleAlignments.Left，TitleAlignments.Centered，" +
                                       "TitleAlignments.Right，以及 TitleAlignments.Split，Split 表示左边为标题，右边为子标题"
                },
                new()
                {
                    returnType = "bool",
                    paramName = "horizontalLine",
                    paramDescription = "是否显示分割线，默认为 true"
                },
                new()
                {
                    returnType = "bool",
                    paramName = "bold",
                    paramDescription = "是否加粗，默认为 true"
                }
            };
        }

        public override List<ResolvedParam> SetResolvedParams()
        {
            return new List<ResolvedParam>
            {
                new()
                {
                    ParamName = "Title",
                    ReturnType = "string",
                    ResolverType = ResolverType.ValueResolver,
                    ParamValues = new List<ParamValue>
                    {
                        new()
                        {
                            returnType = typeof(InspectorProperty).FullName,
                            paramName = "$property",
                            paramDescription = DescriptionConfigs.InspectorPropertyDesc
                        },
                        new()
                        {
                            returnType = "T 泛型",
                            paramName = "$value",
                            paramDescription = DescriptionConfigs.ValueDesc
                        }
                    }
                },
                new()
                {
                    ParamName = "Subtitle",
                    ReturnType = "string",
                    ResolverType = ResolverType.ValueResolver,
                    ParamValues = new List<ParamValue>
                    {
                        new()
                        {
                            returnType = typeof(InspectorProperty).FullName,
                            paramName = "$property",
                            paramDescription = DescriptionConfigs.InspectorPropertyDesc
                        },
                        new()
                        {
                            returnType = "T泛型",
                            paramName = "$value",
                            paramDescription = DescriptionConfigs.ValueDesc
                        }
                    }
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(TitleExample));
        }
    }
}