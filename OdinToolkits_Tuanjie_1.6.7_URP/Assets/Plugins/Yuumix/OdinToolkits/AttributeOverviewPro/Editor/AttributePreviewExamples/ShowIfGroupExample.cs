using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [AttributeOverviewProExample]
    [TypeInfoBox("如果 ShowIfGroup 没有设置 Condition，那么路径既发挥组名作用，也是条件判断的值，引用成员名" +
                 "如果设置 Condition，则路径将只表示组名")]
    public class ShowIfGroupExample : ExampleSO
    {
        #region Serialized Fields

        // 没有写条件，那么路径将覆盖组名，引用成员名，toggle 既是 Group 的组名（可以和其他 Group 连接）
        // 又是 ShowIfGroup 的条件，条件的值 == toggle
        [ShowIfGroup("toggle")]
        [BoxGroup("toggle/Shown Box")]
        public int a, b;

        [BoxGroup("Box")]
        public InfoMessageType enumField = InfoMessageType.Info;

        // 与常规 if 属性一样，ShowIfGroup 也支持指定值。
        // 你也可以将多个 ShowIfGroup 属性链接在一起，以实现更复杂的行为。
        [ShowIfGroup("Box/toggle/enumField", Value = InfoMessageType.Info)]
        [BoxGroup("Box/toggle/enumField/Border", ShowLabel = false)]
        public string fieldName;

        [ShowIfGroup("RectGroup", Condition = "toggle")]
        public GameObject gameObject;

        // 默认情况下，ShowIfGroup 将使用组名；
        // 但是你也可以使用 MemberName 属性来覆盖它。
        // 此时设置了 Condition，那么 RectGroup 将只表示组名
        [ShowIfGroup("RectGroup", Condition = "toggle")]
        public Rect rect;

        public bool toggle = true;

        [BoxGroup("Box/toggle/enumField/Border")]
        public Vector3 vector;

        [BoxGroup("Box")]
        [ShowIfGroup("Box/toggle")]
        public Vector3 x, y;

        #endregion
    }
}
