using Sirenix.OdinInspector;
using UnityEngine;

namespace Dev
{
    [CreateAssetMenu(fileName = "ColorVariable", menuName = "Variables/Color Variable", order = 6)]
    public class ColorVariableSO : BaseVariableSO<Color>
    {
        [ButtonGroup("预设")]
        [Button("白色")]
        public void SetWhite()
        {
            SetValue(Color.white);
        }

        [ButtonGroup("预设")]
        [Button("黑色")]
        public void SetBlack()
        {
            SetValue(Color.black);
        }

        [ButtonGroup("预设")]
        [Button("红色")]
        public void SetRed()
        {
            SetValue(Color.red);
        }

        [ButtonGroup("预设")]
        [Button("绿色")]
        public void SetGreen()
        {
            SetValue(Color.green);
        }

        [ButtonGroup("预设")]
        [Button("蓝色")]
        public void SetBlue()
        {
            SetValue(Color.blue);
        }

        [ButtonGroup("预设")]
        [Button("黄色")]
        public void SetYellow()
        {
            SetValue(Color.yellow);
        }

        [ButtonGroup("预设")]
        [Button("青色")]
        public void SetCyan()
        {
            SetValue(Color.cyan);
        }

        [ButtonGroup("预设")]
        [Button("洋红色")]
        public void SetMagenta()
        {
            SetValue(Color.magenta);
        }

        [ButtonGroup("操作")]
        [Button("反转颜色")]
        public void InvertColor()
        {
            SetValue(new Color(1f - value.r, 1f - value.g, 1f - value.b, value.a));
        }

        [ButtonGroup("操作")]
        [Button("灰度化")]
        public void ToGrayscale()
        {
            float gray = value.grayscale;
            SetValue(new Color(gray, gray, gray, value.a));
        }

        [ShowInInspector]
        [ReadOnly]
        [PropertyOrder(50)]
        [LabelText("十六进制颜色")]
        public string HexColor => $"#{ColorUtility.ToHtmlStringRGBA(value)}";
    }
}
