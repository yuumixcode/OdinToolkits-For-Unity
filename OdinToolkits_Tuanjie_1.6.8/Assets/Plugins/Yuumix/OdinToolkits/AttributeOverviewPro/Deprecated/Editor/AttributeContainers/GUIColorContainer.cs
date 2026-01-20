using System.Collections.Generic;
using Sirenix.OdinInspector.Editor;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class GUIColorContainer : OdinAttributeContainerSO
    {
        protected override string GetHeader() => "GUIColor";

        protected override string GetIntroduction() => "改变 GUI 元素的颜色";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "可以用于突出特殊的，重要的字段",
                "有两种构造函数，第一种是 RGBA 四个参数，第二种是字符串，可以有多种形式",
                "需要引用成员名时，使用 $ 符号作为前缀，告知 Odin 解析字符串，$ 符号在 Rider 中可以自动补全成员名称并高亮"
            };

        public override List<ResolvedParam> GetResolvedParams() =>
            new List<ResolvedParam>
            {
                new ResolvedParam
                {
                    ParamName = "GetColor",
                    ReturnType = nameof(Color),
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
                    ReturnType = "float",
                    ParameterName = "r",
                    ParameterDescription = "红色通道"
                },
                new ParameterValue
                {
                    ReturnType = "float",
                    ParameterName = "g",
                    ParameterDescription = "绿色通道"
                },
                new ParameterValue
                {
                    ReturnType = "float",
                    ParameterName = "b",
                    ParameterDescription = "蓝色通道"
                },
                new ParameterValue
                {
                    ReturnType = "float",
                    ParameterName = "a",
                    ParameterDescription = "Alpha 通道"
                },
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "getColor",
                    ParameterDescription = DescriptionConfigs.ColorDescription
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(GUIColorExample));
    }
}
