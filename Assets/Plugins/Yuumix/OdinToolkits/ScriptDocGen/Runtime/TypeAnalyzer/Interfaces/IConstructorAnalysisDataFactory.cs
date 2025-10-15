using System;
using System.Reflection;

namespace Yuumix.OdinToolkits.Modules
{
    public interface IConstructorAnalysisDataFactory
    {
        ConstructorAnalysisData CreateFromConstructorInfo(ConstructorInfo constructorInfo, Type type);
    }
}
