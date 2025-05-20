namespace Yuumix.OdinToolkits.Common.Editor
{
    public static class OdinToolkitsMenuPaths
    {
        private const string OdinToolkitsMenuItemPath = "Tools/Odin Toolkits";

        // 工具菜单路径
        public const string OdinToolsMenuItemPath = OdinToolkitsMenuItemPath + "/开发工具箱";

        // 中文学习手册菜单路径
        private const string AnalysisManualPath = OdinToolkitsMenuItemPath + "/解析手册";

        // Odin Attributes 解析总览
        public const string AttributeManualPath = AnalysisManualPath + "/" + AttributeManualWindowName;
        public const int AttributeManualPriority = 50;
        public const string AttributeManualWindowName = "Odin Attributes 解析总览";

        // 小工具合集包
        public const string ToolsPackagePath = OdinToolkitsMenuItemPath + "/工具箱";
        public const int ToolsPackagePriority = 150;

        private const string ThirdPartyMenuItemPath = OdinToolkitsMenuItemPath + "/第三方引用";
        public const int ThirdPartyPriority = 200;

        public const string ResolvedParametersPath = ThirdPartyMenuItemPath + "/ResolvedParametersWindow 解析参数一览";
        public const int ResolvedParametersPriority = 0;
    }
}
