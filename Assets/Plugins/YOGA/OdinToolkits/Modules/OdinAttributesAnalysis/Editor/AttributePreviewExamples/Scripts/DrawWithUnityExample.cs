using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class DrawWithUnityExample : ExampleScriptableObject
    {
        [Title("Odin 绘制")] public GameObject objectDrawnWithOdin;

        [Title("调用 Unity 绘制")] [DrawWithUnity] public GameObject objectDrawnWithUnity;
    }
}