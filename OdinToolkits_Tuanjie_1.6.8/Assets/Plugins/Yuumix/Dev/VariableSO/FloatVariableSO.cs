using Sirenix.OdinInspector;
using UnityEngine;

namespace Dev
{
    [CreateAssetMenu(fileName = "FloatVariable", menuName = "Variables/Float Variable", order = 1)]
    public class FloatVariableSO : BaseVariableSO<float>
    {
        [PropertySpace(10)]
        [MinMaxSlider(-100f, 100f, ShowFields = true)]
        [BoxGroup("约束")]
        [LabelText("最小/最大范围")]
        public Vector2 minMaxRange = new Vector2(-100f, 100f);

        [BoxGroup("约束")]
        [LabelText("限制数值")]
        public bool clampValue = false;

        public override void SetValue(float newValue)
        {
            if (clampValue)
            {
                newValue = Mathf.Clamp(newValue, minMaxRange.x, minMaxRange.y);
            }

            base.SetValue(newValue);
        }

        [ButtonGroup("操作")]
        [Button("加 1")]
        public void Increment()
        {
            SetValue(value + 1f);
        }

        [ButtonGroup("操作")]
        [Button("减 1")]
        public void Decrement()
        {
            SetValue(value - 1f);
        }

        [ButtonGroup("操作")]
        [Button("重置为 0")]
        public void Reset()
        {
            SetValue(0f);
        }
    }
}
