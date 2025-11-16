using Sirenix.OdinInspector.Editor;
using System.Collections.Generic;

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

        protected override List<ParamValue> GetParamValues() =>
            new List<ParamValue>
            {
                new ParamValue
                {
                    returnType = "string",
                    paramName = "condition",
                    paramDescription = "填入验证方法的方法名，返回 true 则验证通过，否则不通过，" + DescriptionConfigs.SupportAllResolver
                },
                new ParamValue
                {
                    returnType = "string",
                    paramName = "defaultMessage",
                    paramDescription = "默认的提醒信息，如果不填则使用默认的，" + DescriptionConfigs.SupportAllResolver
                },
                new ParamValue
                {
                    returnType = "InfoMessageType",
                    paramName = "messageType",
                    paramDescription = "提醒等级，绘制一个图标在左侧，有 None，Info，Warning，Error"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "IncludeChildren",
                    paramDescription = "是否该值的子字段修改时也会触发验证，默认为 true"
                },
                new ParamValue
                {
                    returnType = "bool",
                    paramName = "ContinuousValidationCheck",
                    paramDescription = "是否每帧都进行验证，而不是仅在修改时"
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
                    ParamValues = new List<ParamValue>
                    {
                        new ParamValue
                        {
                            returnType = "message",
                            paramName = "message",
                            paramDescription = "提醒信息，可以关联成员变量的值"
                        },
                        new ParamValue
                        {
                            returnType = "InfoMessageType",
                            paramName = "messageType",
                            paramDescription = "提醒等级，绘制一个图标在左侧，有 None，Info，Warning，Error"
                        },
                        new ParamValue
                        {
                            returnType = typeof(InspectorProperty).FullName,
                            paramName = "$property",
                            paramDescription = DescriptionConfigs.InspectorPropertyDesc
                        },
                        new ParamValue
                        {
                            returnType = "T 泛型",
                            paramName = "$value",
                            paramDescription = DescriptionConfigs.ValueDesc
                        }
                    }
                },
                new ResolvedParam
                {
                    ParamName = "DefaultMessage",
                    ReturnType = "string",
                    ResolverType = ResolverType.ValueResolver,
                    ParamValues = new List<ParamValue>
                    {
                        new ParamValue
                        {
                            returnType = typeof(InspectorProperty).FullName,
                            paramName = "$property",
                            paramDescription = DescriptionConfigs.InspectorPropertyDesc
                        },
                        new ParamValue
                        {
                            returnType = "T 泛型",
                            paramName = "$value",
                            paramDescription = DescriptionConfigs.ValueDesc
                        }
                    }
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(ValidateInputExample));
    }
}
