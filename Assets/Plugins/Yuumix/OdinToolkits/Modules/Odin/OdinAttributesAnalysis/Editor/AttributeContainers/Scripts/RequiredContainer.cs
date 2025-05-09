using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts;
using Sirenix.OdinInspector.Editor;
using System;
using System.Collections.Generic;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributeContainers.Scripts
{
    public class RequiredContainer : AbsContainer
    {
        protected override string SetHeader()
        {
            return "Required";
        }

        protected override string SetBrief()
        {
            return "要求字段不能为空，否则发出错误提示";
        }

        protected override List<string> SetTip()
        {
            return new List<string>
            {
                "用于标记关键的值，使其在运行前不能为空",
                "自定义错误信息可以解析成员名称字符串，$ 符号作为前缀解析成员名称，$ 符号在 Rider 中可以自动补全成员名称并高亮",
                "自定义错误信息还可以解析表达式，@ 符号作为前缀表示解析表达式，@ 是字符串的一部分，后面直接编写返回特定值的表达式",
                "优先使用构造函数，而不是 (ErrorMessage = )，构造函数可以在 Rider 中高亮显示被解析的字符串。"
            };
        }

        protected override List<ParamValue> SetParamValues()
        {
            return new List<ParamValue>
            {
                new()
                {
                    returnType = "string",
                    paramName = "errorMessage",
                    paramDescription = "错误信息，" + DescriptionConfigs.SupportAllResolver
                },
                new()
                {
                    returnType = "InfoMessageType",
                    paramName = "infoMessageType",
                    paramDescription = "消息的类型枚举，绘制一个图标在左侧，有 None，Info，Warning，Error"
                }
            };
        }

        public override List<ResolvedParam> SetResolvedParams()
        {
            return new List<ResolvedParam>
            {
                new()
                {
                    ParamName = "Message",
                    ReturnType = nameof(String),
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
            return ReadCodeWithoutNamespace(typeof(RequiredExample));
        }
    }
}