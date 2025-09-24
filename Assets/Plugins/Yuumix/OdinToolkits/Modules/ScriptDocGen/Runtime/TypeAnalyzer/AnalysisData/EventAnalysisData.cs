using System;
using System.Reflection;
using System.Text;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules
{
    [Serializable]
    public class EventAnalysisData : MemberAnalysisData
    {
        public bool isStatic;
        
        // 移除了原来的 FromEventInfo 方法，现在由 EventAnalysisDataFactory 处理
    }
}
