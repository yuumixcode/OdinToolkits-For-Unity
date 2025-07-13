using System;
using System.Collections.Generic;
using System.Reflection;
using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;

namespace Yuumix.OdinToolkits.Editor
{
    [OdinToolkitsAttributeExample]
    public class DetailedInfoBoxExample : ExampleSO
    {
        const string DetailsYoga = "YOGA 框架 == Yuumi Odin Graphic Architecture";

        const string MessageYoga = "YOGA 框架 ...";

        [FoldoutGroup("Message 参数 支持多种解析字符串")]
        public string message = "YOGA 框架 ...";

        [FoldoutGroup("Message 参数 支持多种解析字符串")]
        [DetailedInfoBox("@message", DetailsYoga)]
        public string messageAttributeExpressionExample;

        [FoldoutGroup("Message 参数 支持多种解析字符串")]
        [DetailedInfoBox("$message", DetailsYoga)]
        public string messageFieldNameExample;

        [FoldoutGroup("Message 参数 支持多种解析字符串")]
        [DetailedInfoBox("$GetMessage", DetailsYoga)]
        public string messageMethodNameExample;

        [FoldoutGroup("Message 参数 支持多种解析字符串")]
        [DetailedInfoBox("$MessageProperty", DetailsYoga)]
        public string messagePropertyNameExample;

        [FoldoutGroup("Message 参数 支持多种解析字符串")]
        public string details = "YOGA 框架 == Yuumi Odin Graphic Architecture";

        [FoldoutGroup("Details 参数 支持多种解析字符串")]
        [DetailedInfoBox(MessageYoga, "@details")]
        public string detailsAttributeExpressionExample;

        [FoldoutGroup("Details 参数 支持多种解析字符串")]
        [DetailedInfoBox(MessageYoga, "$details")]
        public string detailsFieldNameExample;

        [FoldoutGroup("Details 参数 支持多种解析字符串")]
        [DetailedInfoBox(MessageYoga, "$GetDetails")]
        public string detailsMethodNameExample;

        [FoldoutGroup("Details 参数 支持多种解析字符串")]
        [DetailedInfoBox(MessageYoga, "$DetailsProperty")]
        public string detailsPropertyNameExample;

        [FoldoutGroup("VisibleIf 参数 支持多种解析字符串")]
        public bool isVisible = true;

        [FoldoutGroup("VisibleIf 参数 支持多种解析字符串")]
        [DetailedInfoBox(MessageYoga, DetailsYoga, VisibleIf = "@isVisible")]
        public string visibleIfAttributeExpressionExample;

        [FoldoutGroup("VisibleIf 参数 支持多种解析字符串")]
        [DetailedInfoBox(MessageYoga, DetailsYoga, VisibleIf = nameof(isVisible))]
        public string visibleIfFieldNameExample;

        [FoldoutGroup("VisibleIf 参数 支持多种解析字符串")]
        [DetailedInfoBox(MessageYoga, DetailsYoga, VisibleIf = nameof(GetVisibility))]
        public string visibleIfMethodNameExample;

        [FoldoutGroup("VisibleIf 参数 支持多种解析字符串")]
        [DetailedInfoBox(MessageYoga, DetailsYoga, VisibleIf = nameof(IsVisibleProperty))]
        public string visibleIfPropertyNameExample;

        [FoldoutGroup("InfoMessageType 参数 枚举类型")]
        [DetailedInfoBox(MessageYoga, DetailsYoga)]
        public string infoMessageTypeDetailedInfoBox1;

        [FoldoutGroup("InfoMessageType 参数 枚举类型")]
        [DetailedInfoBox(MessageYoga, DetailsYoga, InfoMessageType.None)]
        public string infoMessageTypeDetailedInfoBox2;

        [FoldoutGroup("InfoMessageType 参数 枚举类型")]
        [DetailedInfoBox(MessageYoga, DetailsYoga, InfoMessageType.Warning)]
        public string infoMessageTypeDetailedInfoBox3;

        [FoldoutGroup("InfoMessageType 参数 枚举类型")]
        [DetailedInfoBox(MessageYoga, DetailsYoga, InfoMessageType.Error)]
        public string infoMessageTypeDetailedInfoBox4;

        string MessageProperty => message;

        public string DetailsProperty => details;

        public bool IsVisibleProperty => isVisible;

        [PropertyOrder(30)]
        [ShowInInspector]
        [FoldoutGroup("DetailedInfoBox 扩展")]
        [DetailedInfoBox("DetailedInfoBox 可以用于属性...",
            "DetailedInfoBox 完整部分，可以用于属性")]
        public string Property { get; set; }

        string GetMessage() => message;

        string GetDetails() => details;

        bool GetVisibility() => isVisible;

        [PropertyOrder(40)]
        [FoldoutGroup("DetailedInfoBox 扩展")]
        [DetailedInfoBox("DetailedInfoBox 可以用在绘制方法上...",
            "DetailedInfoBox 完整部分，用于标记绘制的方法")]
        [OnInspectorGUI]
        public void OnGUI()
        {
            GUILayout.Label("绘制方法");
        }

        public override void SetDefaultValue()
        {
            message = "YOGA 框架 ...";
            details = "YOGA 框架 == Yuumi Odin Graphic Architecture";
            isVisible = true;
            Property = "";
        }
    }

#if UNITY_EDITOR // Processor 方法需要在编辑器下才能使用，此时位于 Editor 文件夹可以不需要
    public class DetailedInfoBoxDrawerProcessor : OdinAttributeProcessor<DetailedInfoBoxExample>
    {
        public override void ProcessChildMemberAttributes(InspectorProperty parentProperty, MemberInfo member,
            List<Attribute> attributes)
        {
            attributes.Add(new LabelWidthAttribute(250));
            if (member.Name == "isVisible")
            {
                attributes.Add(new LabelTextAttribute("控制是否显示 DetailedInfoBox"));
            }
            else if (member.Name == "Property")
            {
                attributes.Add(new LabelTextAttribute("属性"));
            }
        }
    }
#endif
}
