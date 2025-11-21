using Sirenix.OdinInspector;
using Sirenix.OdinInspector.Editor;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [AttributeOverviewProExample]
    public class RequiredExample : ExampleSO
    {
        [FoldoutGroup("ErrorMessage 参数 支持多种解析字符串")]
        [LabelWidth(200)]
        public bool useAlternativeMessage;

        [FoldoutGroup("ErrorMessage 参数 支持多种解析字符串")]
        [LabelWidth(200)]
        public string message = "Peace, Love & Ducks";

        [FoldoutGroup("ErrorMessage 参数 支持多种解析字符串")]
        [LabelWidth(200)]
        public string alternativeMessage = "Peace, Love & Yuumi Zeus";

        [FoldoutGroup("ErrorMessage 参数 支持多种解析字符串")]
        [LabelWidth(200)]
        [Required("$message")]
        public string fieldNameExample;

        [FoldoutGroup("ErrorMessage 参数 支持多种解析字符串")]
        [LabelWidth(200)]
        [Required(ErrorMessage = nameof(MessageProperty))]
        public string memberInfoNameExample;

        [FoldoutGroup("ErrorMessage 参数 支持多种解析字符串")]
        [LabelWidth(200)]
        [Required("@useAlternativeMessage ? alternativeMessage : message")]
        public string attributeExpressionExample;

        [FoldoutGroup("ErrorMessage 参数 支持多种解析字符串")]
        [LabelWidth(200)]
        [Required("$GetMessage")]
        public string methodNameExample;

        [FoldoutGroup("ErrorMessage 参数 支持多种解析字符串")]
        [LabelWidth(200)]
        [Required("$GetMessageSuffix")]
        public string methodNameExample2;

        [FoldoutGroup("InfoMessageType 参数 枚举类型")]
        [Required("可以自定义信息等级", InfoMessageType.None)]
        [LabelWidth(200)]
        public GameObject requiredGameObject;

        [FoldoutGroup("InfoMessageType 参数 枚举类型")]
        [Required("可以自定义信息等级", InfoMessageType.Info)]
        [LabelWidth(200)]
        public Rigidbody myRigidbody;

        [FoldoutGroup("InfoMessageType 参数 枚举类型")]
        [Required("可以自定义信息等级", InfoMessageType.Warning)]
        [LabelWidth(200)]
        public ScriptableObject requireScriptableObject;

        [FoldoutGroup("InfoMessageType 参数 枚举类型")]
        [Required("可以自定义信息等级", InfoMessageType.Error)]
        [LabelWidth(200)]
        public ScriptableObject requireScriptableObject2;

        public string MessageProperty => useAlternativeMessage ? alternativeMessage : message;

        string GetMessage() => useAlternativeMessage ? alternativeMessage : message;

#if UNITY_EDITOR
        string GetMessageSuffix(InspectorProperty property, string value) =>
            // value 一定为 "" 空字符串，因为只有 value == 空字符串的时候才会执行
            property.Path + "_" + property.NiceName + "_Suffix";
#endif
        public override void SetDefaultValue()
        {
            fieldNameExample = "";
            memberInfoNameExample = "";
            attributeExpressionExample = "";
            methodNameExample = "";
            methodNameExample2 = "";
            requiredGameObject = null;
            myRigidbody = null;
            requireScriptableObject = null;
            requireScriptableObject2 = null;
            useAlternativeMessage = false;
            message = "Peace, Love & Ducks";
            alternativeMessage = "Peace, Love & Yuumi Zeus";
        }
    }
}
