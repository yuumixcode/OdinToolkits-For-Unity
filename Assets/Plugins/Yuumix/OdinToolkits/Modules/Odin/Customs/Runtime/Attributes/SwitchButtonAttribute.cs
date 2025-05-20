using System;

namespace Yuumix.OdinToolkits.Modules.Odin.Customs.Runtime.Attributes
{
    public enum SwitchAlignment
    {
        Left,
        Right,
        Center
    }

    public class SwitchButtonAttribute : Attribute
    {
        private const string DefaultBackgroundColorOn = "@new Color(0.498f, 0.843f, 0.992f)";
        private const string DefaultBackgroundColorOff = "@new Color(0.165f, 0.165f, 0.165f)";
        private const string DefaultSwitchColorOn = DefaultBackgroundColorOff;
        private const string DefaultSwitchColorOff = DefaultBackgroundColorOn;
        private const int DefaultSwitchWidth = 28;
        public SwitchAlignment Alignment;
        public bool Rounded;
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
