using Sirenix.OdinInspector.Editor;
using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Editor;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class DetailedInfoBoxContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "DetailedInfoBox";

        protected override string GetIntroduction() => "可以折叠的信息框";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "可以作用于任何 Property，包括字段，属性，标记绘制的方法",
                "和 InfoBox 类似，但是可以折叠，并分别设置显示的值"
            };

        public override List<ResolvedParam> GetResolvedParams() =>
            new List<ResolvedParam>
            {
                new ResolvedParam
                {
                    ParamName = "Message",
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
                    ParamName = "Details",
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
                    ParamName = "VisibleIf",
                    ReturnType = "bool",
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
                }
            };

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "message",
                    ParameterDescription = "折叠状态下显示的文本，" + DescriptionConfigs.SupportAllResolver
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "details",
                    ParameterDescription = "展开状态下显示的完整文本，" + DescriptionConfigs.SupportAllResolver
                },
                new ParameterValue
                {
                    ReturnType = "InfoMessageType",
                    ParameterName = "infoMessageType",
                    ParameterDescription = "消息的类型枚举，绘制一个图标在左侧，有 None，Info，Warning，Error"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "visibleIf",
                    ParameterDescription = "解析字段名，获取 bool 值，用于控制 DetailedInfoBox 当前是否显示，" +
                                       DescriptionConfigs.SupportAllResolver
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(DetailedInfoBoxExample));
    }
}
