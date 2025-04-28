using Sirenix.OdinInspector;
using UnityEngine;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    [IsChineseAttributeExample]
    public class DrawWithUnityExample : ExampleScriptableObject
    {
        [Title("Odin 绘制")] public GameObject objectDrawnWithOdin;

        [Title("调用 Unity 绘制")] [DrawWithUnity] public GameObject objectDrawnWithUnity;
    }
}