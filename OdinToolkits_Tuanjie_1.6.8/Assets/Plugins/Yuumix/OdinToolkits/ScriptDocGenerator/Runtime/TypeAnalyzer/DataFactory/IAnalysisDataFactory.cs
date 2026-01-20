using System;
using System.Reflection;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.ScriptDocGenerator
{
    [Summary("解析数据工厂接口，自定义扩展解析数据工厂")]
    public interface IAnalysisDataFactory
    {
        [Summary("创建类型数据")]
        public ITypeData CreateTypeData(Type type, IAnalysisDataFactory factory = null,
            IAttributeFilter filter = null);

        [Summary("创建构造函数数据")]
        public IConstructorData CreateConstructorData(ConstructorInfo constructorInfo,
            IAttributeFilter filter = null);

        [Summary("创建事件数据")]
        public IEventData CreateEventData(EventInfo eventInfo, IAttributeFilter filter = null);

        [Summary("创建方法数据")]
        public IMethodData CreateMethodData(MethodInfo methodInfo, IAttributeFilter filter = null);

        [Summary("创建属性数据")]
        public IPropertyData CreatePropertyData(PropertyInfo propertyInfo, IAttributeFilter filter = null);

        [Summary("创建字段数据")]
        public IFieldData CreateFieldData(FieldInfo fieldInfo, IAttributeFilter filter = null);
    }
}
