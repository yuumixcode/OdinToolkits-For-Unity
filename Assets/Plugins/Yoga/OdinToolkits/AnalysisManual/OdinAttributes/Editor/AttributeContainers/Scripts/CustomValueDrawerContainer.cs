using Sirenix.OdinInspector.Editor;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public class CustomValueDrawerContainer : AbsContainer
    {
        protected override string SetHeader() => "CustomValueDrawer";

        protected override string SetBrief() => "对特殊的字段进行自定义绘制";

        protected override List<string> SetTip() => new List<string>()
        {
            "唯一的参数是绘制相关的方法名，且方法的返回值必须为自定义绘制字段的类型",
            "注意使用 UNITY_EDITOR 宏定义",
            "自定义绘制的方法最多可以有四个参数，由 Odin 解析获得，方法内部直接使用即可",
            "可以选择是否接入 Odin 的绘制链，默认是不接入的",
            "Odin 提供的 InspectorProperty 类型对象可以获得很多信息，类似于 SerializedProperty"
        };

        public override List<ResolvedParam> SetResolvedParams() =>
            new List<ResolvedParam>()
            {
                new ResolvedParam()
                {
                    ParamName = "Action",
                    ReturnType = "T 泛型",
                    ResolverType = ResolverType.ValueResolver,
                    ParamValues = new List<ParamValue>()
                    {
                        new ParamValue()
                        {
                            returnType = typeof(GUIContent).FullName,
                            paramName = "$label",
                            paramDescription = "Object Label 通常是指显示在 Inspector 面板上的名称"
                        },
                        new ParamValue()
                        {
                            returnType = "Func<GUIContent, bool>",
                            paramName = "$callNextDrawer",
                            paramDescription = "是否进入 Odin 的下一层绘制，传入 GUIContent 类型的 label"
                        },
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
                returnType = "string",
                paramName = "action",
                paramDescription = "自定义绘制方法的名称字符串，" + DescriptionConfigs.SupportMemberResolverLite
            }
        };

        protected override string SetOriginalCode() => ReadCodeWithoutNamespace(typeof(CustomValueDrawerExample));
    }
}
