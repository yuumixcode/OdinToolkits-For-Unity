using Sirenix.OdinInspector;
using Yuumix.OdinToolkits.Common.EditorLocalization;

namespace Yuumix.OdinToolkits.Modules.CustomExtensions.Structs
{
    public struct ButtonAttributeConfig
    {
        public string ChineseName;
        public string EnglishName;
        public ButtonSizes ButtonSize;
        public int ButtonHeight;
        public ButtonStyle Style;
        public SdfIconType Icon;
        public IconAlignment ButtonIconAlignment;
        public bool Stretch;
        public bool DrawResult;
        public bool Expanded;

        // The alignment of the button represented by a range from 0 to 1 where 0 is the left edge of the available space and 1 is the right edge.
        // ButtonAlignment only has an effect when Stretch is set to false.
        public float ButtonAlignment;

        /// <summary>
        ///     <para>Whether to display the button method's parameters (if any) as values in the inspector. True by default.</para>
        ///     <para>
        ///     If this is set to false, the button method will instead be invoked through an ActionResolver or ValueResolver
        ///     (based on whether it returns a value), giving access to contextual named parameter values like "InspectorProperty
        ///     property" that can be passed to the button method.
        ///     </para>
        /// </summary>
        public bool DisplayParameters;

        /// <summary>
        /// Whether the containing object or scene (if there is one) should be marked dirty when the button is clicked. True by
        /// default. Note that if this is false, undo for any changes caused by the button click is also disabled, as registering
        /// undo events also causes dirtying.
        /// </summary>
        public bool DirtyOnClick;

        public ButtonAttributeConfig(
            string chineseName = null,
            string englishName = null,
            ButtonSizes buttonSize = ButtonSizes.Medium,
            ButtonStyle style = ButtonStyle.CompactBox,
            SdfIconType icon = SdfIconType.None,
            IconAlignment buttonIconAlignment = IconAlignment.LeftOfText,
            int buttonHeight = -1,
            bool stretch = true,
            bool drawResult = true,
            bool expanded = false,
            float buttonAlignment = 0.5f,
            bool displayParameters = true,
            bool dirtyOnClick = true)
        {
            ChineseName = chineseName;
            EnglishName = englishName;
            ButtonSize = buttonSize;
            Style = style;
            Icon = icon;
            ButtonIconAlignment = buttonIconAlignment;
            ButtonHeight = buttonHeight;
            Stretch = stretch;
            DrawResult = drawResult;
            Expanded = expanded;
            ButtonAlignment = buttonAlignment;
            DisplayParameters = displayParameters;
            DirtyOnClick = dirtyOnClick;
        }

        public static ButtonAttribute CreateChineseButtonAttribute(ButtonAttributeConfig config)
        {
            var button = new ButtonAttribute(config.ChineseName, config.ButtonSize)
            {
                Style = config.Style,
                Icon = config.Icon,
                IconAlignment = config.ButtonIconAlignment,
                ButtonAlignment = config.ButtonAlignment,
                Stretch = config.Stretch,
                DrawResult = config.DrawResult,
                Expanded = config.Expanded,
                DisplayParameters = config.DisplayParameters,
                DirtyOnClick = config.DirtyOnClick
            };
            // 如果 ButtonHeight 大于 ButtonSize 对应的高度，则覆盖
            if (config.ButtonHeight > (int)config.ButtonSize)
            {
                button.ButtonHeight = config.ButtonHeight;
            }

            return button;
        }

        public static ButtonAttribute CreateEnglishButtonAttribute(ButtonAttributeConfig config)
        {
            var button = new ButtonAttribute(config.EnglishName, config.ButtonSize)
            {
                Style = config.Style,
                Icon = config.Icon,
                IconAlignment = config.ButtonIconAlignment,
                ButtonAlignment = config.ButtonAlignment,
                Stretch = config.Stretch,
                DrawResult = config.DrawResult,
                Expanded = config.Expanded,
                DisplayParameters = config.DisplayParameters,
                DirtyOnClick = config.DirtyOnClick
            };
            // 如果 ButtonHeight 大于 ButtonSize 对应的高度，则覆盖
            if (config.ButtonHeight > (int)config.ButtonSize)
            {
                button.ButtonHeight = config.ButtonHeight;
            }

            return button;
        }
    }
}
