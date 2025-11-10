using Sirenix.OdinInspector;
using System;
using System.Diagnostics;

namespace Yuumix.OdinToolkits.Core
{
    /// <summary>
    /// 多语言 Button 特性，依赖并兼容 Odin Inspector 的绘制系统。
    /// </summary>
    [AttributeUsage(AttributeTargets.All, Inherited = false)]
    [Conditional("UNITY_EDITOR")]
    public class BilingualButtonAttribute : ShowInInspectorAttribute
    {
        // The alignment of the button represented by a range from 0 to 1 where 0 is the left edge of the available space and 1 is the right edge.
        // ButtonAlignment only has an effect when Stretch is set to false.
        public float ButtonAlignment;
        public readonly int ButtonHeight;
        public readonly IconAlignment ButtonIconAlignment;
        public readonly ButtonSizes ButtonSize;
        public readonly string ChineseName;

        /// <summary>
        /// Whether the containing object or scene (if there is one) should be marked dirty when the button is clicked. True by
        /// default. Note that if this is false, undo for any changes caused by the button click is also disabled, as registering
        /// undo events also causes dirtying.
        /// </summary>
        public readonly bool DirtyOnClick;

        /// <summary>
        ///     <para>Whether to display the button method's parameters (if any) as values in the inspector. True by default.</para>
        ///     <para>
        ///     If this is set to false, the button method will instead be invoked through an ActionResolver or ValueResolver
        ///     (based on whether it returns a value), giving access to contextual named parameter values like "InspectorProperty
        ///     property" that can be passed to the button method.
        ///     </para>
        /// </summary>
        public readonly bool DisplayParameters;

        public readonly bool DrawResult;
        public readonly string EnglishName;
        public readonly bool Expanded;
        public readonly SdfIconType Icon;
        public readonly bool Stretch;
        public readonly ButtonStyle Style;

        public BilingualButtonAttribute(string chineseName, string englishName = null,
            ButtonSizes buttonSize = ButtonSizes.Medium,
            ButtonStyle style = ButtonStyle.Box,
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
            EnglishName = englishName ?? chineseName;
            ButtonSize = buttonSize;
            ButtonHeight = buttonHeight;
            Style = style;
            Icon = icon;
            ButtonIconAlignment = buttonIconAlignment;
            Stretch = stretch;
            DrawResult = drawResult;
            Expanded = expanded;
            ButtonAlignment = buttonAlignment;
            DisplayParameters = displayParameters;
            DirtyOnClick = dirtyOnClick;
        }

        public ButtonAttribute CreateButton()
        {
            var button = new ButtonAttribute(ChineseName, ButtonSize)
            {
                Style = Style,
                Icon = Icon,
                IconAlignment = ButtonIconAlignment,
                ButtonAlignment = ButtonAlignment,
                Stretch = Stretch,
                DrawResult = DrawResult,
                Expanded = Expanded,
                DisplayParameters = DisplayParameters,
                DirtyOnClick = DirtyOnClick
            };
            // 如果 ButtonHeight 大于 ButtonSize 对应的高度，则覆盖
            if (ButtonHeight > (int)ButtonSize)
            {
                button.ButtonHeight = ButtonHeight;
            }

            return button;
        }
    }
}
