using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class MinMaxSliderExample : ExampleScriptableObject
    {
        [PropertySpace(30f, 30f)] [MinMaxSlider(-10f, 10f, true)] [InlineButton("DebugValue", "输出值")]
        public Vector2 vector2MinMaxSlider;

        private void DebugValue()
        {
            Debug.Log("vector2MinMaxSlider: " + vector2MinMaxSlider);
        }
    }
}