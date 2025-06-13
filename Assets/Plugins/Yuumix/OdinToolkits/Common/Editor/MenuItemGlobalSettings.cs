namespace Yuumix.OdinToolkits.Common.Editor
{
    // 快捷键记录
    // & = Mac[shift + option]
    // % = Mac[shift + command]
    public static class MenuItemGlobalSettings
    {
        #region Top

        const string RootMenuItemName = "Tools/Odin Toolkits";
        const string WindowsMenuItemName = RootMenuItemName + "/Windows";
        const string ThirdPartyMenuItemName = RootMenuItemName + "/Third Party";
        const string WIPMenuItemName = RootMenuItemName + "/Work In Progress";

        #endregion

        #region -100 EditorSettings

        public const string EditorSettingsMenuItemName = RootMenuItemName + "/Editor Settings";
        public const int EditorSettingsWindowPriority = -100;
        public const string EditorSettingsWindowName = "Editor Settings";

        #endregion

        #region Windows 0 - 200

        #region 0 ToolsPackage

        public const string ToolsPackageMenuItemName = WindowsMenuItemName + "/Tools Package &%T";
        public const int ToolsPackagePriority = 0;
        public const string ToolsPackageWindowEnglishName = "Tools Package";

        #endregion

        #region 5 Odin Attributes Chinese

        public const string AttributeChineseMenuItemName = WindowsMenuItemName + "/Attribute Overview Chinese";
        public const int AttributeChinesePriority = 5;
        public const string AttributeChineseWindowName = "Attribute Overview Chinese";

        #endregion

        #endregion

        #region ThirdParty 400 - 600

        public const string ResolvedParametersMenuItemName = ThirdPartyMenuItemName + "/Resolved Parameters Overview";
        public const int ResolvedParametersPriority = 400;
        public const string ResolvedParametersOverviewWindowName = "Resolved Parameters Overview";

        #endregion

        #region WIP (Work In Progress) EditorWindowHub

        public const string TempHubMenuItemName = WIPMenuItemName + "/Temp Hub";
        public const int TempHubPriority = 500;
        public const string ShowToastMethodWindowName = "ShowToast Method Demo";
        public const string InspectObjectMethodWindowName = "InspectObject Method Demo";
        public const string AttributeOverviewProWindowName = "Attribute Overview Pro";

        #endregion
    }
}
