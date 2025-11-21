using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [AttributeOverviewProExample]
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
