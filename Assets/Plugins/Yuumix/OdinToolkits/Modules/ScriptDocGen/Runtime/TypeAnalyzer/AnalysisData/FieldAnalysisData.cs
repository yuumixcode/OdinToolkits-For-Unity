using System;
using System.Reflection;
using System.Text;
using Yuumix.OdinToolkits.Core;

namespace Yuumix.OdinToolkits.Modules
{
    [Serializable]
    public class FieldAnalysisData : MemberAnalysisData
    {
        public bool isStatic;
        public bool isConst;
        public bool isReadonly;
        
        // 移除了原来的 FromFieldInfo 方法，现在由 FieldAnalysisDataFactory 处理
    }
}
