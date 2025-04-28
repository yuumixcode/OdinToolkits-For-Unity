using Sirenix.OdinInspector;
using UnityEngine;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    [IsChineseAttributeExample]
    public class WrapExample : ExampleScriptableObject
    {
        [FoldoutGroup("基础使用")] [Wrap(0f, 30f)] public int intWrapFrom0To100;

        [FoldoutGroup("基础使用")] [Wrap(0f, 30f)] public float floatWrapFrom0To100;

        [FoldoutGroup("基础使用")] [Wrap(0f, 30f)] public Vector3 vector3WrapFrom0To100;

        [FoldoutGroup("特殊值")] [Wrap(0f, 360)] public float angleWrap;

        [FoldoutGroup("特殊值")] [Wrap(0f, Mathf.PI * 2)]
        public float radianWrap;
    }
}