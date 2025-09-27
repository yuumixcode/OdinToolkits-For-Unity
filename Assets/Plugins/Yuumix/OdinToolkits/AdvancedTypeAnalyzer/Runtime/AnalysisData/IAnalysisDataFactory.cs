using System.Reflection;

namespace Yuumix.OdinToolkits.AdvancedTypeAnalyzer
{
    /// <summary>
    /// 解析数据工厂接口
    /// </summary>
    public interface IAnalysisDataFactory
    {
        public IFieldData CreateFieldData(FieldInfo fieldInfo);
    }

    /// <summary>
    /// Yuumix 默认提供的一个解析数据工厂
    /// </summary>
    public class YuumixDefaultAnalysisDataFactory : IAnalysisDataFactory
    {
        public IFieldData CreateFieldData(FieldInfo fieldInfo)
        {
            return new FieldData(fieldInfo);
        }
    }
}
