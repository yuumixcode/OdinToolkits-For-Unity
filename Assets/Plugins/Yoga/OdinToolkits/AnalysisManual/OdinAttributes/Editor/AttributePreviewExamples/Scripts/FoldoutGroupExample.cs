using Sirenix.OdinInspector;

namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    [IsChineseAttributeExample]
    public class FoldoutGroupExample : ExampleScriptableObject
    {
        [FoldoutGroup(groupName: "基础使用")]
        [Title("groupName: \"基础使用\"")]
        public string foldoutGroup1;

        [FoldoutGroup(groupName: "Group B", order: 10)]
        [Title("order: 10")]
        public string foldoutGroup2;

        [FoldoutGroup(groupName: "Group C", order: 2)]
        [Title("order: 2")]
        public string foldoutGroup3;

        [FoldoutGroup(groupName: "Group D", expanded: true)]
        [Title("expanded: true")]
        public string foldoutGroup4;
    }
}