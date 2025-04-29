using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class SuppressInvalidErrorExample : ExampleScriptableObject
    {
        [Range(0, 10)] public string InvalidAttributeError = "此特性不匹配，会报错";

        [Range(0, 10)] [SuppressInvalidAttributeError]
        public string SuppressedError = "此特性不匹配，但是标记了抑制，也不会报错";
    }
}