using System;

namespace Yuumix.OdinToolkits.Editor.Core
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

        const string ROOT_MENU_ITEM_NAME = "Tools/Odin Toolkits";
        const string GETTING_STARTED_MENU_ITEM_NAME = ROOT_MENU_ITEM_NAME + "/Getting Started";
        const string OVERVIEW_MENU_ITEM_NAME = ROOT_MENU_ITEM_NAME + "/Overview";
        const string EXPERIMENTAL_MENU_ITEM_NAME = ROOT_MENU_ITEM_NAME + "/Work In Progress";

        #endregion

        #region -105 RuntimeConfig

        public const string RuntimeConfigMenuItemName = ROOT_MENU_ITEM_NAME + "/Runtime Config";
        public const int RuntimeConfigPriority = -105;
        public const string RuntimeConfigWindowNameCn = "Odin Toolkits 运行时配置文件";
        public const string RuntimeConfigWindowNameEn = "Odin Toolkits Runtime Config";

        #endregion

        #region -100 EditorSettings

        public const string EditorSettingsMenuItemName = ROOT_MENU_ITEM_NAME + "/Editor Settings";
        public const int EditorSettingsWindowPriority = -100;
        public const string EditorSettingsWindowNameCn = "Odin Toolkits 编辑器设置";
        public const string EditorSettingsWindowNameEn = "Odin Toolkits Editor Settings";

        #endregion

        #region -95 ToolsPackage

        public const string ToolsPackageMenuItemName = ROOT_MENU_ITEM_NAME + "/Tools Package %&T";
        public const int ToolsPackagePriority = -95;
        public const string ToolsPackageWindowEnglishName = "Tools Package";

        #endregion

        #region Windows 0 - 200

        #region 5 Odin Attributes Chinese

        public const string AttributeChineseMenuItemName = OVERVIEW_MENU_ITEM_NAME + "/Attribute Overview Chinese";
        public const int AttributeChinesePriority = 5;
        public const string AttributeChineseWindowName = "Attribute Overview Chinese";

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

        public const string CommunityMenuItemName = ROOT_MENU_ITEM_NAME + "/Community";
        public const int CommunityPriority = 495;
        public const string CommunityWindowNameCn = "社区贡献窗口";
        public const string CommunityWindowNameEn = "Community Window";

        #endregion

        #region 500 Help

        public const string HelpMenuItemName = ROOT_MENU_ITEM_NAME + "/Help";
        public const int HelpWindowPriority = 500;
        public const string HelpWindowNameCn = "Odin Toolkits 帮助窗口";
        public const string HelpWindowNameEn = "Odin Toolkits Help Window";

        #endregion

        [Obsolete]
        public const string ResolvedParametersMenuItemName = CommunityMenuItemName + "/Resolved Parameters Overview";
        public const int ResolvedParametersPriority = 15;
        public const string ResolvedParametersOverviewWindowName = "Resolved Parameters Overview";
    }
}
