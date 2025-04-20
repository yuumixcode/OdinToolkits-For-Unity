using Sirenix.OdinInspector;
using System;
using UnityEngine;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    [IsChineseAttributeExample]
    public class ValidateInputExample : ExampleScriptableObject
    {
        [FoldoutGroup("Condition 参数 用于验证值")]
        [ValidateInput("@!string.IsNullOrWhiteSpace($value)", "Field can't be empty")]
        [LabelWidth(250)]
        public string conditionAttributeExpressionExample;

        [FoldoutGroup("Condition 参数 用于验证值")]
        [InfoBox("如果想要通过 Condition 方法设置 ValidateInputAttribute 的其他值的话，需要使用 ref 引用，否则无法覆盖默认值")]
        [ValidateInput("IsValid")]
        [LabelWidth(250)]
        public string conditionMethodNameExample;

        [FoldoutGroup("DefaultMessage 参数 支持多种解析字符串")]
        [LabelWidth(250)]
        public string alternativeMessage = "YOGA";

        [FoldoutGroup("DefaultMessage 参数 支持多种解析字符串")]
        [LabelWidth(250)]
        public string setMessage = "SOAP";

        [FoldoutGroup("DefaultMessage 参数 支持多种解析字符串")]
        [LabelWidth(250)]
        public bool useAlternativeMessage = true;

        [FoldoutGroup("DefaultMessage 参数 支持多种解析字符串")]
        [ValidateInput("@false",
            "@useAlternativeMessage ? alternativeMessage : setMessage",
            ContinuousValidationCheck = true)]
        [LabelWidth(250)]
        public string defaultMessageAttributeExpressionExample;

        [FoldoutGroup("DefaultMessage 参数 支持多种解析字符串")]
        [ValidateInput("@false", "$setMessage", ContinuousValidationCheck = true)]
        [LabelWidth(250)]
        public string defaultMessageFieldNameExample;

        [FoldoutGroup("DefaultMessage 参数 支持多种解析字符串")]
        [LabelWidth(250)]
        [ValidateInput("@false", "$GetMessage", ContinuousValidationCheck = true)]
        public string defaultMessageMethodNameExample;

        [FoldoutGroup("DefaultMessage 参数 支持多种解析字符串")]
        [LabelWidth(250)]
        [ValidateInput("@false", nameof(MessageProperty), ContinuousValidationCheck = true)]
        public string defaultMessagePropertyNameExample;

        [FoldoutGroup("InfoMessageType 参数 枚举类型")]
        [ValidateInput("@false", "默认为 Error 类型")]
        [LabelWidth(250)]
        public string infoMessageTypeExampleError;

        [FoldoutGroup("InfoMessageType 参数 枚举类型")]
        [ValidateInput("@false", "@false", InfoMessageType.None)]
        [LabelWidth(250)]
        public string infoMessageTypeExampleNone;

        [FoldoutGroup("InfoMessageType 参数 枚举类型")]
        [ValidateInput("@false", "@false", InfoMessageType.Info)]
        [LabelWidth(250)]
        public string infoMessageTypeExampleInfo;

        [FoldoutGroup("InfoMessageType 参数 枚举类型")]
        [ValidateInput("@false", "@false", InfoMessageType.Warning)]
        [LabelWidth(250)]
        public string infoMessageTypeExampleWarning;

        [FoldoutGroup("IncludeChildren 参数")]
        [ValidateInput("ChildrenIsValid", "Child 的 name 为 null", IncludeChildren = true)]
        [InfoBox("该参数几乎没有意义，只要能看到这个字段，那么就会自动执行验证，不一定需要有变化才验证")]
        public TestIncludeChildren includeChildren;

        [FoldoutGroup("ContinuousValidationCheck 参数")]
        [ValidateInput("IsContinuousValidation", "强制持续验证", ContinuousValidationCheck = true)]
        [InfoBox("强制持续验证，每秒多次，比默认的自动验证频率更高")]
        [LabelWidth(250)]
        public string continuousValidationCheck;

        [FoldoutGroup("ValidateInput 扩展")]
        [Title("动态修改消息", "可以根据传入的值来修改信息")]
        [InfoBox("可以自定义 message 信息，需要在验证方法中添加 ref string errorMessage 形参")]
        [ValidateInput("HasMeshRendererDynamicMessage")]
        [LabelWidth(250)]
        public GameObject dynamicMessage;

        [FoldoutGroup("ValidateInput 扩展")]
        [Title("动态修改消息类型")]
        [LabelWidth(250)]
        public InfoMessageType messageTypeChanged;

        [FoldoutGroup("ValidateInput 扩展")]
        [InfoBox("必须有 MeshRenderer 组件，可以使用一个字段值来动态变换信息类型，同时也可以自定义 message ")]
        [ValidateInput("HasMeshRendererDynamicMessageAndType")]
        [LabelWidth(250)]
        public GameObject dynamicMessageAndType;

        public string MessageProperty => useAlternativeMessage ? alternativeMessage : setMessage;

        // 如果想要通过 Condition 方法设置 ValidateInputAttribute 的其他值的话，需要使用 ref 引用，否则无法覆盖默认值
        bool IsValid(string value, string thisMessage, ref InfoMessageType messageType)
        {
            thisMessage = "Field can't be empty";
            messageType = InfoMessageType.Error;

            return !string.IsNullOrWhiteSpace(value);
        }

        string GetMessage() => useAlternativeMessage ? alternativeMessage : setMessage;

        bool ChildrenIsValid(TestIncludeChildren testIncludeChildren)
        {
            if (!string.IsNullOrWhiteSpace(testIncludeChildren.child.name))
            {
                return true;
            }

            Debug.Log("TestIncludeChildren 验证一次");
            return false;
        }

        bool IsContinuousValidation(string value)
        {
            Debug.Log("ContinuousValidation 验证一次");
            return !string.IsNullOrWhiteSpace(value);
        }

        bool HasMeshRendererDynamicMessage(GameObject gameObject, ref string errorMessage)
        {
            if (gameObject == null)
            {
                return true;
            }

            if (gameObject.GetComponentInChildren<MeshRenderer>() != null)
            {
                return true;
            }

            // 如果 errorMessage == null，则使用默认的错误消息
            errorMessage = "\"" + gameObject.name + "\" must have a MeshRenderer component";

            return false;
        }

        bool HasMeshRendererDynamicMessageAndType(GameObject gameObject, ref string errorMessage,
            ref InfoMessageType messageType)
        {
            if (gameObject == null)
            {
                return true;
            }

            if (gameObject.GetComponentInChildren<MeshRenderer>() == null)
            {
                // 如果 errorMessage == null，则使用默认的错误消息
                errorMessage = "\"" + gameObject.name + "\" should have a MeshRenderer component";
                // 如果 messageType == null，则使用默认的信息类型
                messageType = messageTypeChanged;

                return false;
            }

            return true;
        }

        [Serializable]
        public class TestIncludeChildren
        {
            public Child child;
        }

        [Serializable]
        public class Child
        {
            public string name;
            public int num;
        }
    }
}
