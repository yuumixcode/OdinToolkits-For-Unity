using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    [TypeInfoBox("Play 运行模式下隐藏字段")]
    public class HideInPlayModeExample : ExampleScriptableObject
    {
        [Title("Hide in play mode")] [HideInPlayMode]
        public GameObject hideInPlayMode1;

        [HideInPlayMode] public Material hideInPlayMode2;
    }
}