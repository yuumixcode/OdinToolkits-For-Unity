namespace Yuumix.OdinToolkits.Core.Editor
{
    // 快捷键记录
    // & = Mac[shift + option]
    // % = Mac[shift + command]
    // % 表示 Ctrl（Mac 上是 Command）
    // & 表示 Alt（Mac 上是 Option）
    // # 表示 Shift
    public static class OdinToolkitsWindowMenuItems
    {
        #region Primary MenuItem

        const string ROOT = "Tools/Odin Toolkits";

        const string EXPERIMENTAL_MENU_ITEM_NAME = ROOT + "/Work In Progress";

        #endregion

        #region -100 GettingStarted

        public const string GETTING_STARTED = ROOT + "/Getting Started";
        public const int GETTING_STARTED_PRIORITY = -100;
        public const string GETTING_STARTED_WINDOW_NAME = "Getting Started";

        #endregion

        #region -80 Preferences

        public const string PREFERENCES = ROOT + "/Preferences";
        public const int PREFERENCES_PRIORITY = -80;
        public const string PREFERENCES_WINDOW_NAME = "Preferences";

        #endregion

        #region 0 ToolsPackage

        public const string TOOLS_PACKAGE_MENU_ITEM_NAME = ROOT + "/Tools Package %&T";
        public const int TOOLS_PACKAGE_PRIORITY = 0;
        public const string TOOLS_PACKAGE_WINDOW_ENGLISH_NAME = "Tools Package";

        #endregion

        #region Windows 0 - 200

        #region 5 Attribute Overview Pro

        public const string OVERVIEW_PRO = ROOT + "/Attributes Overview Pro";
        public const int OVERVIEW_PRO_PRIORITY = 5;
        public const string OVERVIEW_PRO_WINDOW_NAME = "Attributes Overview Pro";

        #endregion

        #region 6 Script Doc Generator

        public const string SCRIPT_DOC_GEN = ROOT + "/Script Doc Generator";
        public const int SCRIPT_DOC_GEN_PRIORITY = 6;
        public const string SCRIPT_DOC_GEN_WINDOW_NAME = "Script Doc Generator";

        #endregion

        #endregion

        #region 10 WIP (Work In Progress) EditorWindowHub

        public const string TempHubMenuItemName = EXPERIMENTAL_MENU_ITEM_NAME + "/Temp Hub";
        public const int TempHubPriority = 10;
        public const string ShowToastMethodWindowName = "ShowToast Method Demo";
        public const string InspectObjectMethodWindowName = "InspectObject Method Demo";
        public const string AttributeOverviewProWindowName = "Attribute Overview Pro";

        #endregion

        #region 495 Community

        public const string COMMUNITY = ROOT + "/Community";
        public const int COMMUNITY_PRIORITY = 200;
        public const string COMMUNITY_WINDOW_NAME = "Community";

        #endregion

        public const string RESOLVED_PARAMETERS_OVERVIEW_WINDOW_NAME = "Resolved Parameters Overview";
    }
}
