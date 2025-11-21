using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [AttributeOverviewProExample]
    public class TitleGroupExample : ExampleSO
    {
        #region Serialized Fields

        [FoldoutGroup("title 参数")]
        [TitleGroup("title 参数/主标题")]
        public int titleGroup1;

        [FoldoutGroup("subtitle 参数")]
        [TitleGroup("subtitle 参数/主标题", "子标题，对标题进行补充")]
        public int titleGroup2;

        [FoldoutGroup("alignment 参数")]
        [TitleGroup("alignment 参数/标题 1", "补充标题", TitleAlignments.Centered)]
        public int titleGroup3;

        [FoldoutGroup("alignment 参数")]
        [TitleGroup("alignment 参数/标题 2", "补充标题", TitleAlignments.Split)]
        public int titleGroup4;

        [FoldoutGroup("horizontalLine 参数")]
        [TitleGroup("horizontalLine 参数/标题", horizontalLine: false)]
        public int titleGroup5;

        [FoldoutGroup("boldTitle 参数")]
        [TitleGroup("boldTitle 参数/标题", boldTitle: false)]
        public int titleGroup6;

        [FoldoutGroup("indent 参数")]
        [TitleGroup("indent 参数/标题", indent: true)]
        public int titleGroup7;

        [FoldoutGroup("indent 参数")]
        [TitleGroup("indent 参数/标题/深层次标题", indent: true)]
        public int titleGroup8;

        [FoldoutGroup("order 参数")]
        [TitleGroup("order 参数/标题1", order: 5)]
        public int titleGroup9;

        [FoldoutGroup("order 参数")]
        [TitleGroup("order 参数/标题2", order: 2)]
        public int titleGroup10;

        #endregion
    }
}
