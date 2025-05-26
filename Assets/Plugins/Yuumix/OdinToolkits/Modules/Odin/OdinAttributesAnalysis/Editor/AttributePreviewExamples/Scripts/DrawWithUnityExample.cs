using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class DrawWithUnityExample : ExampleScriptableObject
    {
        [Title("Odin 绘制")]
        public GameObject objectDrawnWithOdin;

        [Title("调用 Unity 绘制")]
        [DrawWithUnity]
        public GameObject objectDrawnWithUnity;
    }
}
