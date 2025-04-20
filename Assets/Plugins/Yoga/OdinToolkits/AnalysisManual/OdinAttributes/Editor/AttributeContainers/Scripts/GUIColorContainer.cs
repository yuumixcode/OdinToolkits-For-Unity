using Sirenix.OdinInspector.Editor;
using System.Collections.Generic;
using UnityEngine;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class GUIColorContainer : AbsContainer
    {
        protected override string SetHeader() => "GUIColor";

        protected override string SetBrief() => "改变 GUI 元素的颜色";

        protected override List<string> SetTip() => new List<string>()
        {
            "可以用于突出特殊的，重要的字段",
            "有两种构造函数，第一种是 RGBA 四个参数，第二种是字符串，可以有多种形式",
            "需要引用成员名时，使用 $ 符号作为前缀，告知 Odin 解析字符串，$ 符号在 Rider 中可以自动补全成员名称并高亮"
        };

        public override List<ResolvedParam> SetResolvedParams() =>
            new List<ResolvedParam>()
            {
                new ResolvedParam()
                {
                    ParamName = "GetColor",
                    ReturnType = nameof(Color),
                    ResolverType = ResolverType.ValueResolver,
                    ParamValues = new List<ParamValue>()
                    {
                        new ParamValue()
                        {
                            returnType = typeof(InspectorProperty).FullName,
                            paramName = "$property",
                            paramDescription = DescriptionConfigs.InspectorPropertyDesc
                        },
                        new ParamValue()
                        {
                            returnType = "T 泛型",
                            paramName = "$value",
                            paramDescription = DescriptionConfigs.ValueDesc
                        }
                    }
                }
            };

        protected override List<ParamValue> SetParamValues() => new List<ParamValue>()
        {
            new ParamValue()
            {
                returnType = "float",
                paramName = "r",
                paramDescription = "红色通道"
            },
            new ParamValue()
            {
                returnType = "float",
                paramName = "g",
                paramDescription = "绿色通道"
            },
            new ParamValue()
            {
                returnType = "float",
                paramName = "b",
                paramDescription = "蓝色通道"
            },
            new ParamValue()
            {
                returnType = "float",
                paramName = "a",
                paramDescription = "Alpha 通道"
            },
            new ParamValue()
            {
                returnType = "string",
                paramName = "getColor",
                paramDescription = DescriptionConfigs.ColorDescription
            }
        };

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(GUIColorExample));
    }
}
