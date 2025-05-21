namespace Yuumix.OdinToolkits.Common.Editor
{
    public static class OdinToolkitsMenuPaths
    {
        // 快捷键记录
        // & = Mac[shift + option]
        // % = Mac[shift + command]

        private const string OdinToolkitsMenuItemPath = "Tools/Odin Toolkits";

        private const string WindowsPath = OdinToolkitsMenuItemPath + "/Windows";

        // 工具菜单路径
        public const string OdinToolsMenuItemPath = OdinToolkitsMenuItemPath + "/开发工具箱";

        // Odin Attributes 解析总览
        public const string AttributeManualPath = WindowsPath + "/" + AttributeManualWindowName;
        public const int AttributeManualPriority = 50;
        public const string AttributeManualWindowName = "Odin Attributes 解析总览";

        // 小工具合集包
        public const string ToolsPackagePath = WindowsPath + "/Tools Package &%T";
        public const int ToolsPackagePriority = 0;

        private const string ThirdPartyMenuItemPath = OdinToolkitsMenuItemPath + "/Third Party";
        public const int ThirdPartyPriority = 200;

        public const string ResolvedParametersPath = ThirdPartyMenuItemPath + "/ResolvedParametersWindow 解析参数一览";
        public const int ResolvedParametersPriority = 0;

        public const string AttributeOverviewProMenuPath = WindowsPath + "/Attribute Overview Pro";
    }
}
