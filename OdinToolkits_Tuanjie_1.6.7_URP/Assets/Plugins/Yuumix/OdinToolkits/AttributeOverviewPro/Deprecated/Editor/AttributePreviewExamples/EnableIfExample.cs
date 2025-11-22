using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    [AttributeOverviewProExample]
    public class EnableIfExample : ExampleSO
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
        [EnableIf("someEnum", InfoMessageType.Info)]
        public Vector2 info;

        [PropertyOrder(10)]
        [FoldoutGroup("基础使用")]
        [EnableIf("someEnum", InfoMessageType.Error)]
        public Vector2 error;

        [PropertyOrder(10)]
        [FoldoutGroup("基础使用")]
        [EnableIf("someEnum", InfoMessageType.Warning)]
        public Vector2 warning;

        [PropertyOrder(10)]
        [FoldoutGroup("基础使用")]
        [EnableIf("isToggled")]
        public int enableIfToggled;

        [PropertyOrder(10)]
        [FoldoutGroup("基础使用")]
        [EnableIf("someObject")]
        public Vector3 enabledWhenIsNotNull;

        [PropertyOrder(10)]
        [FoldoutGroup("基础使用")]
        [EnableIf("Method")]
        public int enableWithMethod;

        [PropertyOrder(10)]
        [FoldoutGroup("基础使用")]
        [EnableIf("@this.isToggled && this.someObject != null || " +
                  "this.someEnum == InfoMessageType.Error")]
        public int enableWithExpression;

        #endregion

        bool Method() =>
            (isToggled && someObject != null) ||
            someEnum == InfoMessageType.Error;
    }
}
