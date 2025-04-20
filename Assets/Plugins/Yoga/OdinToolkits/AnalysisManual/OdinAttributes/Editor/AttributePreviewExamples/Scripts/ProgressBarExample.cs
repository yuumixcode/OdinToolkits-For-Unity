using Sirenix.OdinInspector;
using UnityEngine;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    [IsChineseAttributeExample]
    public class ProgressBarExample : ExampleScriptableObject
    {
        [FoldoutGroup("min 和 max")]
        [ProgressBar(0f, 10f)]
        public int progressBar1;

        [FoldoutGroup("ColorGetter")]
        [ProgressBar(-5f, 15f, ColorGetter = "lightpurple")]
        public int progressBar2;

        [FoldoutGroup("Height")]
        [ProgressBar(-10f, 20f, Height = 30)]
        public int progressBar3;

        [FoldoutGroup("Segmented")]
        [ProgressBar(-10f, 20f, Segmented = true)]
        public int progressBar4;

        [FoldoutGroup("DrawValueLabel 和 CustomValueStringGetter")]
        [ProgressBar(-10f, 20f, DrawValueLabel = true, CustomValueStringGetter = nameof(progressBar5))]
        public int progressBar5;

        [FoldoutGroup("动态的生命值，颜色变换")]
        [ProgressBar(0, 100, ColorGetter = "GetHealthBarColor")]
        public int dynamicHealthBar;

        Color GetHealthBarColor()
        {
            var color = Color.Lerp(Color.red, Color.green, dynamicHealthBar / 100f);
            return color;
        }

        [FoldoutGroup("堆叠生命值，多层生命值")]
        [PropertyRange(0, 300)]
        public int stackHeath;

        [FoldoutGroup("堆叠生命值，多层生命值")]
        [HideLabel]
        [ShowInInspector]
        [ProgressBar(0, 100, ColorGetter = "GetStackedHealthColor",
            BackgroundColorGetter = "GetStackHealthBackgroundColor", DrawValueLabel = false)]
        private float StackedHealthProgressBar
        {
            get
            {
                var value = stackHeath % 100f;
                if (stackHeath != 0 && value == 0)
                {
                    value = 100;
                }

                return value;
            }
        }

        Color GetStackedHealthColor()
        {
            Color color = stackHeath switch
            {
                <= 100 => Color.yellow,
                > 100 and <= 200 => Color.red,
                > 200 => Color.green
            };

            return color;
        }

        Color GetStackHealthBackgroundColor()
        {
            Color color;
            switch (stackHeath)
            {
                case < 100:
                    color = Color.grey;
                    break;
                case >= 100 and < 200:
                    color = Color.yellow;
                    break;
                case >= 200:
                    color = Color.red;
                    break;
            }

            return color;
        }
    }
}