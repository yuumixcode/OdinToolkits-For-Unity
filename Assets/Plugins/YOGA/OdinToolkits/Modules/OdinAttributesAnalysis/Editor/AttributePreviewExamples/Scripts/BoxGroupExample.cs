using Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Common.Editor;
using Sirenix.OdinInspector;

namespace Plugins.YOGA.OdinToolkits.Modules.OdinAttributesAnalysis.Editor.AttributePreviewExamples.Scripts
{
    [IsChineseAttributeExample]
    public class BoxGroupExample : ExampleScriptableObject
    {
        [PropertyOrder(10)]
        [FoldoutGroup("基础使用")]
        [BoxGroup("基础使用/A Group", false, order: 1)]
        [Title("showLabel: false")]
        public string boxGroup1;

        [PropertyOrder(20)]
        [FoldoutGroup("基础使用")]
        [BoxGroup("基础使用/B Group", centerLabel: true, order: 2)]
        [Title("centerLabel: true，默认 Group 路径最后一层即为名称")]
        public string boxGroup2;

        [PropertyOrder(30)] [FoldoutGroup("基础使用")] [BoxGroup("基础使用/C Group", order: 10)] [Title("order: 10")]
        public string boxGroup3;

        [PropertyOrder(40)] [FoldoutGroup("基础使用")] [BoxGroup("基础使用/C Group")]
        public string boxGroup4;

        [PropertyOrder(50)]
        [FoldoutGroup("基础使用")]
        [BoxGroup("基础使用/D Group", LabelText = "groupName1")]
        [Title("LabelText: groupName1，不支持引用变量名")]
        public string groupName1;

        [PropertyOrder(50)] [FoldoutGroup("基础使用")] [BoxGroup("基础使用/D Group", LabelText = "groupName1")]
        public string boxGroup5;

        [PropertyOrder(60)]
        [FoldoutGroup("基础使用")]
        [BoxGroup("基础使用/E Group", LabelText = "自定义标题")]
        [Title("LabelText: 自定义标题")]
        public string groupName2;
    }
}