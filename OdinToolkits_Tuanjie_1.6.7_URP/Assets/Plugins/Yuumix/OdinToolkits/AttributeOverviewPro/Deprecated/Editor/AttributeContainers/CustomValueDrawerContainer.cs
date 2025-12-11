using Sirenix.OdinInspector.Editor;
using System.Collections.Generic;
using Yuumix.OdinToolkits.Core.SafeEditor;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;
using YuumixEditor;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    public class CustomValueDrawerContainer : OdinAttributeContainerSO
    {
        protected override BilingualHeaderWidget GetHeaderWidget() => GlobalTempHeader.ModifyWidget(
            "CustomValueDrawer",
            "CustomValueDrawer",
            "对特殊的字段进行自定义绘制",
            "CustomValueDrawer"
        );

        protected override string GetHeader() => "CustomValueDrawer";

        protected override string GetIntroduction() => "对特殊的字段进行自定义绘制";

        protected override List<string> GetTips() =>
            new List<string>
            {
                "唯一的参数是绘制相关的方法名，且方法的返回值必须为自定义绘制字段的类型",
                "注意使用 UNITY_EDITOR 宏定义",
                "自定义绘制的方法最多可以有四个参数，由 Odin 解析获得，方法内部直接使用即可",
                "可以选择是否接入 Odin 的绘制链，默认是不接入的",
                "Odin 提供的 InspectorProperty 类型对象可以获得很多信息，类似于 SerializedProperty"
            };

        public override List<ResolvedParam> GetResolvedParams() =>
            new List<ResolvedParam>
            {
                new ResolvedParam
                {
                    ParamName = "Action",
                    ReturnType = "T 泛型",
                    ResolverType = ResolverType.ValueResolver,
                    ParamValues = new List<ParameterValue>
                    {
                        new ParameterValue
                        {
                            ReturnType = typeof(GUIContent).FullName,
                            ParameterName = "$label",
                            ParameterDescription = "Object Label 通常是指显示在 Inspector 面板上的名称"
                        },
                        new ParameterValue
                        {
                            ReturnType = "Func<GUIContent, bool>",
                            ParameterName = "$callNextDrawer",
                            ParameterDescription = "是否进入 Odin 的下一层绘制，传入 GUIContent 类型的 label"
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
                }
            };

        protected override List<ParameterValue> GetParamValues() =>
            new List<ParameterValue>
            {
                new ParameterValue
                {
                    ReturnType = "string",
                    ParameterName = "action",
                    ParameterDescription = "自定义绘制方法的名称字符串，" + DescriptionConfigs.SupportMemberResolverLite
                }
            };

        protected override string GetOriginalCode() => ReadCodeWithoutNamespace(typeof(CustomValueDrawerExample));
    }
}
