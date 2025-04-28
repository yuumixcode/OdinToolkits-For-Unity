namespace YOGA.OdinToolkits.AnalysisManual.OdinAttributes.Editor
{
    public static class DescriptionConfigs
    {
        public const string ColorDescription = "填写颜色字符串，同时支持多种表示方式: " +
                                               "1. 颜色名称(如: \"red\", \"orange\", \"green\", \"blue\")，" +
                                               "2. Hex 码(如: \"#FF0000\" and \"#FF0000FF\")，" +
                                               "3. RGBA (如: \"RGBA(1,1,1,1)\") or RGB (如: \"RGB(1,1,1)\")，" +
                                               "4. 表达式获取字段值(如: \"@this.MyColor\")，" +
                                               "可以直接使用的颜色名称有: black, blue, clear, cyan, gray, green," +
                                               " grey, magenta, orange, purple, red, transparent, " +
                                               "transparentBlack, transparentWhite, white, yellow, lightblue, " +
                                               "lightcyan, lightgray, lightgreen, lightgrey, lightmagenta, " +
                                               "lightorange, lightpurple, lightred, lightyellow, darkblue, " +
                                               "darkcyan, darkgray, darkgreen, darkgrey, darkmagenta, " +
                                               "darkorange, darkpurple, darkred, darkyellow.";

        public const string SupportAllResolver = SupportMemberResolverLite + SupportOdinExpressions;

        public const string SupportMemberResolverLite =
            "支持解析成员名(包括字段，属性，成员方法)，特性构造函数的参数在 Rider 支持高亮显示。";

        public const string SupportOdinExpressions =
            "支持表达式返回值，如: @Debug.Log(xxx)，不会在 Rider 中高亮显示。";

        public const string InspectorPropertyDesc = "Odin 提供的 InspectorProperty 类型的对象，类似于 SerializedProperty";

        public const string ValueDesc = "添加该特性标记的 property 的值，值的类型为 property 的类型";
    }
}