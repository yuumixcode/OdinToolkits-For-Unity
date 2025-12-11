using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.AttributeOverviewPro.Shared;

namespace Yuumix.OdinToolkits.AttributeOverviewPro.Deprecated.Editor
{
    [AttributeOverviewProExample]
    public class TabGroupExample : ExampleSO
    {
        #region Serialized Fields

        [PropertyOrder(10)]
        [FoldoutGroup("Title", GroupName = "分组连接")]
        [TabGroup("Title/GroupA", "A")]
        public string tabGroup1 = "TabGroup1";

        [PropertyOrder(12)]
        [FoldoutGroup("Title")]
        [TabGroup("Title/GroupA", "B")]
        public string tabGroup2 = "TabGroup2";

        [PropertyOrder(10)]
        [FoldoutGroup("useFixedHeight", GroupName = "固定相同高度")]
        [TabGroup("useFixedHeight/GroupB", "A", true)]
        public string tabGroup3 = "TabGroup3";

        [PropertyOrder(12)]
        [TabGroup("useFixedHeight/GroupB", "A")]
        public string tabGroup4 = "TabGroup4";

        [PropertyOrder(10)]
        [TabGroup("useFixedHeight/GroupB", "B")]
        public string tabGroup5 = "TabGroup5";

        [PropertyOrder(10)]
        [FoldoutGroup("icon", GroupName = "图标以及颜色")]
        [TabGroup("icon/GroupA", "A", SdfIconType.Image, TextColor = "lightorange")]
        public string tabGroup6 = "TabGroup6";

        [PropertyOrder(10)]
        [TabGroup("icon/GroupA", "B", SdfIconType.Apple, TextColor = "lightgray")]
        public string tabGroup7 = "TabGroup7";

        [PropertyOrder(10)]
        [FoldoutGroup("TabName", GroupName = "不同于 TabId 的 TabName")]
        [TabGroup("TabName/GroupA", "A", TabName = "A 组")]
        public string tabGroup8 = nameof(tabGroup8);

        [PropertyOrder(10)]
        [TabGroup("TabName/GroupA", "B", TabName = "B 组")]
        public string tabGroup9 = nameof(tabGroup9);

        [PropertyOrder(10)]
        [FoldoutGroup("Paddingless", GroupName = "Paddingless 参数")]
        [TabGroup("Paddingless/GroupA", "A", Paddingless = true)]
        public string tabGroup10 = nameof(tabGroup10);

        [PropertyOrder(10)]
        [TabGroup("Paddingless/GroupA", "B")]
        public string tabGroup11 = nameof(tabGroup11);

        [PropertyOrder(10)]
        [FoldoutGroup("TestOnly", GroupName = "HideTabGroupIfTabGroupOnlyHasOneTab 参数")]
        [TabGroup("TestOnly/GroupA", "A", HideTabGroupIfTabGroupOnlyHasOneTab = true)]
        public string onlyTab = "";

        [PropertyOrder(20)]
        [FoldoutGroup("TabLayouting", GroupName = "TabLayouting 参数")]
        [TabGroup("TabLayouting/multi row", "World Map", SdfIconType.Map, TextColor = "orange",
            TabLayouting = TabLayouting.MultiRow)]
        [TabGroup("TabLayouting/multi row", "Character", SdfIconType.PersonFill, TextColor = "orange")]
        [TabGroup("TabLayouting/multi row", "Wand", SdfIconType.Magic, TextColor = "red")]
        [TabGroup("TabLayouting/multi row", "Abilities", TextColor = "green")]
        [TabGroup("TabLayouting/multi row", "Missions", SdfIconType.ExclamationSquareFill,
            TextColor = "yellow")]
        [TabGroup("TabLayouting/multi row", "Collection 1", TextColor = "blue")]
        [TabGroup("TabLayouting/multi row", "Collection 2", TextColor = "blue")]
        [TabGroup("TabLayouting/multi row", "Collection 3", TextColor = "blue")]
        [TabGroup("TabLayouting/multi row", "Collection 4", TextColor = "blue")]
        [TabGroup("TabLayouting/multi row", "Settings", SdfIconType.GearFill, TextColor = "grey")]
        [TabGroup("TabLayouting/multi row", "Guide", SdfIconType.QuestionSquareFill, TextColor = "blue")]
        public float e, f, g, h;

        [PropertyOrder(20)]
        [TabGroup("TabLayouting/shrink tabs", "World Map", SdfIconType.Map, TextColor = "orange",
            TabLayouting = TabLayouting.Shrink)]
        [TabGroup("TabLayouting/shrink tabs", "Character", SdfIconType.PersonFill, TextColor = "orange")]
        [TabGroup("TabLayouting/shrink tabs", "Wand", SdfIconType.Magic, TextColor = "red")]
        [TabGroup("TabLayouting/shrink tabs", "Abilities", TextColor = "green")]
        [TabGroup("TabLayouting/shrink tabs", "Missions", SdfIconType.ExclamationSquareFill,
            TextColor = "yellow")]
        [TabGroup("TabLayouting/shrink tabs", "Collection 1", TextColor = "blue")]
        [TabGroup("TabLayouting/shrink tabs", "Collection 2", TextColor = "blue")]
        [TabGroup("TabLayouting/shrink tabs", "Collection 3", TextColor = "blue")]
        [TabGroup("TabLayouting/shrink tabs", "Collection 4", TextColor = "blue")]
        [TabGroup("TabLayouting/shrink tabs", "Settings", SdfIconType.GearFill, TextColor = "grey")]
        [TabGroup("TabLayouting/shrink tabs", "Guide", SdfIconType.QuestionSquareFill, TextColor = "blue")]
        public float a, b, c, d;

        #endregion
    }
}
