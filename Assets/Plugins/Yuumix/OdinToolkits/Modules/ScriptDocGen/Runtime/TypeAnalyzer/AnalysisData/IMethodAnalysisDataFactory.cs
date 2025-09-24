using System;
using System.Reflection;

namespace Yuumix.OdinToolkits.Modules
{
    public interface IMethodAnalysisDataFactory
    {
        MethodAnalysisData CreateFromMethodInfo(MethodInfo methodInfo, Type type);
    }
}