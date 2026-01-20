using Sirenix.OdinInspector;
using UnityEngine;

namespace Dev
{
    [CreateAssetMenu(fileName = "Vector2Variable", menuName = "Variables/Vector2 Variable", order = 4)]
    public class Vector2VariableSO : BaseVariableSO<Vector2>
    {
        [ButtonGroup("操作")]
        [Button("重置为零")]
        public void ResetToZero()
        {
            SetValue(Vector2.zero);
        }

        [ButtonGroup("操作")]
        [Button("重置为一")]
        public void ResetToOne()
        {
            SetValue(Vector2.one);
        }

        [ButtonGroup("预设")]
        [Button("上")]
        public void SetUp()
        {
            SetValue(Vector2.up);
        }

        [ButtonGroup("预设")]
        [Button("下")]
        public void SetDown()
        {
            SetValue(Vector2.down);
        }

        [ButtonGroup("预设")]
        [Button("左")]
        public void SetLeft()
        {
            SetValue(Vector2.left);
        }

        [ButtonGroup("预设")]
        [Button("右")]
        public void SetRight()
        {
            SetValue(Vector2.right);
        }

        [ShowInInspector]
        [ReadOnly]
        [PropertyOrder(50)]
        [LabelText("长度")]
        public float Magnitude => value.magnitude;

        [ShowInInspector]
        [ReadOnly]
        [PropertyOrder(51)]
        [LabelText("归一化")]
        public Vector2 Normalized => value.normalized;
    }
}
