using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class DetailedInfoBoxContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "DetailedInfoBox";
        }

        protected override string SetBrief()
        {
            return "可以折叠的信息框";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "可以作用于任何 Property，包括字段，属性，标记绘制的方法",
                "和 InfoBox 类似，但是可以折叠，并分别设置显示的值"
            };
        }

        public override List<ResolvedParam> SetResolvedParams()
        {
            return new List<ResolvedParam>
            {
                new()
                {
                    ParamName = "Message",
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
                    ParamName = "Details",
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
                    ParamName = "VisibleIf",
                    ReturnType = "bool",
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
                }
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>
            {
                new()
                {
                    returnType = "string",
                    paramName = "message",
                    paramDescription = "折叠状态下显示的文本，" + DescriptionConfigs.SupportAllResolver
                },
                new()
                {
                    returnType = "string",
                    paramName = "details",
                    paramDescription = "展开状态下显示的完整文本，" + DescriptionConfigs.SupportAllResolver
                },
                new()
                {
                    returnType = "InfoMessageType",
                    paramName = "infoMessageType",
                    paramDescription = "消息的类型枚举，绘制一个图标在左侧，有 None，Info，Warning，Error"
                },
                new()
                {
                    returnType = "bool",
                    paramName = "visibleIf",
                    paramDescription = "解析字段名，获取 bool 值，用于控制 DetailedInfoBox 当前是否显示，" +
                                       DescriptionConfigs.SupportAllResolver
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(DetailedInfoBoxExample));
        }
    }
}