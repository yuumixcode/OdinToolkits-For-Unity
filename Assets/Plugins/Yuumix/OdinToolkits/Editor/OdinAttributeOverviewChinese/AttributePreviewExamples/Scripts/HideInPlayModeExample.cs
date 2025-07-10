using Sirenix.OdinInspector;
using UnityEngine;
using Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Common.Editor;

namespace Yuumix.OdinToolkits.Modules.Odin.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    [TypeInfoBox("Play 运行模式下隐藏字段")]
    public class HideInPlayModeExample : ExampleScriptableObject
    {
        [Title("Hide in play mode")]
        [HideInPlayMode]
        public GameObject hideInPlayMode1;

        [HideInPlayMode]
        public Material hideInPlayMode2;
    }
}
