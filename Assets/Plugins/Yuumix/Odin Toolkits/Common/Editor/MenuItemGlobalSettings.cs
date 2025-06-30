namespace Yuumix.OdinToolkits.Common.Editor
{
    // 快捷键记录
    // & = Mac[shift + option]
    // % = Mac[shift + command]
    // % 表示 Ctrl（Mac 上是 Command）
    // & 表示 Alt（Mac 上是 Option）
    // # 表示Shift
    public static class MenuItemGlobalSettings
    {
        #region Top

        const string RootMenuItemName = "Tools/Odin Toolkits";
        const string WindowsMenuItemName = RootMenuItemName + "/Windows";
        const string ThirdPartyMenuItemName = RootMenuItemName + "/Third Party";
        const string WIPMenuItemName = RootMenuItemName + "/Work In Progress";

        #endregion

        #region -105 RuntimeConfig

        public const string RuntimeConfigMenuItemName = RootMenuItemName + "/Runtime Config";
        public const int RuntimeConfigPriority = -105;
        public const string RuntimeConfigWindowNameCn = "Odin Toolkits 运行时配置文件";
        public const string RuntimeConfigWindowNameEn = "Odin Toolkits Runtime Config";

        #endregion

        #region -100 EditorSettings

        public const string EditorSettingsMenuItemName = RootMenuItemName + "/Editor Settings";
        public const int EditorSettingsWindowPriority = -100;
        public const string EditorSettingsWindowNameCn = "Odin Toolkits 编辑器设置";
        public const string EditorSettingsWindowNameEn = "Odin Toolkits Editor Settings";

        #endregion

        #region -95 ToolsPackage

        public const string ToolsPackageMenuItemName = RootMenuItemName + "/Tools Package %&T";
        public const int ToolsPackagePriority = -95;
        public const string ToolsPackageWindowEnglishName = "Tools Package";

        #endregion

        #region Windows 0 - 200

        #region 5 Odin Attributes Chinese

        public const string AttributeChineseMenuItemName = WindowsMenuItemName + "/Attribute Overview Chinese";
        public const int AttributeChinesePriority = 5;
        public const string AttributeChineseWindowName = "Attribute Overview Chinese";

        #endregion

        #endregion

        #region ThirdParty 400 - 600

        public const string ResolvedParametersMenuItemName = ThirdPartyMenuItemName + "/Resolved Parameters Overview";
        public const int ResolvedParametersPriority = 10;
        public const string ResolvedParametersOverviewWindowName = "Resolved Parameters Overview";

        #endregion

        #region WIP (Work In Progress) EditorWindowHub

        public const string TempHubMenuItemName = WIPMenuItemName + "/Temp Hub";
        public const int TempHubPriority = 15;
        public const string ShowToastMethodWindowName = "ShowToast Method Demo";
        public const string InspectObjectMethodWindowName = "InspectObject Method Demo";
        public const string AttributeOverviewProWindowName = "Attribute Overview Pro";

        #endregion

        #region Help

        public const string HelpMenuItemName = RootMenuItemName + "/Help";
        public const int HelpWindowPriority = 1000;
        public const string HelpWindowNameCn = "Odin Toolkits 帮助窗口";
        public const string HelpWindowNameEn = "Odin Toolkits Help Window";

        #endregion
    }
}
