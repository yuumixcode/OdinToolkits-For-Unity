using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class ShowIfExample : ExampleScriptableObject
    {
        [PropertyOrder(10)] [Title("用于判断的参数")] public Object someObject;

        [PropertyOrder(10)] [EnumToggleButtons]
        public InfoMessageType someEnum;

        [PropertyOrder(10)] public bool isToggled;

        [PropertyOrder(10)] [FoldoutGroup("基础使用")] [ShowIf("someEnum", InfoMessageType.Info)]
        public Vector2 info;

        [PropertyOrder(10)] [FoldoutGroup("基础使用")] [ShowIf("someEnum", InfoMessageType.Error)]
        public Vector2 error;

        [PropertyOrder(10)] [FoldoutGroup("基础使用")] [ShowIf("someEnum", InfoMessageType.Warning)]
        public Vector2 warning;

        [PropertyOrder(10)] [FoldoutGroup("基础使用")] [ShowIf("isToggled")]
        public int showIfToggled;

        [PropertyOrder(10)] [FoldoutGroup("基础使用")] [ShowIf("someObject")]
        public Vector3 showWhenIsNotNull;

        [PropertyOrder(10)] [FoldoutGroup("基础使用")] [ShowIf("Method")]
        public int showWithMethod;

        [PropertyOrder(10)]
        [FoldoutGroup("基础使用")]
        [ShowIf("@this.isToggled && this.someObject != null || " +
                "this.someEnum == InfoMessageType.Error")]
        public int showWithExpression;

        private bool Method()
        {
            return (isToggled && someObject != null) ||
                   someEnum == InfoMessageType.Error;
        }
    }
}