namespace Yuumix.OdinToolkits.Modules.APIBrowser.Tests
{
    public abstract class PropertyClassTestBase
    {
        public static float DefaultLabelWidth { get; set; }
        public static int DefaultLabelHeight { get; private set; }
        public static string DefaultLabelText { get; protected set; }
        public string DefaultLabelText2 { get; set; }
        public virtual string DefaultLabelText3 { get; set; }
        public abstract string DefaultLabelText4 { get; set; }
    }
}
