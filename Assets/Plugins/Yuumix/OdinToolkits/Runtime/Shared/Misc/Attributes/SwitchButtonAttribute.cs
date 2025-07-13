using System;

namespace Yuumix.OdinToolkits.Shared
{
    public enum SwitchAlignment
    {
        Left,
        Right,
        Center
    }

    public class SwitchButtonAttribute : Attribute
    {
        const string DefaultBackgroundColorOn = "@new Color(0.498f, 0.843f, 0.992f)";
        const string DefaultBackgroundColorOff = "@new Color(0.165f, 0.165f, 0.165f)";
        const string DefaultSwitchColorOn = DefaultBackgroundColorOff;
        const string DefaultSwitchColorOff = DefaultBackgroundColorOn;
        const int DefaultSwitchWidth = 28;
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
            BackgroundColorOn = backgroundColorOn ?? DefaultBackgroundColorOn;
            BackgroundColorOff = backgroundColorOff ?? DefaultBackgroundColorOff;
            SwitchColorOn = switchColorOn ?? backgroundColorOff ?? DefaultSwitchColorOn;
            SwitchColorOff = switchColorOff ?? backgroundColorOn ?? DefaultSwitchColorOff;
        }
    }
}
