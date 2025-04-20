using Sirenix.OdinInspector;
using UnityEngine;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    [IsChineseAttributeExample]
    public class HideIfExample : ExampleScriptableObject
    {
        [PropertyOrder(10)]
        [Title("用于判断的参数")]
        public Object someObject;

        [PropertyOrder(10)]
        [EnumToggleButtons]
        public InfoMessageType someEnum;

        [PropertyOrder(10)]
        public bool isToggled;

        [PropertyOrder(10)]
        [FoldoutGroup("基础使用")]
        [HideIf("someEnum", InfoMessageType.Info)]
        public Vector2 info;

        [PropertyOrder(10)]
        [FoldoutGroup("基础使用")]
        [HideIf("someEnum", InfoMessageType.Error)]
        public Vector2 error;

        [PropertyOrder(10)]
        [FoldoutGroup("基础使用")]
        [HideIf("someEnum", InfoMessageType.Warning)]
        public Vector2 warning;

        [PropertyOrder(10)]
        [FoldoutGroup("基础使用")]
        [HideIf("isToggled")]
        public int hideIfToggled;

        [PropertyOrder(10)]
        [FoldoutGroup("基础使用")]
        [HideIf("someObject")]
        public Vector3 hideWhenIsNotNull;

        [PropertyOrder(10)]
        [FoldoutGroup("基础使用")]
        [HideIf("Method")]
        public int hideWithMethod;

        [PropertyOrder(10)]
        [FoldoutGroup("基础使用")]
        [HideIf("@this.isToggled && this.someObject != null || " +
                "this.someEnum == InfoMessageType.Error")]
        public int hideWithExpression;

        bool Method() => this.isToggled && this.someObject != null ||
                         this.someEnum == InfoMessageType.Error;
    }
}