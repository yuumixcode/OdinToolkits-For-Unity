using System;

namespace Yuumix.OdinToolkits.Modules
{
    [Serializable]
    public class FieldAnalysisData : MemberAnalysisData
    {
        #region Serialized Fields

        public bool isStatic;
        public bool isConst;
        public bool isReadonly;

        #endregion

        // 移除了原来的 FromFieldInfo 方法，现在由 FieldAnalysisDataFactory 处理
    }
}
