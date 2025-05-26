namespace Yuumix.OdinToolkits.Common.Editor
{
    public static class OdinToolkitsMenuPaths
    {
        // 快捷键记录
        // & = Mac[shift + option]
        // % = Mac[shift + command]

        const string OdinToolkitsMenuItemPath = "Tools/Odin Toolkits";
        const string WindowsPath = OdinToolkitsMenuItemPath + "/Windows";

        public const string EditorSettingsPath = WindowsPath + "/Editor Settings";
        public const int EditorSettingsWindowPriority = -10;

        // Odin Attributes 解析总览
        public const string AttributeManualPath = WindowsPath + "/" + AttributeManualWindowName;
        public const int AttributeManualPriority = 50;
        public const string AttributeManualWindowName = "Odin Attributes 解析总览";

        // 小工具合集包
        public const string ToolsPackagePath = WindowsPath + "/Tools Package &%T";
        public const int ToolsPackagePriority = 10;

        const string ThirdPartyMenuItemPath = OdinToolkitsMenuItemPath + "/Third Party";
        public const int ThirdPartyPriority = 200;

        public const string ResolvedParametersPath = ThirdPartyMenuItemPath + "/ResolvedParametersWindow";
        public const int ResolvedParametersPriority = 0;

        public const string AttributeOverviewProMenuPath = WindowsPath + "/Attribute Overview Pro";
    }
}
