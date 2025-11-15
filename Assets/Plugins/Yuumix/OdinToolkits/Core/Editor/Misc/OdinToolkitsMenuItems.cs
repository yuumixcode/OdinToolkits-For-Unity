namespace Yuumix.OdinToolkits.Core.Editor
{
    // 快捷键记录
    // & = Mac[shift + option]
    // % = Mac[shift + command]
    // % 表示 Ctrl（Mac 上是 Command）
    // & 表示 Alt（Mac 上是 Option）
    // # 表示 Shift
    public static class OdinToolkitsMenuItems
    {
        #region Primary MenuItem

        const string ROOT = "Tools/Odin Toolkits";

        #endregion

        public const string RESOLVED_PARAMETERS_OVERVIEW_WINDOW_NAME = "Resolved Parameters Overview";

        #region -970 GettingStarted

        public const string GETTING_STARTED = ROOT + "/Getting Started";
        public const int GETTING_STARTED_PRIORITY = -970;
        public const string GETTING_STARTED_WINDOW_NAME = "Getting Started";

        #endregion

        #region -900 EditorSettingsWindow

        public const string EDITOR_SETTINGS = ROOT + "/Editor Settings";
        public const string EDITOR_SETTINGS_WINDOW_NAME = "Editor Settings Window";
        public const int EDITOR_SETTINGS_PRIORITY = -900;

        #endregion

        #region -895 RuntimeConfigWindow

        public const string RUNTIME_CONFIG = ROOT + "/Runtime Config";
        public const int RUNTIME_CONFIG_PRIORITY = -895;
        public const string RUNTIME_CONFIG_WINDOW_NAME = "Runtime Config Window";

        #endregion

        #region -850 Script Doc Generator

        public const string SCRIPT_DOC_GEN = ROOT + "/Script Doc Generator";
        public const int SCRIPT_DOC_GEN_PRIORITY = -850;
        public const string SCRIPT_DOC_GEN_WINDOW_NAME = "Script Doc Generator";

        #endregion

        #region -845 Attribute Overview Pro

        public const string OVERVIEW_PRO = ROOT + "/Attributes Overview Pro";
        public const int OVERVIEW_PRO_PRIORITY = -845;
        public const string OVERVIEW_PRO_WINDOW_NAME = "Attributes Overview Pro";

        #endregion

        #region -840 ToolsPackage

        public const string TOOLS_PACKAGE_MENU_ITEM_NAME = ROOT + "/Tools Package %&T";
        public const int TOOLS_PACKAGE_PRIORITY = -840;
        public const string TOOLS_PACKAGE_WINDOW_ENGLISH_NAME = "Tools Package";

        #endregion

        #region 1000 Community

        public const string COMMUNITY = ROOT + "/Community";
        public const int COMMUNITY_PRIORITY = 1000;
        public const string COMMUNITY_WINDOW_NAME = "Community";

        #endregion
    }
}
