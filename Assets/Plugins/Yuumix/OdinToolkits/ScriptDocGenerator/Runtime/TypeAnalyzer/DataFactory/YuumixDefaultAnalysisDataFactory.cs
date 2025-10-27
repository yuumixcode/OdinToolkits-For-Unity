using System;
using System.Reflection;
using UnityEngine;

namespace Yuumix.OdinToolkits.ScriptDocGenerator
{
    /// <summary>
    /// 解析数据工厂接口
    /// </summary>
    public interface IAnalysisDataFactory
    {
        public ITypeData CreateTypeData(Type type, IAnalysisDataFactory factory = null, IAttributeFilter filter = null);

        public IConstructorData CreateConstructorData(ConstructorInfo constructorInfo,
            IAttributeFilter filter = null);

        public IEventData CreateEventData(EventInfo eventInfo, IAttributeFilter filter = null);
        public IMethodData CreateMethodData(MethodInfo methodInfo, IAttributeFilter filter = null);
        public IPropertyData CreatePropertyData(PropertyInfo propertyInfo, IAttributeFilter filter = null);
        public IFieldData CreateFieldData(FieldInfo fieldInfo, IAttributeFilter filter = null);
    }

    /// <summary>
    /// Yuumix 默认提供的一个解析数据工厂
    /// </summary>
    [Serializable]
    public class YuumixDefaultAnalysisDataFactory : IAnalysisDataFactory
    {
        #region IAnalysisDataFactory Members

        public ITypeData CreateTypeData(Type type, IAnalysisDataFactory factory = null, IAttributeFilter filter = null)
        {
            if (type != null)
            {
                return new TypeData(type, filter, factory ?? this);
            }

            Debug.LogError("Type is null");
            return null;
        }

        public IConstructorData CreateConstructorData(ConstructorInfo constructorInfo,
            IAttributeFilter filter = null)
        {
            if (constructorInfo != null)
            {
                return new ConstructorData(constructorInfo, filter);
            }

            Debug.LogError("ConstructorInfo is null");
            return null;
        }

        public IEventData CreateEventData(EventInfo eventInfo, IAttributeFilter filter = null)
        {
            if (eventInfo != null)
            {
                return new EventData(eventInfo, filter);
            }

            Debug.LogError("EventInfo is null");
            return null;
        }

        public IMethodData CreateMethodData(MethodInfo methodInfo, IAttributeFilter filter = null)

        {
            if (methodInfo != null)
            {
                return new MethodData(methodInfo, filter);
            }

            Debug.LogError("MethodInfo is null");
            return null;
        }

        public IPropertyData CreatePropertyData(PropertyInfo propertyInfo, IAttributeFilter filter = null)
        {
            if (propertyInfo != null)
            {
                return new PropertyData(propertyInfo, filter);
            }

            Debug.LogError("PropertyInfo is null");
            return null;
        }

        public IFieldData CreateFieldData(FieldInfo fieldInfo, IAttributeFilter filter = null)
        {
            if (fieldInfo != null)
            {
                return new FieldData(fieldInfo, filter);
            }

            Debug.LogError("FieldInfo is null");
            return null;
        }

        #endregion
    }
}
