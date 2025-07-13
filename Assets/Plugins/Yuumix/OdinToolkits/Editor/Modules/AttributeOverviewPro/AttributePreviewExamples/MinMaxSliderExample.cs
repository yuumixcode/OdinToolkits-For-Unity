using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.Editor
{
    [OdinToolkitsAttributeExample]
    public class MinMaxSliderExample : ExampleSO
    {
        [PropertySpace(30f, 30f)]
        [MinMaxSlider(-10f, 10f, true)]
        [InlineButton("DebugValue", "输出值")]
        public Vector2 vector2MinMaxSlider;

        void DebugValue()
        {
            Debug.Log("vector2MinMaxSlider: " + vector2MinMaxSlider);
        }
    }
}
