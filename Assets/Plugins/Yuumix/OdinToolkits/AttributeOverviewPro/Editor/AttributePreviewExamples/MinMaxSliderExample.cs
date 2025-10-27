using Sirenix.OdinInspector;
using UnityEngine;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [OdinToolkitsAttributeExample]
    public class MinMaxSliderExample : ExampleSO
    {
        #region Serialized Fields

        [PropertySpace(30f, 30f)]
        [MinMaxSlider(-10f, 10f, true)]
        [InlineButton("DebugValue", "输出值")]
        public Vector2 vector2MinMaxSlider;

        #endregion

        void DebugValue()
        {
            Debug.Log("vector2MinMaxSlider: " + vector2MinMaxSlider);
        }
    }
}
