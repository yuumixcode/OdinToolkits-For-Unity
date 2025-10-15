using System;

namespace Yuumix.OdinToolkits.Modules
{
    [Serializable]
    public class EventAnalysisData : MemberAnalysisData
    {
        #region Serialized Fields

        public bool isStatic;

        #endregion

        // 移除了原来的 FromEventInfo 方法，现在由 EventAnalysisDataFactory 处理
    }
}
