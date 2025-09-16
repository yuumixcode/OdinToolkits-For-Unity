using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Core.Editor
{
    public static class ChangeCategory
    {
        public static string Added => "Added: ".ToGreen();
        public static string Changed => "Changed: ".ToYellow();
        public static string Deprecated => "Deprecated: ".ToRed();
        public static string Removed => "Removed: ".ToRed();
        public static string Fixed => "Fixed: ".ToGreen();
        public static string Security => "Security: ".ToRed();
    }
}
