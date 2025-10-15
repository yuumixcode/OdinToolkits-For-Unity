using System;
using System.Reflection;

namespace Yuumix.OdinToolkits.Modules
{
    public interface IFieldAnalysisDataFactory
    {
        FieldAnalysisData CreateFromFieldInfo(FieldInfo fieldInfo, Type type);
    }
}
