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

        #region -980 GettingStarted

        public const string GETTING_STARTED = ROOT + "/Getting Started";
        public const int GETTING_STARTED_PRIORITY = -980;
        public const string GETTING_STARTED_WINDOW_NAME = "Getting Started";

        #endregion

        #region 10100 EditorSettingsWindow

        public const string EDITOR_SETTINGS = ROOT + "/Editor Settings";
        public const int EDITOR_SETTINGS_PRIORITY = 10100;
        public const string EDITOR_SETTINGS_WINDOW_NAME = "Editor Settings Window";

        #endregion

        #region 10105 RuntimeConfigWindow

        public const string RUNTIME_CONFIG = ROOT + "/Runtime Config";
        public const int RUNTIME_CONFIG_PRIORITY = 10105;
        public const string RUNTIME_CONFIG_WINDOW_NAME = "Runtime Config Window";

        #endregion

        #region 10200 Script Doc Generator

        public const string SCRIPT_DOC_GEN = ROOT + "/Script Doc Generator";
        public const int SCRIPT_DOC_GEN_PRIORITY = 10200;
        public const string SCRIPT_DOC_GEN_WINDOW_NAME = "Script Doc Generator";

        #endregion

        #region 10200 Attribute Overview Pro

        public const string OVERVIEW_PRO = ROOT + "/Attribute Overview Pro";
        public const int OVERVIEW_PRO_PRIORITY = 10200;
        public const string OVERVIEW_PRO_WINDOW_NAME = "Attribute Overview Pro";

        #endregion

        #region 10200 ToolsPackage

        public const string TOOLS_PACKAGE_MENU_ITEM_NAME = ROOT + "/Tools Package %&T";
        public const int TOOLS_PACKAGE_PRIORITY = 10200;
        public const string TOOLS_PACKAGE_WINDOW_ENGLISH_NAME = "Tools Package";

        #endregion

        #region 100000 Community

        public const string COMMUNITY = ROOT + "/Community";
        public const int COMMUNITY_PRIORITY = 100000;
        public const string COMMUNITY_WINDOW_NAME = "Community";

        #endregion
    }
}
