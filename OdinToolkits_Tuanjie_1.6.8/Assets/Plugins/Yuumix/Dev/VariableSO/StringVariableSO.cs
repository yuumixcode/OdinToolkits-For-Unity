using Sirenix.OdinInspector;
using UnityEngine;

namespace Dev
{
    [CreateAssetMenu(fileName = "StringVariable", menuName = "Variables/String Variable", order = 3)]
    public class StringVariableSO : BaseVariableSO<string>
    {
        [PropertySpace(10)]
        [BoxGroup("选项")]
        [LabelText("多行")]
        public bool isMultiline = false;

        [BoxGroup("选项")]
        [LabelText("最大长度 (0 = 无限制)")]
        [MinValue(0)]
        public int maxLength = 0;

        public override void SetValue(string newValue)
        {
            if (maxLength > 0 && newValue != null && newValue.Length > maxLength)
            {
                newValue = newValue.Substring(0, maxLength);
            }

            base.SetValue(newValue ?? string.Empty);
        }

        [ButtonGroup("操作")]
        [Button("清空")]
        public void Clear()
        {
            SetValue(string.Empty);
        }

        [ButtonGroup("操作")]
        [Button("转为大写")]
        public void ToUpperCase()
        {
            SetValue(value?.ToUpper());
        }

        [ButtonGroup("操作")]
        [Button("转为小写")]
        public void ToLowerCase()
        {
            SetValue(value?.ToLower());
        }

        [ShowInInspector]
        [ReadOnly]
        [PropertyOrder(50)]
        [LabelText("字符数量")]
        public int CharacterCount => value?.Length ?? 0;
    }
}
