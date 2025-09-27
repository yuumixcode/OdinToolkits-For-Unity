using Yuumix.OdinToolkits.AdvancedTypeAnalyzer;

namespace Yuumix.OdinToolkits.Tests.Editor
{
    /// <summary>
    /// 用于单元测试的解析数据工厂类
    /// </summary>
    public static class UnitTestAnalysisFactory
    {
        /// <summary>
        /// 用于测试的解析数据工厂，如果要测试不同的解析数据工厂，只需要修改这个字段即可
        /// </summary>
        public static readonly IAnalysisDataFactory Instance = new YuumixDefaultAnalysisDataFactory();
    }
}
