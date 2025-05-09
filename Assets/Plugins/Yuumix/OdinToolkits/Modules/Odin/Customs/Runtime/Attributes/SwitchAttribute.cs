using System;

namespace YOGA.OdinToolkits.Modules.CustomExtensions.Runtime.Attributes
{
    public enum SwitchAlignment
    {
        Left,
        Right,
        Center
    }

    public class SwitchAttribute : Attribute
    {
        private const string DefaultBackgroundColorOn = "@new Color(0.498f, 0.843f, 0.992f)";
        private const string DefaultBackgroundColorOff = "@new Color(0.165f, 0.165f, 0.165f)";
        private const string DefaultSwitchColorOn = DefaultBackgroundColorOff;
        private const string DefaultSwitchColorOff = DefaultBackgroundColorOn;
        public SwitchAlignment Alignment;
        public string BackgroundColorOff;

        public string BackgroundColorOn;
        public bool Rounded;
        public string SwitchColorOff;
        public string SwitchColorOn;

        public SwitchAttribute(
            SwitchAlignment alignment,
            string backgroundColorOn = null,
            string backgroundColorOff = null,
            string switchColorOn = null,
            string switchColorOff = null,
            bool rounded = true)
        {
            Alignment = alignment;
            Rounded = rounded;
            SetColors(backgroundColorOn, backgroundColorOff, switchColorOn, switchColorOff);
        }

        public SwitchAttribute(
            string backgroundColorOn = null,
            string backgroundColorOff = null,
            string switchColorOn = null,
            string switchColorOff = null,
            bool rounded = true)
        {
            Alignment = SwitchAlignment.Left;
            Rounded = rounded;
            SetColors(backgroundColorOn, backgroundColorOff, switchColorOn, switchColorOff);
        }

        private void SetColors(
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
