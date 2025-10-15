using System;
using System.Reflection;

namespace Yuumix.OdinToolkits.Modules
{
    public interface IPropertyAnalysisDataFactory
    {
        PropertyAnalysisData CreateFromPropertyInfo(PropertyInfo propertyInfo, Type type);
    }
}
