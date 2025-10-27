using System;
using Sirenix.OdinInspector;

namespace Yuumix.OdinToolkits.Modules.Editor
{
    [OdinToolkitsAttributeExample]
    public class EnumToggleButtonsExample : ExampleSO
    {
        [Flags]
        public enum SomeBitmaskEnum
        {
            A = 1 << 1,
            B = 1 << 2,
            C = 1 << 3,
            All = A | B | C
        }

        public enum SomeEnum
        {
            First,
            Second,
            Third,
            Fourth,
            AndSoOn
        }

        public enum SomeEnumWithIcons
        {
            [LabelText(SdfIconType.TextLeft)]
            TextLeft,

            [LabelText(SdfIconType.TextCenter)]
            TextCenter,

            [LabelText(SdfIconType.TextRight)]
            TextRight
        }

        public enum SomeEnumWithIconsAndNames
        {
            [LabelText("Align Left", SdfIconType.TextLeft)]
            TextLeft,

            [LabelText("Align Center", SdfIconType.TextCenter)]
            TextCenter,

            [LabelText("Align Right", SdfIconType.TextRight)]
            TextRight
        }

        const string Code61 = "[EnumToggleButtons]";

        [PropertyOrder(1)]
        [InfoBox("默认的枚举类型样式")]
        public SomeBitmaskEnum defaultEnumBitmask;

        [PropertyOrder(10)]
        [FoldoutGroup("EnumToggleButtons 基础使用")]
        [EnumToggleButtons]
        public SomeEnum someEnumField;

        [PropertyOrder(20)]
        [FoldoutGroup("EnumToggleButtons 基础使用")]
        [EnumToggleButtons]
        [HideLabel]
        [InfoBox("隐藏 Label")]
        public SomeEnum wideEnumField;

        [PropertyOrder(30)]
        [FoldoutGroup("EnumToggleButtons 基础使用")]
        [InfoBox("位掩码，Flag 标记的枚举，可多选")]
        [EnumToggleButtons]
        public SomeBitmaskEnum bitmaskEnumField;

        [PropertyOrder(40)]
        [FoldoutGroup("EnumToggleButtons 基础使用")]
        [InfoBox("隐藏 Label")]
        [EnumToggleButtons]
        [HideLabel]
        public SomeBitmaskEnum enumFieldWide;

        [PropertyOrder(50)]
        [FoldoutGroup("EnumToggleButtons 基础使用")]
        [InfoBox("带图标的枚举，内部使用了 LabelText 设置枚举值图标")]
        [EnumToggleButtons]
        public SomeEnumWithIcons enumWithIcons;

        [PropertyOrder(60)]
        [FoldoutGroup("EnumToggleButtons 基础使用")]
        [InfoBox("带图标的枚举，内部使用了 LabelText 设置枚举值图标和名称")]
        [EnumToggleButtons]
        public SomeEnumWithIconsAndNames enumWithIconsAndNames;
    }
}
