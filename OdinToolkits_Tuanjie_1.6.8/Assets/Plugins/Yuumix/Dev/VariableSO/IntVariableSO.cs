using Sirenix.OdinInspector;
using UnityEngine;

namespace Dev
{
    [CreateAssetMenu(fileName = "IntVariable", menuName = "Variables/Int Variable", order = 0)]
    public class IntVariableSO : BaseVariableSO<int>
    {
        [PropertySpace(10)]
        [MinMaxSlider(-1000, 1000, ShowFields = true)]
        [BoxGroup("约束")]
        [LabelText("最小/最大范围")]
        public Vector2Int minMaxRange = new Vector2Int(-1000, 1000);

        [BoxGroup("约束")]
        [LabelText("限制数值")]
        public bool clampValue;

        [Button("重置为 0")]
        public void Reset()
        {
            SetValue(0);
        }

        public override void SetValue(int newValue)
        {
            if (clampValue)
            {
                newValue = Mathf.Clamp(newValue, minMaxRange.x, minMaxRange.y);
            }

            base.SetValue(newValue);
        }
    }
}
