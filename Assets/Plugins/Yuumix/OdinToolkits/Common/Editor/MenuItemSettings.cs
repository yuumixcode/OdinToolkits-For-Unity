namespace Yuumix.OdinToolkits.Common.Editor
{
    // 快捷键记录
    // & = Mac[shift + option]
    // % = Mac[shift + command]
    public static class MenuItemSettings
    {
        #region Top

        const string RootMenuItemName = "Tools/Odin Toolkits";
        const string WindowsMenuItemName = RootMenuItemName + "/Windows";
        const string LearnArchiveMenuItemName = RootMenuItemName + "/Learn Archive";
        const string ThirdPartyMenuItemName = RootMenuItemName + "/Third Party";
        const string OdinEditorWindowAPIMenuItemName = LearnArchiveMenuItemName + "/OdinEditorWindow";

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
        public const string ToolsPackageWindowName = "Tools Package";

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

        #region Learn Archive 800 - 1000

        #region 800 ShowToastMethod

        public const string ShowToastMethodMenuItemName = OdinEditorWindowAPIMenuItemName + "/ShowToast Method";
        public const int ShowToastMethodPriority = 800;
        public const string ShowToastMethodWindowName = "ShowToast Method Demo";

        #endregion

        #region 801 InspectObjectMethod

        public const string InspectObjectMethodMenuItemName = OdinEditorWindowAPIMenuItemName + "/InspectObject Method";
        public const int InspectObjectPriority = 801;
        public const string InspectObjectMethodWindowName = "InspectObject Method Demo";

        #endregion

        #region 805 SirenixGUIStyleOverview

        public const string SirenixGUIStyleOverviewMenu = LearnArchiveMenuItemName + "/SirenixGUIStyle Overview";
        public const int SirenixGUIStyleOverviewPriority = 805;

        #endregion

        #region Editor API Learn

        public const string EditorAPILearnMenuItem = LearnArchiveMenuItemName+"/Editor API Learn";
        public const int EditorAPIPriority = 820;

        #endregion

        #region 1000 Attribute Overview Pro

        public const string AttributeOverviewProMenuItemName = LearnArchiveMenuItemName + "/Attribute Overview Pro";
        public const int AttributeOverviewProPriority = 1000;
        public const string AttributeOverviewProWindowName = "Attribute Overview Pro";

        #endregion

        #endregion
    }
}
