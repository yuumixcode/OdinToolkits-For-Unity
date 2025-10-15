using System;

namespace Yuumix.OdinToolkits.Modules
{
    public interface ITypeAnalysisDataFactory
    {
        TypeAnalysisData CreateFromType(Type type);
        MethodAnalysisData[] MarkOverloadMethod(MethodAnalysisData[] methodAnalysisDataArray);
    }
}
