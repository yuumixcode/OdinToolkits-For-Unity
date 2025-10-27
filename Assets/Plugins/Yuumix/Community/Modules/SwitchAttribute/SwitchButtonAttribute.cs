using System;

namespace Yuumix.Community.SwitchAttribute
{
    public enum SwitchAlignment
    {
        Left,
        Right,
        Center
    }

    /// <summary>
    /// 对 bool 类型的变量使用，绘制一个开关样式，而非使用 Unity 的默认 bool 样式
    /// </summary>
    public class SwitchButtonAttribute : Attribute
    {
        const string DEFAULT_BACKGROUND_COLOR_ON = "@new Color(0.498f, 0.843f, 0.992f)";
        const string DEFAULT_BACKGROUND_COLOR_OFF = "@new Color(0.165f, 0.165f, 0.165f)";
        const string DEFAULT_SWITCH_COLOR_ON = DEFAULT_BACKGROUND_COLOR_OFF;
        const string DEFAULT_SWITCH_COLOR_OFF = DEFAULT_BACKGROUND_COLOR_ON;
        const int DEFAULT_SWITCH_WIDTH = 28;
        public readonly SwitchAlignment Alignment;
        public readonly bool Rounded;
        public string BackgroundColorOff;
        public string BackgroundColorOn;
        public string SwitchColorOff;
        public string SwitchColorOn;

        public SwitchButtonAttribute(
            SwitchAlignment alignment = SwitchAlignment.Left,
            bool rounded = true,
            string backgroundColorOn = null,
            string backgroundColorOff = null,
            string switchColorOn = null,
            string switchColorOff = null)
        {
            Alignment = alignment;
            Rounded = rounded;
            SetColors(backgroundColorOn, backgroundColorOff, switchColorOn, switchColorOff);
        }

        void SetColors(
            string backgroundColorOn,
            string backgroundColorOff,
            string switchColorOn,
            string switchColorOff)
        {
            BackgroundColorOn = backgroundColorOn ?? DEFAULT_BACKGROUND_COLOR_ON;
            BackgroundColorOff = backgroundColorOff ?? DEFAULT_BACKGROUND_COLOR_OFF;
            SwitchColorOn = switchColorOn ?? backgroundColorOff ?? DEFAULT_SWITCH_COLOR_ON;
            SwitchColorOff = switchColorOff ?? backgroundColorOn ?? DEFAULT_SWITCH_COLOR_OFF;
        }
    }
}
