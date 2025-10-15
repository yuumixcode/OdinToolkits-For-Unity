using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [OdinToolkitsAttributeExample]
    public class FoldoutGroupExample : ExampleSO
    {
        #region Serialized Fields

        [FoldoutGroup("基础使用")]
        [Title("groupName: \"基础使用\"")]
        public string foldoutGroup1;

        [FoldoutGroup("Group B", 10)]
        [Title("order: 10")]
        public string foldoutGroup2;

        [FoldoutGroup("Group C", 2)]
        [Title("order: 2")]
        public string foldoutGroup3;

        [FoldoutGroup("Group D", true)]
        [Title("expanded: true")]
        public string foldoutGroup4;

        #endregion
    }
}
