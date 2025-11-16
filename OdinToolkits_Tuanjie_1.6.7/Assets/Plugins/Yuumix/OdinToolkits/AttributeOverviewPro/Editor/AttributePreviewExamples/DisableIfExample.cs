using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [OdinToolkitsAttributeExample]
    public class DisableIfExample : ExampleSO
    {
        #region Serialized Fields

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
        [DisableIf("someEnum", InfoMessageType.Info)]
        public Vector2 info;

        [PropertyOrder(10)]
        [FoldoutGroup("基础使用")]
        [DisableIf("someEnum", InfoMessageType.Error)]
        public Vector2 error;

        [PropertyOrder(10)]
        [FoldoutGroup("基础使用")]
        [DisableIf("someEnum", InfoMessageType.Warning)]
        public Vector2 warning;

        [PropertyOrder(10)]
        [FoldoutGroup("基础使用")]
        [DisableIf("isToggled")]
        public int disableIfToggled;

        [PropertyOrder(10)]
        [FoldoutGroup("基础使用")]
        [DisableIf("someObject")]
        public Vector3 enabledWhenNull;

        [PropertyOrder(10)]
        [FoldoutGroup("基础使用")]
        [DisableIf("Method")]
        public int disableWithMethod;

        [PropertyOrder(10)]
        [FoldoutGroup("基础使用")]
        [DisableIf("@this.isToggled && this.someObject != null || " +
                   "this.someEnum == InfoMessageType.Error")]
        public int disableWithExpression;

        #endregion

        bool Method() =>
            (isToggled && someObject != null) ||
            someEnum == InfoMessageType.Error;
    }
}
