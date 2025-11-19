using Sirenix.OdinInspector.Editor;
using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Editor;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class ValidateInputContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "ValidateInput";

        protected override string GetIntroduction() => "对值进行验证，不满足要求则发出提示信息";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "需要使用 ref，第三个为提醒等级，也需要使用 ref，代码示例中查看"
            };

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "condition",
                    ParameterDescription = "填入验证方法的方法名，返回 true 则验证通过，否则不通过，" + DescriptionConfigs.SupportAllResolver
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "defaultMessage",
                    ParameterDescription = "默认的提醒信息，如果不填则使用默认的，" + DescriptionConfigs.SupportAllResolver
                },
                new ParameterValue
                {
                    ReturnType = "InfoMessageType",
                    ParameterName = "messageType",
                    ParameterDescription = "提醒等级，绘制一个图标在左侧，有 None，Info，Warning，Error"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "IncludeChildren",
                    ParameterDescription = "是否该值的子字段修改时也会触发验证，默认为 true"
                },
                new ParameterValue
                {
                    ReturnType = "bool",
                    ParameterName = "ContinuousValidationCheck",
                    ParameterDescription = "是否每帧都进行验证，而不是仅在修改时"
                }
            };

        public override List<ResolvedParam> GetResolvedParams() =>
            new List<ResolvedParam>
            {
                new ResolvedParam
                {
                    ParamName = "Condition",
                    ReturnType = "bool",
                    ResolverType = ResolverType.ValueResolver,
                    ParamValues = new List<ParameterValue>
                    {
                        new ParameterValue
                        {
                            ReturnType = "message",
                            ParameterName = "message",
                            ParameterDescription = "提醒信息，可以关联成员变量的值"
                        },
                        new ParameterValue
                        {
                            ReturnType = "InfoMessageType",
                            ParameterName = "messageType",
                            ParameterDescription = "提醒等级，绘制一个图标在左侧，有 None，Info，Warning，Error"
                        },
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
                    ParamName = "DefaultMessage",
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
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(ValidateInputExample));
    }
}
