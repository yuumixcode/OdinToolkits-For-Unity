using Yuumix.OdinToolkits.AdvancedTypeAnalyzer;

namespace Yuumix.OdinToolkits.Tests.Editor
{
    /// <summary>
    /// 用于单元测试的解析数据工厂类
    /// </summary>
    public static class UnitTestAnalysisFactory
    {
        /// <summary>
        /// 用于测试的解析数据工厂，如果想要使用不同的解析数据工厂，可以在这里替换为其他工厂实例
        /// </summary>
        public static readonly IAnalysisDataFactory Default = new YuumixDefaultAnalysisDataFactory();
    }
}
