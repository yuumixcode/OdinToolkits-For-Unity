using System;
using System.Reflection;
using UnityEngine;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.ScriptDocGenerator
{
    /// <summary>
    /// Yuumix 默认提供的解析数据工厂实现类
    /// </summary>
    [Summary("Yuumix 默认提供的解析数据工厂实现类")]
    [Serializable]
    public class YuumixDefaultAnalysisDataFactory : IAnalysisDataFactory
    {
        #region IAnalysisDataFactory Members

        [Summary("创建类型数据")]
        public ITypeData CreateTypeData(Type type, IAnalysisDataFactory factory = null,
            IAttributeFilter filter = null)
        {
            if (type != null)
            {
                return new TypeData(type, filter, factory ?? this);
            }

            Debug.LogError("Type is null");
            return null;
        }

        [Summary("创建构造函数数据")]
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

        [Summary("创建事件数据")]
        public IEventData CreateEventData(EventInfo eventInfo, IAttributeFilter filter = null)
        {
            if (eventInfo != null)
            {
                return new EventData(eventInfo, filter);
            }

            Debug.LogError("EventInfo is null");
            return null;
        }

        [Summary("创建方法数据")]
        public IMethodData CreateMethodData(MethodInfo methodInfo, IAttributeFilter filter = null)

        {
            if (methodInfo != null)
            {
                return new MethodData(methodInfo, filter);
            }

            Debug.LogError("MethodInfo is null");
            return null;
        }

        [Summary("创建属性数据")]
        public IPropertyData CreatePropertyData(PropertyInfo propertyInfo,
            IAttributeFilter filter = null)
        {
            if (propertyInfo != null)
            {
                return new PropertyData(propertyInfo, filter);
            }

            Debug.LogError("PropertyInfo is null");
            return null;
        }

        [Summary("创建字段数据")]
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
