using Sirenix.OdinInspector;
using Sirenix.Utilities;

namespace Yuumix.OdinToolkits.Common.InspectorLocalization
{
    public readonly struct ButtonAttributeData
    {
        public readonly string ChineseName;
        public readonly string EnglishName;
        public readonly ButtonSizes ButtonSize;
        public readonly int ButtonHeight;
        public readonly ButtonStyle Style;
        public readonly SdfIconType Icon;
        public readonly IconAlignment ButtonIconAlignment;
        public readonly bool Stretch;
        public readonly bool DrawResult;
        public readonly bool Expanded;

        // The alignment of the button represented by a range from 0 to 1 where 0 is the left edge of the available space and 1 is the right edge.
        // ButtonAlignment only has an effect when Stretch is set to false.
        public readonly float ButtonAlignment;

        /// <summary>
        ///     <para>Whether to display the button method's parameters (if any) as values in the inspector. True by default.</para>
        ///     <para>
        ///     If this is set to false, the button method will instead be invoked through an ActionResolver or ValueResolver
        ///     (based on whether it returns a value), giving access to contextual named parameter values like "InspectorProperty
        ///     property" that can be passed to the button method.
        ///     </para>
        /// </summary>
        public readonly bool DisplayParameters;

        /// <summary>
        /// Whether the containing object or scene (if there is one) should be marked dirty when the button is clicked. True by
        /// default. Note that if this is false, undo for any changes caused by the button click is also disabled, as registering
        /// undo events also causes dirtying.
        /// </summary>
        public readonly bool DirtyOnClick;

        public ButtonAttributeData(
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

        public static ButtonAttribute CreateChineseButtonAttribute(ButtonAttributeData data)
        {
            var button = new ButtonAttribute(data.ChineseName, data.ButtonSize)
            {
                Style = data.Style,
                Icon = data.Icon,
                IconAlignment = data.ButtonIconAlignment,
                ButtonAlignment = data.ButtonAlignment,
                Stretch = data.Stretch,
                DrawResult = data.DrawResult,
                Expanded = data.Expanded,
                DisplayParameters = data.DisplayParameters,
                DirtyOnClick = data.DirtyOnClick
            };
            // 如果 ButtonHeight 大于 ButtonSize 对应的高度，则覆盖
            if (data.ButtonHeight > (int)data.ButtonSize)
            {
                button.ButtonHeight = data.ButtonHeight;
            }

            return button;
        }

        public static ButtonAttribute CreateEnglishButtonAttribute(ButtonAttributeData data)
        {
            if (data.EnglishName.IsNullOrWhitespace())
            {
                return CreateChineseButtonAttribute(data);
            }

            var button = new ButtonAttribute(data.EnglishName, data.ButtonSize)
            {
                Style = data.Style,
                Icon = data.Icon,
                IconAlignment = data.ButtonIconAlignment,
                ButtonAlignment = data.ButtonAlignment,
                Stretch = data.Stretch,
                DrawResult = data.DrawResult,
                Expanded = data.Expanded,
                DisplayParameters = data.DisplayParameters,
                DirtyOnClick = data.DirtyOnClick
            };
            // 如果 ButtonHeight 大于 ButtonSize 对应的高度，则覆盖
            if (data.ButtonHeight > (int)data.ButtonSize)
            {
                button.ButtonHeight = data.ButtonHeight;
            }

            return button;
        }
    }
}
