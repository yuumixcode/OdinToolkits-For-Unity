using System;
using System.Reflection;

namespace Yuumix.OdinToolkits.Modules
{
    public interface IEventAnalysisDataFactory
    {
        EventAnalysisData CreateFromEventInfo(EventInfo eventInfo, Type type);
    }
}