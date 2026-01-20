using Sirenix.OdinInspector;
using UnityEngine;

namespace Dev
{
    [CreateAssetMenu(fileName = "Vector3Variable", menuName = "Variables/Vector3 Variable", order = 5)]
    public class Vector3VariableSO : BaseVariableSO<Vector3>
    {
        [ButtonGroup("操作")]
        [Button("重置为零")]
        public void ResetToZero()
        {
            SetValue(Vector3.zero);
        }

        [ButtonGroup("操作")]
        [Button("重置为一")]
        public void ResetToOne()
        {
            SetValue(Vector3.one);
        }

        [ButtonGroup("预设/轴向")]
        [Button("上")]
        public void SetUp()
        {
            SetValue(Vector3.up);
        }

        [ButtonGroup("预设/轴向")]
        [Button("下")]
        public void SetDown()
        {
            SetValue(Vector3.down);
        }

        [ButtonGroup("预设/轴向")]
        [Button("左")]
        public void SetLeft()
        {
            SetValue(Vector3.left);
        }

        [ButtonGroup("预设/轴向")]
        [Button("右")]
        public void SetRight()
        {
            SetValue(Vector3.right);
        }

        [ButtonGroup("预设/轴向")]
        [Button("前")]
        public void SetForward()
        {
            SetValue(Vector3.forward);
        }

        [ButtonGroup("预设/轴向")]
        [Button("后")]
        public void SetBack()
        {
            SetValue(Vector3.back);
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
        public Vector3 Normalized => value.normalized;
    }
}
