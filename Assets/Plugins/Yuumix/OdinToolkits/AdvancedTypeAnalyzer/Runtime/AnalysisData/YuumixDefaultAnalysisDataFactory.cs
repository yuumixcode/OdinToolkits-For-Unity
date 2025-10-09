using System.Reflection;

namespace Yuumix.OdinToolkits.AdvancedTypeAnalyzer
{
    /// <summary>
    /// 解析数据工厂接口
    /// </summary>
    public interface IAnalysisDataFactory
    {
        public IFieldData CreateFieldData(FieldInfo fieldInfo);
        public IEventData CreateEventData(EventInfo eventInfo);
        public IPropertyData CreatePropertyData(PropertyInfo propertyInfo);
        public IMethodData CreateMethodData(MethodInfo methodInfo);
    }

    /// <summary>
    /// Yuumix 默认提供的一个解析数据工厂
    /// </summary>
    public class YuumixDefaultAnalysisDataFactory : IAnalysisDataFactory
    {
        public IFieldData CreateFieldData(FieldInfo fieldInfo) => new FieldData(fieldInfo);

        public IEventData CreateEventData(EventInfo eventInfo) => new EventData(eventInfo);

        public IPropertyData CreatePropertyData(PropertyInfo propertyInfo) => new PropertyData(propertyInfo);

        public IMethodData CreateMethodData(MethodInfo methodInfo) => new MethodData(methodInfo);

        public IParameterData CreateParameterData(ParameterInfo parameterInfo) => new ParameterData(parameterInfo);
    }
}
