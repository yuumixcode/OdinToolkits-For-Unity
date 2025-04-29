using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Sirenix.OdinInspector;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class FoldoutGroupExample : ExampleScriptableObject
    {
        [FoldoutGroup("基础使用")] [Title("groupName: \"基础使用\"")]
        public string foldoutGroup1;

        [FoldoutGroup("Group B", 10)] [Title("order: 10")]
        public string foldoutGroup2;

        [FoldoutGroup("Group C", 2)] [Title("order: 2")]
        public string foldoutGroup3;

        [FoldoutGroup("Group D", true)] [Title("expanded: true")]
        public string foldoutGroup4;
    }
}