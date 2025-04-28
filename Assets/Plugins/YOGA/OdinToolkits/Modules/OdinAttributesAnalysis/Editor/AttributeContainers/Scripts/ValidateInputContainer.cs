using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class ValidateInputContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "ValidateInput";
        }

        protected override string SetBrief()
        {
            return "对值进行验证，不满足要求则发出提示信息";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "需要使用 ref，第三个为提醒等级，也需要使用 ref，代码示例中查看"
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>
            {
                new()
                {
                    returnType = "string",
                    paramName = "condition",
                    paramDescription = "填入验证方法的方法名，返回 true 则验证通过，否则不通过，" + DescriptionConfigs.SupportAllResolver
                },
                new()
                {
                    returnType = "string",
                    paramName = "defaultMessage",
                    paramDescription = "默认的提醒信息，如果不填则使用默认的，" + DescriptionConfigs.SupportAllResolver
                },
                new()
                {
                    returnType = "InfoMessageType",
                    paramName = "messageType",
                    paramDescription = "提醒等级，绘制一个图标在左侧，有 None，Info，Warning，Error"
                },
                new()
                {
                    returnType = "bool",
                    paramName = "IncludeChildren",
                    paramDescription = "是否该值的子字段修改时也会触发验证，默认为 true"
                },
                new()
                {
                    returnType = "bool",
                    paramName = "ContinuousValidationCheck",
                    paramDescription = "是否每帧都进行验证，而不是仅在修改时"
                }
            };
        }

        public override List<ResolvedParam> SetResolvedParams()
        {
            return new List<ResolvedParam>
            {
                new()
                {
                    ParamName = "Condition",
                    ReturnType = "bool",
                    ResolverType = ResolverType.ValueResolver,
                    ParamValues = new List<ParamValue>
                    {
                        new()
                        {
                            returnType = "message",
                            paramName = "message",
                            paramDescription = "提醒信息，可以关联成员变量的值"
                        },
                        new()
                        {
                            returnType = "InfoMessageType",
                            paramName = "messageType",
                            paramDescription = "提醒等级，绘制一个图标在左侧，有 None，Info，Warning，Error"
                        },
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
                    ParamName = "DefaultMessage",
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
                }
            };
        }

        protected override string SetOriginalCode()
        {
            return ReadCodeWithoutNamespace(typeof(ValidateInputExample));
        }
    }
}