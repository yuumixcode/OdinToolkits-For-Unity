using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class MinMaxSliderExample : ExampleScriptableObject
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
