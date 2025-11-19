using Sirenix.OdinInspector.Editor;
using System;
using System.Collections.Generic;
using Yuumix.OdinToolkits.AttributeOverviewPro.Editor;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    public class RequiredContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "Required";

        protected override string GetIntroduction() => "要求字段不能为空，否则发出错误提示";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "用于标记关键的值，使其在运行前不能为空",
                "自定义错误信息可以解析成员名称字符串，$ 符号作为前缀解析成员名称，$ 符号在 Rider 中可以自动补全成员名称并高亮",
                "自定义错误信息还可以解析表达式，@ 符号作为前缀表示解析表达式，@ 是字符串的一部分，后面直接编写返回特定值的表达式",
                "优先使用构造函数，而不是 (ErrorMessage = )，构造函数可以在 Rider 中高亮显示被解析的字符串。"
            };

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "errorMessage",
                    ParameterDescription = "错误信息，" + DescriptionConfigs.SupportAllResolver
                },
                new ParameterValue
                {
                    ReturnType = "InfoMessageType",
                    ParameterName = "infoMessageType",
                    ParameterDescription = "消息的类型枚举，绘制一个图标在左侧，有 None，Info，Warning，Error"
                }
            };

        public override List<ResolvedParam> GetResolvedParams() =>
            new List<ResolvedParam>
            {
                new ResolvedParam
                {
                    ParamName = "Message",
                    ReturnType = nameof(String),
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

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(RequiredExample));
    }
}
