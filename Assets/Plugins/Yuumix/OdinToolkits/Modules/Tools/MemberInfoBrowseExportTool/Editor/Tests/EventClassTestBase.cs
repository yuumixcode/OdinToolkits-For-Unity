using System;

namespace Yuumix.OdinToolkits.Modules.Tools.MemberInfoBrowseExportTool.Editor.Tests
{
    public abstract class EventClassTestBase
    {
        public Action<float> ActionFloat;
        public Action ActionNo;
        public Func<float> FuncFloat;
        public abstract event Action EventAbstract;
        public abstract event Action<float> EventAbstractFloat;
        protected abstract event Action EventProtectedAbstract;
        protected abstract event Action<float> EventProtectedFloat;
        internal abstract event Action EventInternal;
        internal abstract event Action<float> EventInternalFloat;
#pragma warning disable CS0067 // 事件从未使用过
        public event Action EventNo;
        public event Action<float> EventFloat;
        public event Action<float, int> EventFloatInt;
        public event Func<float> EventFuncFloat;
#pragma warning restore CS0067 // 事件从未使用过
    }
}
