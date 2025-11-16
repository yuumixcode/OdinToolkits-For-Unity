using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [OdinToolkitsAttributeExample]
    [TypeInfoBox("如果 HideIfGroup 没有设置 Condition，那么路径既发挥组名作用，也是条件判断的值，引用成员名" +
                 "如果设置 Condition，则路径将只表示组名")]
    public class HideIfGroupExample : ExampleSO
    {
        #region Serialized Fields

        public bool toggle = true;

        // 没有写条件，那么路径将覆盖组名，引用成员名，toggle 既是 Group 的组名（可以和其他 Group 连接）
        // 又是 HideIfGroup 的条件，条件的值 == toggle
        [HideIfGroup("toggle")]
        [BoxGroup("toggle/Shown Box")]
        public int a, b;

        [BoxGroup("Box")]
        public InfoMessageType enumField = InfoMessageType.Info;

        [BoxGroup("Box")]
        [HideIfGroup("Box/toggle")]
        public Vector3 x, y;

        // 与常规 if 属性一样，HideIfGroup 也支持指定值。
        // 你也可以将多个 HideIfGroup 属性链接在一起，以实现更复杂的行为。
        [HideIfGroup("Box/toggle/enumField", Value = InfoMessageType.Info)]
        [BoxGroup("Box/toggle/enumField/Border", ShowLabel = false)]
        public string fieldName;

        [BoxGroup("Box/toggle/enumField/Border")]
        public Vector3 vector;

        // 默认情况下，HideIfGroup 将使用组名；
        // 但是你也可以使用 MemberName 属性来覆盖它。
        // 此时设置了 Condition，那么 RectGroup 将只表示组名
        [HideIfGroup("RectGroup", Condition = "toggle")]
        public Rect rect;

        [HideIfGroup("RectGroup", Condition = "toggle")]
        public GameObject gameObject;

        #endregion
    }
}
